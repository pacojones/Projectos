USE MyDb


--#############################################################################################--
--######################################"#  TIPOS    #"########################################--
--#############################################################################################--


/*GO
  CREATE TYPE Amount
    FROM DECIMAL(18, 5) NOT NULL*/


--#############################################################################################--
--#######################################  TABELAS    #########################################--
--#############################################################################################--

/*
DROP PROCEDURE SP_StoreListSelect
DROP PROCEDURE SP_ShoppingCartItemUpdate
DROP PROCEDURE SP_ShoppingCartListSelect
DROP PROCEDURE SP_ShoppingCartSelect
DROP PROCEDURE SP_ShoppingCartUpdate
DROP PROCEDURE SP_ShoppingCartInsert


DROP TABLE TBL_ShoppingCartItems
DROP TABLE TBL_ShoppingCart
DROP TABLE TBL_Users
DROP TABLE SYS_ShoppingCartActions
DROP TABLE SYS_ShoppingCartItems
DROP TABLE SYS_ShoppingCartItemStates
DROP TABLE SYS_ShoppingCartStates
DROP TABLE SYS_UsersTypes
DROP TABLE SYS_ShoppingCartItemLocations
DROP TABLE SYS_ShoppingCartItemUnits
DROP TABLE SYS_ShoppingCartStores
DROP TABLE SYS_ShoppingCartItemCategories
*/

GO
CREATE TABLE SYS_ShoppingCartItemCategories (ID           BIGINT IDENTITY(1, 1) NOT NULL,
                                             TS           TIMESTAMP,
                                             Code         VARCHAR(32)   NOT NULL,
                                             Description  VARCHAR(128)  NOT NULL,
                                             FlagEnabled  TINYINT       NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartItemCategories PRIMARY KEY (ID)
)

GO
CREATE INDEX UK_SYS_ShoppingCartItemCategories ON SYS_ShoppingCartItemCategories (Code)

GO
CREATE TABLE SYS_ShoppingCartStores (ID           BIGINT IDENTITY(1, 1) NOT NULL,
                                     TS           TIMESTAMP,
                                     Code         VARCHAR(32)  NOT NULL,
                                     Description  VARCHAR(128) NOT NULL,
                                     FlagEnabled  TINYINT      NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartStores PRIMARY KEY (ID)
)

GO
CREATE INDEX UK_SYS_ShoppingCartStores ON SYS_ShoppingCartStores (Code)

GO
CREATE TABLE SYS_ShoppingCartItemUnits (ID           BIGINT IDENTITY(1, 1) NOT NULL,
                                     TS           TIMESTAMP,
                                     Code         VARCHAR(32)  NOT NULL,
                                     Description  VARCHAR(128) NOT NULL,
                                     FlagEnabled  TINYINT      NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartItemUnits PRIMARY KEY (ID)
)

GO
CREATE INDEX UK_SYS_ShoppingCartItemUnits ON SYS_ShoppingCartItemUnits (Code)

GO
CREATE TABLE SYS_ShoppingCartItemLocations(ID           BIGINT IDENTITY(1, 1) NOT NULL,
                                           TS           TIMESTAMP,
                                           Code         VARCHAR(32)  NOT NULL,
                                           Description  VARCHAR(128) NOT NULL,
                                           [Order]       BIGINT NOT NULL,
                                           FlagEnabled  TINYINT      NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartItemLocations PRIMARY KEY (ID)
)

GO
CREATE INDEX UK_SYS_ShoppingCartItemLocations ON SYS_ShoppingCartItemLocations (Code)

GO
CREATE TABLE SYS_UsersTypes (ID BIGINT IDENTITY(1, 1) NOT NULL,
                             TS TIMESTAMP,
                             Code VARCHAR(32) NOT NULL,
                             Description VARCHAR(128) NOT NULL,
  CONSTRAINT PK_SYS_UsersTypes PRIMARY KEY (ID)
)

