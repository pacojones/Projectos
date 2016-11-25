using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class SqlHelper
    {
        private static object lockObject = new object();
        private static SqlDatabase _database = null;

        private DataContext context;
        private int retryCount;

        private long? outputResult;

        public long? OutputResult
        {
            get
            {
                return this.outputResult;
            }
        }

        public SqlHelper(DataContext context)
        {
            this.context = context;
            this.retryCount = 30;
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public SqlDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    lock (lockObject)
                    {
                        DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
                        _database = DatabaseFactory.CreateDatabase(Constants.DbConnectionName) as SqlDatabase;
                    }
                }

                return _database;
            }
        }

        private void AddParameters(SqlCommand cmd,  Dictionary<string, object> parameters)
        {
            SqlParameter output = new SqlParameter("@O_R", SqlDbType.BigInt);
            output.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(output);

            SqlParameter contextInput = new SqlParameter("@I_Context", SqlDbType.Xml);
            contextInput.Direction = ParameterDirection.Input;
            contextInput.Value = SerializationHelper.SerializeObject(context);
            cmd.Parameters.Add(contextInput);

            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Int32.</returns>
        internal int ExecuteNonQuery(string procedure, Dictionary<string, object> parameters)
        {
            int result = -1;

            var connection = this.Database.CreateConnection() as SqlConnection;

            SqlCommand cmd = new SqlCommand(procedure, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            this.AddParameters(cmd, parameters);

            int currentAttempt = 1;

            while(currentAttempt <= this.retryCount)
            {
                try
                {
                    connection.Open();

                    cmd.Transaction = connection.BeginTransaction(IsolationLevel.Snapshot);

                    result = cmd.ExecuteNonQuery();

                    cmd.Transaction.Commit();

                    outputResult = (long)cmd.Parameters["@O_R"].Value;

                    break;
                }
                catch(SqlException ex)
                {
                    if (cmd.Transaction != null)
                        cmd.Transaction.Rollback();

                    if (ex.ErrorCode.In(1204, 1205, 1211, 1222, 1510, 2625, 3309, 3928, 3960, 3961, 7112, 7391, 8650))
                    {
                        currentAttempt++;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception)
                {
                    if (cmd.Transaction != null)
                        cmd.Transaction.Rollback();

                    throw;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
            
            return result;
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>List&lt;Dictionary&lt;System.String, System.Object&gt;&gt;.</returns>
        internal DataSet ExecuteReader(string procedure, Dictionary<string, object> parameters)
        {
            var connection = this.Database.CreateConnection() as SqlConnection;

            SqlCommand cmd = new SqlCommand(procedure, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            this.AddParameters(cmd, parameters);

            connection.Open();
            
            var dataReader = cmd.ExecuteReader();

            DataSet dataSet = new DataSet() ;
            DataTable dataTable;
            object[] values;

            try
            {
                do
                {
                    dataTable = new DataTable();
                    this.BuildColumnsList(dataReader, dataTable);

                    // Load the rows into the DataTable.
                    values = new object[dataReader.FieldCount];

                    dataTable.BeginLoadData();

                    while (dataReader.Read())
                    {
                        dataReader.GetValues(values);

                        dataTable.Rows.Add(values);
                    }

                    dataTable.EndLoadData();

                    dataSet.Tables.Add(dataTable);
                }
                while (dataReader.NextResult());
            }
            finally
            {
                dataReader.Close();
            }

            /*var columns = reader.GetSchemaTable().Rows
                                     .Cast<DataRow>()
                                     .Select(r => (string)r["ColumnName"])
                                     .ToList();

            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                Dictionary<string, object> item = new Dictionary<string, object>();

                foreach (var column in columns)
                    item.Add(column, reader[column]);

                items.Add(item);
            }*/

            connection.Close();

            return dataSet;
        }

        private void BuildColumnsList(SqlDataReader dataReader, DataTable dataTable)
        {
            DataTable schemaTable;
            DataColumn column;

            schemaTable = dataReader.GetSchemaTable();
            if (schemaTable != null)
            {
                foreach (DataRow dataRow in schemaTable.Rows)
                {
                    // Each SchemaTable row represents a DataColumn.
                    // Each SchemaTable column represents a property for the DataColumn.
                    // When we iterate over the SchemaTable's Columns collection, we'll populate the DataColumn's properties.
                    column = new DataColumn(Convert.ToString(dataRow["ColumnName"]));
                    column.DataType = (Type)dataRow["DataType"];

                    // Avoid duplicate column names
                    if (dataTable.Columns.Contains(column.ColumnName))
                    {
                        while (dataTable.Columns.Contains(column.ColumnName))
                        {
                            column.ColumnName = column.ColumnName + "_";
                        }
                    }

                    dataTable.Columns.Add(column);
                }
            }
        }

    }
}
