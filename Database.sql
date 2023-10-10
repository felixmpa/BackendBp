create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go

INSERT INTO BackendBp.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20231009225621_InitialCreateForBpClient', N'7.0.11');
INSERT INTO BackendBp.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20231010030655_InitialCreateForBpTransaction', N'7.0.11');

create table Person
(
    Id             int identity
        constraint PK_Person
            primary key,
    Identification nvarchar(max) not null,
    Name           nvarchar(max) not null,
    Sex            nvarchar(50)  not null,
    Age            int,
    Address        nvarchar(max),
    Phone          nvarchar(max)
)
go

INSERT INTO BackendBp.dbo.Person (Identification, Name, Sex, Age, Address, Phone) VALUES (N'0001230001', N'Jose Lema Edited', N'Male', 19, N'Otavalo sn y principal #123', N'098254799');
INSERT INTO BackendBp.dbo.Person (Identification, Name, Sex, Age, Address, Phone) VALUES (N'0004560002', N'Marianela Montalvo', N'Female', 25, N'Amazonas y NNUU', N'098254785');
INSERT INTO BackendBp.dbo.Person (Identification, Name, Sex, Age, Address, Phone) VALUES (N'0007890002', N'Juan Osorio', N'Male', 35, N'13 junio y Equinoccial', N'098874587');

create table Customers
(
    Id       int identity
        constraint PK_Customers
            primary key,
    PersonId int           not null
        constraint FK_Customers_Person_PersonId
            references Person
            on delete cascade,
    Password nvarchar(max) not null,
    Status   int           not null
)
go

create unique index IX_Customers_PersonId
    on Customers (PersonId)
go

INSERT INTO BackendBp.dbo.Customers (PersonId, Password, Status) VALUES (1, N'12346789', 1);
INSERT INTO BackendBp.dbo.Customers (PersonId, Password, Status) VALUES (2, N'5678', 1);
INSERT INTO BackendBp.dbo.Customers (PersonId, Password, Status) VALUES (3, N'1245', 1);

create table Accounts
(
    Id               int identity
        constraint PK_Accounts
            primary key,
    CustomerId       int            not null,
    AccountNumber    nvarchar(12)   not null,
    AccountType      int            not null,
    InitialBalance   decimal(18, 2) not null,
    AvailableBalance decimal(18, 2) not null,
    Status           int            not null
)
go

INSERT INTO BackendBp.dbo.Accounts (CustomerId, AccountNumber, AccountType, InitialBalance, AvailableBalance, Status) VALUES (1, N'478758', 1, 2000.00, 1425.00, 1);
INSERT INTO BackendBp.dbo.Accounts (CustomerId, AccountNumber, AccountType, InitialBalance, AvailableBalance, Status) VALUES (2, N'225487', 4, 100.00, 700.00, 1);
INSERT INTO BackendBp.dbo.Accounts (CustomerId, AccountNumber, AccountType, InitialBalance, AvailableBalance, Status) VALUES (3, N'495878', 1, 0.00, 150.00, 1);
INSERT INTO BackendBp.dbo.Accounts (CustomerId, AccountNumber, AccountType, InitialBalance, AvailableBalance, Status) VALUES (2, N'496825', 1, 540.00, 0.00, 1);
INSERT INTO BackendBp.dbo.Accounts (CustomerId, AccountNumber, AccountType, InitialBalance, AvailableBalance, Status) VALUES (3, N'585545', 4, 1000.00, 1000.00, 1);

create table TransactionBalance
(
    Id         int identity
        constraint PK_TransactionBalance
            primary key,
    CustomerId int            not null,
    AccountId  int            not null
        constraint FK_TransactionBalance_Accounts_AccountId
            references Accounts
            on delete cascade,
    Date       datetime2      not null,
    Type       int            not null,
    Amount     decimal(18, 2) not null,
    Balance    decimal(18, 2) not null
)
go

create index IX_TransactionBalance_AccountId
    on TransactionBalance (AccountId)
go

INSERT INTO BackendBp.dbo.TransactionBalance (CustomerId, AccountId, Date, Type, Amount, Balance) VALUES (1, 1, N'2023-10-10 10:30:49.1851972', 2, -575.00, 1425.00);
INSERT INTO BackendBp.dbo.TransactionBalance (CustomerId, AccountId, Date, Type, Amount, Balance) VALUES (2, 2, N'2023-10-10 10:31:32.3214199', 1, 600.00, 700.00);
INSERT INTO BackendBp.dbo.TransactionBalance (CustomerId, AccountId, Date, Type, Amount, Balance) VALUES (3, 3, N'2023-10-10 10:31:53.8089583', 1, 150.00, 150.00);
INSERT INTO BackendBp.dbo.TransactionBalance (CustomerId, AccountId, Date, Type, Amount, Balance) VALUES (2, 4, N'2023-10-10 10:32:10.1485431', 2, -540.00, 0.00);