GO 
CREATE INDEX UK_SYS_UsersTypes ON SYS_UsersTypes (Code)

GO
CREATE TABLE SYS_ShoppingCartStates (ID BIGINT IDENTITY(1, 1) NOT NULL,
                             TS TIMESTAMP,
                             Code VARCHAR(32) NOT NULL,
                             Description VARCHAR(128) NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartStates PRIMARY KEY (ID)
)

GO 
CREATE INDEX UK_SYS_ShoppingCartStates ON SYS_ShoppingCartStates (Code)

GO
CREATE TABLE SYS_ShoppingCartItemStates (ID BIGINT IDENTITY(1, 1) NOT NULL,
                             TS TIMESTAMP,
                             Code VARCHAR(32) NOT NULL,
                             Description VARCHAR(128) NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartItemStates PRIMARY KEY (ID)
)

GO 
CREATE INDEX UK_SYS_ShoppingCartItemStates ON SYS_ShoppingCartItemStates (Code)

GO
CREATE TABLE SYS_ShoppingCartItems (ID              BIGINT IDENTITY(1, 1) NOT NULL,
                                    TS              TIMESTAMP,
                                    CategoryID      BIGINT        NOT NULL,
                                    Code            VARCHAR(32)   NOT NULL,
                                    Description     VARCHAR(128)  NOT NULL,
                                    UnitTypeID      BIGINT        NOT NULL,
                                    LocationID      BIGINT        NOT NULL,
                                    UnitPrice       Amount        NOT NULL,
                                    DefaultStoreID  BIGINT        NULL,
                                    DefaultQuantity BIGINT        NULL,
                                    FlagEnabled     TINYINT       NOT NULL,
  CONSTRAINT PK_SYS_ShoppingCartItems PRIMARY KEY (ID),
  CONSTRAINT FK_SYS_ShoppingCartItems_SYS_ShoppingCartItemCategories FOREIGN KEY (CategoryID)     REFERENCES SYS_ShoppingCartItemCategories (ID),
  CONSTRAINT FK_SYS_ShoppingCartItems_SYS_ShoppingCartItemUnits      FOREIGN KEY (UnitTypeID)     REFERENCES SYS_ShoppingCartItemUnits (ID),
  CONSTRAINT FK_SYS_ShoppingCartItems_SYS_ShoppingCartStores         FOREIGN KEY (DefaultStoreID) REFERENCES SYS_ShoppingCartStores (ID)
)

GO 
CREATE INDEX UK_SYS_ShoppingCartItems ON SYS_ShoppingCartItems (Code)

GO
CREATE TABLE SYS_ShoppingCartActions (ID              BIGINT IDENTITY(1, 1) NOT NULL,
                                      TS              TIMESTAMP,
                                      Code            VARCHAR(32)   NOT NULL,
                                      Description     VARCHAR(128)  NOT NULL,
                                      StatusID        BIGINT        NULL,
  CONSTRAINT PK_SYS_ShoppingCartActions PRIMARY KEY (ID),
  CONSTRAINT FK_SYS_ShoppingCartActions_SYS_ShoppingCartStates FOREIGN KEY (StatusID) REFERENCES SYS_ShoppingCartStates (ID)
)

GO 
CREATE INDEX UK_SYS_ShoppingCartActions ON SYS_ShoppingCartActions (Code)

GO
CREATE TABLE TBL_Users (ID BIGINT IDENTITY(1, 1) NOT NULL,
                        TS TIMESTAMP,
                        TypeID BIGINT NOT NULL,
                        Username VARCHAR(32) NOT NULL,
                        Name VARCHAR(128) NOT NULL,
                        Email VARCHAR(128) NULL,
                        FlagEnabled TINYINT NOT NULL,
  CONSTRAINT PK_TBL_Users PRIMARY KEY (ID),
  CONSTRAINT FK_TBL_Users_SYS_UsersTypes FOREIGN KEY (TypeID) REFERENCES SYS_UsersTypes (ID)
)


GO 
CREATE INDEX UK_TBL_Users ON TBL_Users (Username)

GO
CREATE TABLE TBL_ShoppingCart(ID        BIGINT IDENTITY(1, 1) NOT NULL,
                              TS        TIMESTAMP,
                              StatusID  BIGINT NOT NULL,
                              OwnerID   BIGINT NOT NULL,
  CONSTRAINT PK_TBL_ShoppingCart                        PRIMARY KEY (ID),
  CONSTRAINT FK_TBL_ShoppingCart_SYS_ShoppingCartStates FOREIGN KEY (StatusID)  REFERENCES SYS_ShoppingCartStates (ID),
  CONSTRAINT FK_TBL_ShoppingCart_TBL_Users              FOREIGN KEY (OwnerID)   REFERENCES TBL_Users (ID),
)

GO
CREATE TABLE TBL_ShoppingCartItems (ID        BIGINT IDENTITY(1, 1) NOT NULL,      
                                    TS        TIMESTAMP,
                                    CartID    BIGINT NOT NULL,
                                    ItemID    BIGINT NOT NULL,
                                    Quantity  BIGINT NOT NULL,
                                    StoreID   BIGINT NOT NULL,
                                    StatusID  BIGINT NOT NULL,
  CONSTRAINT PK_TBL_ShoppingCartItems                            PRIMARY KEY (ID),
  CONSTRAINT FK_TBL_ShoppingCartItems_TBL_ShoppingCart           FOREIGN KEY (CartID)   REFERENCES TBL_ShoppingCart (ID),
  CONSTRAINT FK_TBL_ShoppingCartItems_SYS_ShoppingCartItemStates FOREIGN KEY (StatusID) REFERENCES SYS_ShoppingCartItemStates (ID),
  CONSTRAINT FK_TBL_ShoppingCartItems_SYS_ShoppingCartStores     FOREIGN KEY (StatusID) REFERENCES SYS_ShoppingCartStores (ID),
)


--#############################################################################################--
--########################################  PROCS    ##########################################--
--#############################################################################################--

--  EXEC SP_ShoppingCartInsert NULl, NULL

GO
CREATE PROCEDURE SP_ShoppingCartInsert
   @O_R         BIGINT OUTPUT,
   @I_Context   XML
  AS
BEGIN

  SET @O_R = 0;
  
  INSERT TBL_ShoppingCart (StatusID,
                           OwnerID)
    SELECT ST.ID  StatusID,
           USR.ID OwnerID
      FROM SYS_ShoppingCartStates ST
      LEFT JOIN TBL_Users         USR ON USR.Username = 'admin'--@I_Username
      WHERE ST.Code = 'OPEN'

  SET @O_R = SCOPE_IDENTITY();

  INSERT TBL_ShoppingCartItems (CartID,
                                ItemID,
                                Quantity,
                                StoreID,
                                StatusID)
    SELECT @O_R               CartID,
           IT.ID              ItemID,
           IT.DefaultQuantity Quantity,
           IT.DefaultStoreID  StoreID,
           ST.ID              StatusID
      FROM SYS_ShoppingCartItems           IT
      LEFT JOIN SYS_ShoppingCartItemStates ST ON ST.Code = 'OPEN'
      WHERE IT.FlagEnabled = 1

END

GO
CREATE PROCEDURE SP_ShoppingCartItemUpdate
  @O_R                    BIGINT OUTPUT,
  @I_Context              XML,
  @I_ShoppingCartItemID   BIGINT,
  @I_Quantity             BIGINT = NULL,
  @I_StoreID              BIGINT = NULL
AS
BEGIN

  SET @O_R = 0;

  UPDATE TBL_ShoppingCartItems
    SET Quantity = ISNULL(@I_Quantity, Quantity),
        StoreID = ISNULL(@I_StoreID, StoreID)
    WHERE ID = @I_ShoppingCartItemID;

  SET @O_R = @O_R + @@rowcount;

END

GO
CREATE PROCEDURE SP_ShoppingCartListSelect
  @O_R        BIGINT OUTPUT,
  @I_Context  XML
AS
BEGIN

  SET @O_R = 0;

  SELECT TOP 10 SC.*,
         ST.Code        StateCode,
         ST.Description StateDescription,
         U.Username     OwnerUsername,
         U.Name         OwnerName
    FROM TBL_ShoppingCart       SC
    JOIN SYS_ShoppingCartStates ST ON SC.StatusID = ST.ID 
    JOIN TBL_Users              U  ON SC.OwnerID = U.ID
    ORDER BY SC.ID DESC

  SET @O_R = @@rowcount;

END



GO
CREATE PROCEDURE SP_ShoppingCartSelect
  @O_R              BIGINT OUTPUT,
  @I_Context        XML,
  @I_ShoppingCartID BIGINT = NULL
AS
BEGIN

  SET @O_R = 0;
  
  SELECT SC.*,
         U.Username     Owner,
         ST.Code        StateCode,
         ST.Description StateDescription,
         U.Username     OwnerUsername,
         U.Name         OwnerName
    FROM TBL_ShoppingCart       SC
    JOIN SYS_ShoppingCartStates ST ON SC.StatusID = ST.ID 
    JOIN TBL_Users              U  ON SC.OwnerID = U.ID
    WHERE SC.ID = @I_ShoppingCartID

  SET @O_R = @O_R + @@rowcount;

  SELECT IT.*,
         DF.Code            DefinitionCode,
         DF.Description     DefinitionDescription,
         DF.DefaultQuantity DefaultQuantity,
         DF.UnitPrice       UnitPrice,
         DF.CategoryID      CategoryID,
         CAT.Code           CategoryCode,
         CAT.Description    CategoryDescription,
         DF.UnitTypeID      UnitTypeID,
         UT.Code            UnitCode,
         UT.Description     UnitDescription,
         DF.LocationID      LocationID,
         LC.Code            LocationCode,
         LC.Description     LocationDescription,
         LC.[Order]         LocationOrder,
         ST.Code            StoreCode,
         ST.Description     StoreDescription,
         ES.Code            StatusCode,
         ES.Description     StatusDescription,
         DF.DefaultStoreID  DefaultStoreID,
         ST_D.Code          DefaultStoreCode,
         ST_D.Description   DefaultStoreDescription
    FROM TBL_ShoppingCartItems          IT
    JOIN SYS_ShoppingCartItems          DF  ON IT.ItemID = DF.ID
    JOIN SYS_ShoppingCartItemCategories CAT ON DF.CategoryID = CAT.ID
    JOIN SYS_ShoppingCartItemUnits      UT  ON DF.UnitTypeID = UT.ID
    JOIN SYS_ShoppingCartStores         ST  ON IT.StoreID = ST.ID
    JOIN SYS_ShoppingCartStores         ST_D ON DF.DefaultStoreID = ST_D.ID
    JOIN SYS_ShoppingCartItemLocations  LC  ON DF.LocationID = LC.ID
    JOIN SYS_ShoppingCartItemStates     ES  ON IT.StatusID = ES.ID
    WHERE IT.CartID = @I_ShoppingCartID

  SET @O_R = @O_R + @@rowcount;

END

GO
CREATE PROCEDURE SP_ShoppingCartUpdate
   @O_R               BIGINT OUTPUT,
   @I_Context         XML,
   @I_ShoppingCartID  BIGINT,
   @I_ActionID        BIGINT,
   @I_ShoppingCart    XML
  AS
BEGIN
  
  SET @O_R = 0;

  DECLARE @Items TABLE(ID BIGINT NOT NULL,
                       Quantity BIGINT NOT NULL,
                       StoreID BIGINT NOT NULL,
                       PRIMARY KEY (ID))
  
  INSERT @Items (ID,
                 Quantity,
                 StoreID)
  SELECT a.b.value('(ID[1])','BIGINT')      ID,
         a.b.value('Quantity[1]','BIGINT')  Quantity,
         a.b.value('StoreID[1]','BIGINT')   StoreID
    FROM @I_ShoppingCart.nodes('/ShoppingCart/Items/Item') AS a(b)
  
  UPDATE TBL_ShoppingCartItems
    SET StatusID = ST.ID,
        Quantity = I.Quantity,
        StoreID = I.StoreID
    FROM TBL_ShoppingCartItems           IT
    JOIN @Items                          I    ON IT.ID = I.ID
    LEFT JOIN SYS_ShoppingCartActions    ACT  ON ACT.ID = @I_ActionID
    LEFT JOIN SYS_ShoppingCartItemStates ST   ON ST.Code = CASE WHEN ACT.Code = 'CAN'
                                                                  THEN 'CAN'
                                                                WHEN ACT.Code = 'OPEN'
                                                                  THEN 'OPEN'
                                                                WHEN ACT.Code = 'FIN'
                                                                  THEN CASE WHEN IT.Quantity = 0
                                                                         THEN 'OUT'
                                                                         ELSE 'CL'
                                                                       END
                                                           END
  
  SET @O_R = @O_R + @@rowcount;

  UPDATE TBL_ShoppingCart
    SET StatusID = ACT.StatusID
    FROM TBL_ShoppingCart             C
    LEFT JOIN SYS_ShoppingCartActions ACT ON ACT.ID = @I_ActionID
    WHERE C.ID = @I_ShoppingCartID

  SET @O_R = @O_R + @@rowcount;

END

GO
CREATE PROCEDURE SP_StoreListSelect
  @O_R              BIGINT OUTPUT,
  @I_Context        XML
AS
BEGIN

  SELECT ST.*
    FROM SYS_ShoppingCartStores ST
    WHERE ST.FlagEnabled = 1

END
GO

--#############################################################################################--
--######################################  INSERTS    ##########################################--
--#############################################################################################--


GO
INSERT SYS_ShoppingCartItemCategories (Code,
                                       Description,
                                       FlagEnabled)
  SELECT DT.Code          Code,
         DT.Description   Description,
         1                FlagEnabled
    FROM (SELECT 'MERC'   Code, 'Mercearia' Description UNION ALL
          SELECT 'BEB'    Code, 'Bebidas'   Description UNION ALL
          SELECT 'FRESC'  Code, 'Frescos'   Description UNION ALL
          SELECT 'HIG'    Code, 'Higiene'   Description UNION ALL
          SELECT 'LIMP'   Code, 'Limpeza'   Description UNION ALL
          SELECT 'CONG'   Code, 'Limpeza'   Description UNION ALL
          SELECT 'LACT'   Code, 'Limpeza'   Description UNION ALL
          SELECT 'BEBE'   Code, 'Limpeza'   Description
         )                                    DT
    LEFT JOIN SYS_ShoppingCartItemCategories  IC ON DT.Code = IC.Code
    WHERE IC.ID IS NULL

INSERT SYS_ShoppingCartStores (Code,
                               Description,
                               FlagEnabled)
  SELECT DT.Code          Code,
         DT.Description   Description,
         1                FlagEnabled
    FROM (SELECT 'PDSH' Code,'Pingo Doce - Senhora da Hora' Description UNION ALL
          SELECT 'PDBOAV' Code,'Pingo Doce - Boavista' Description UNION ALL
          SELECT 'CNTSH' Code,'Continente - Senhora da Hora' Description UNION ALL
          SELECT 'CNTRM' Code,'Continente - Ramalde' Description UNION ALL
          SELECT 'JMBMS' Code,'Jumbo - Mar Shopping' Description UNION ALL
          SELECT 'JMBAS' Code,'Jumbo - Arrábida Shopping' Description UNION ALL
          SELECT 'AFSH' Code,'Alma da Fruta - Senhora da Hora' Description
         )                            DT
    LEFT JOIN SYS_ShoppingCartStores  ST ON DT.Code = ST.Code
    WHERE ST.ID IS NULL

INSERT SYS_ShoppingCartItemUnits (Code,
                                  Description,
                                  FlagEnabled)
  SELECT DT.Code          Code,
         DT.Description   Description,
         1                FlagEnabled
    FROM (SELECT 'UNIT' Code, 'Unidade'   Description UNION ALL
          SELECT 'PES'  Code, 'Peso (KG)' Description
         )                              DT
    LEFT JOIN SYS_ShoppingCartItemUnits U  ON DT.Code = U.Code
    WHERE U.ID IS NULL

INSERT SYS_ShoppingCartItemLocations (Code,
                                      Description,
                                      [Order],
                                      FlagEnabled)
  SELECT DT.Code          Code,
         DT.Description   Description,
         DT.[Order]       'Order',
         1                FlagEnabled
    FROM (SELECT 'DISP'     Code, 1 [Order], 'Dispensa'                Description UNION ALL
          SELECT 'LAV'      Code, 2 [Order], 'Lavandaria'              Description UNION ALL
          SELECT 'COZMOV'   Code, 3 [Order], 'Cozinha - móveis'        Description UNION ALL
          SELECT 'COZFRIG'  Code, 4 [Order], 'Cozinha - frigorífico'   Description UNION ALL
          SELECT 'SALAP'    Code, 5 [Order], 'Sala - aparador'         Description UNION ALL
          SELECT 'DISPCOR'  Code, 6 [Order], 'Dispensa (corredor)'     Description UNION ALL
          SELECT 'WCINT'    Code, 7 [Order], 'WC (intermédio)'         Description UNION ALL
          SELECT 'WCSUI'    Code, 8 [Order], 'WC (suite)'              Description
         )                                  DT
    LEFT JOIN SYS_ShoppingCartItemLocations L  ON DT.Code = L.Code
    WHERE L.ID IS NULL

INSERT SYS_UsersTypes (Code,
                       Description)
  SELECT DT.Code,
         DT.Description
    FROM (SELECT 'ADMIN'  Code, 'Administrador' Description UNION ALL
          SELECT 'SHOP'   Code, 'Shopper'       Description UNION ALL
          SELECT 'VIEW'   Code, 'Viewer'        Description
         )                    DT
    LEFT JOIN SYS_UsersTypes  UT ON DT.Code = UT.Code
    WHERE UT.ID IS NULL

INSERT SYS_ShoppingCartStates (Code,
                               Description)
  SELECT DT.Code,
         DT.Description
    FROM (SELECT 'OPEN' Code, 'Aberta'      Description UNION ALL
          SELECT 'FIN'  Code, 'Finalizada'  Description UNION ALL
          SELECT 'CAN'  Code, 'Cancelada'   Description
         )                            DT
    LEFT JOIN SYS_ShoppingCartStates  CS ON DT.Code = CS.Code

INSERT SYS_ShoppingCartItemStates (Code,
                                   Description)
  SELECT DT.Code,
         DT.Description
    FROM (SELECT 'OPEN' Code, 'Aberto'           Description UNION ALL
          SELECT 'CL'   Code, 'Fechado'          Description UNION ALL
          SELECT 'CAN'  Code, 'Cancelado'        Description UNION ALL
          SELECT 'OUT'  Code, 'Fora da lista'    Description
         )                                DT
    LEFT JOIN SYS_ShoppingCartItemStates  ST ON DT.Code = ST.Code

INSERT SYS_ShoppingCartItems (CategoryID,
                              Code,
                              Description,
                              LocationID,
                              UnitTypeID,
                              UnitPrice,
                              DefaultStoreID,
                              DefaultQuantity,
                              FlagEnabled)
  SELECT CAT.ID                   CategoryID,
         DT.Code                  Code,
         DT.Description           Description,
         LOC.ID                   LocationID,
         UN.ID                    UnitTypeID,
         DT.UnitPrice             UnitPrice,
         ST.ID                    DefaultStoreID,
         DT.DefaultQuantity       DefaultQuantity,
         1                        FlagEnabled
    FROM (SELECT 'LACT' Category, 'LMAG'  Code, 'Leite Magro'                 Description, 'DISP' Location, 'UNIT' Unit, 0.82 UnitPrice, 'CNTSH' DefaultStore, 18 DefaultQuantity UNION ALL
          SELECT 'LACT' Category, 'LMG'   Code, 'Leite Meio-Gordo'            Description, 'DISP' Location, 'UNIT' Unit, 0.62 UnitPrice, 'CNTSH' DefaultStore, 18 DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, 'AG5L'  Code, 'Água >= 5L'                  Description, 'DISP' Location, 'UNIT' Unit, 1.25 UnitPrice, 'CNTSH' DefaultStore, 6  DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, 'AGMAR' Code, 'Água Peq. Mariana (Pack 6)'  Description, 'DISP' Location, 'UNIT' Unit, 1.69 UnitPrice, 'CNTSH' DefaultStore, 2  DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, 'AGNOS' Code, 'Água Peq. 0.5L (Pack 6)'     Description, 'DISP' Location, 'UNIT' Unit, 1.69 UnitPrice, 'CNTSH' DefaultStore, 1  DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, 'COCA'  Code, 'Coca-Cola 1L (Pack 4)'       Description, 'DISP' Location, 'UNIT' Unit, 3.99 UnitPrice, 'CNTSH' DefaultStore, 1  DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, '7UP'   Code, 'Seven up 1.5/2L'             Description, 'DISP' Location, 'UNIT' Unit, 1.09 UnitPrice, 'CNTSH' DefaultStore, 1  DefaultQuantity UNION ALL
          SELECT 'BEB'  Category, '7UP'   Code, 'Seven up 1.5/2L'             Description, 'DISP' Location, 'UNIT' Unit, 1.09 UnitPrice, 'CNTSH' DefaultStore, 1  DefaultQuantity
         )                                    DT
    LEFT JOIN SYS_ShoppingCartItems           CI  ON DT.Code = CI.Code
    LEFT JOIN SYS_ShoppingCartItemCategories  CAT ON DT.Category = CAT.Code
    LEFT JOIN SYS_ShoppingCartItemLocations   LOC ON DT.Location = LOC.Code
    LEFT JOIN SYS_ShoppingCartItemUnits       UN  ON DT.Unit = UN.Code
    LEFT JOIN SYS_ShoppingCartStores          ST  ON DT.DefaultStore = ST.Code
    WHERE CI.ID IS NULL
    
INSERT SYS_ShoppingCartActions (Code,
                                Description,
                                StatusID)
  SELECT DT.Code          Code,
         DT.Description   Description,
         ST.ID            StatusID
    FROM (SELECT 'FIN'  Code, 'Finalizar' Description, 'FIN'  ShoppingCartState UNION ALL
          SELECT 'CAN'  Code, 'Cancelar'  Description, 'CAN'  ShoppingCartState UNION ALL
          SELECT 'REOP' Code, 'Reabrir'   Description, 'OPEN' ShoppingCartState
         )                                DT
    LEFT JOIN SYS_ShoppingCartStates      ST  ON DT.ShoppingCartState = ST.Code
    LEFT JOIN SYS_ShoppingCartActions     ACT ON DT.Code = ACT.Code

INSERT TBL_Users (TypeID,
                  Username,
                  Name,
                  Email,
                  FlagEnabled)
  SELECT UT.ID        TypeID,
         DT.Username  Username,
         DT.Name      Name,
         DT.Email     Email,
         1            FlagEnabled
    FROM (SELECT 'ADMIN' UserType, 'admin' Username, 'Administrator' Name, 'admin@company.com' Email
         )              DT
    LEFT JOIN TBL_Users U ON DT.Username = U.Username
    LEFT JOIN SYS_UsersTypes UT ON DT.UserType = UT.Code
    WHERE U.ID IS NULL