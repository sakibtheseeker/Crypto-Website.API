create database crypto;

-- User Table
create table Users (
uid int primary key identity,
uname varchar(50) not null,
uemail varchar(50) not null unique,
upassword varchar(255) not null ,
createdAt datetime  not null default sysdatetime(),
createdBy int not null,
updatedAt datetime , 
updatedBy int,
deletedAt datetime ,
deletedBy int,
isActive bit not null default 1);


-- Crypto Table
create table Crypto(
cid int primary key identity,
cname varchar(25) not null,
cphoto varbinary(max),     -- neef to change add url
currentprice decimal(12,2) not null,
createdAt datetime  not null default sysdatetime(),
createdBy int,
updatedAt datetime , 
updatedBy int,
deletedAt datetime ,
deletedBy int,
isActive bit not null default 1)


-- Watchlist Crypto Table
create table Watchlist(
fid int primary key identity,
uid int not null,
cid int not null,
createdAt datetime  not null default sysdatetime(),
createdBy int,
updatedAt datetime , 
updatedBy int,
deletedAt datetime ,
deletedBy int,
isActive bit not null default 1,

constraint FK_FavCrypto_Users
foreign key (uid) references Users(uid),

constraint FK_FavCrypto_Crypto
foreign key (cid) references Crypto(cid),

constraint UQ_FavCrypto unique (uid, cid)
)


-- Transaction History    -- wallet should get updated whenever there is transaction usse commit
create table TransactionsHistory(
tid int primary key identity,
uid int not null,
cid int not null,
transactionType	varchar(50)	,
price	decimal(18,2),
quantity	decimal(18,2),
createdAt	datetime	not null default sysdatetime(),
createdBy	int	,
updatedAt	datetime,
updatedBy	int,
deletedBy	int,
deletedAt	datetime,	
isActive	bit	not null  default 1,

constraint FK_Transactions_User
foreign key (uid) references Users(uid),

constraint FK_Transactions_Crypto
foreign key (cid) references Crypto(cid),

constraint CK_Transaction_Type
check (transactionType IN ('BUY', 'SELL'))
);


-- Wallet
create table Wallet(
wid int primary key identity,
uid int not null,
currentBal decimal(18,2) default 0,
createdAt	datetime	not null default sysdatetime(),
createdBy	int	,
updatedAt	datetime,
updatedBy	int,
deletedBy	int,
deletedAt	datetime,	
isActive	bit	not null  default 1,

constraint FK_Wallet_User
foreign key (uid) references Users(uid),

constraint UQ_Wallet_User
unique (uid));


-- WalletTransactions
create table WalletTransactions
(
wtid int primary key identity,
wid int not null,
amount decimal(18,2) not null ,
transactionType varchar(10) not null, 
paymentMethod varchar(20) not null,
paymentType varchar(20) not null,
createdAt	datetime	not null default sysdatetime(),
createdBy	int	,
updatedAt	datetime,
updatedBy	int,
deletedBy	int,
deletedAt	datetime,	
isActive	bit	not null  default 1,

constraint FK_WalletTransactions_Wallet
foreign key (wid) references Wallet(wid),

constraint CK_WalletTransaction_Type
check (transactionType IN ('CREDIT', 'DEBIT'))
);

-- Portfolio
create table Portfolio
(
pid int primary key identity(1,1),
uid int NOT NULL,
cid int NOT NULL,
quantity decimal(18,8) not null default 0,
avgBuyPrice decimal(18,2) not null default 0,
createdAt	datetime	not null default sysdatetime(),
createdBy	int	,
updatedAt	datetime,
updatedBy	int,
deletedBy	int,
deletedAt	datetime,	
isActive	bit	not null  default 1,

CONSTRAINT FK_Portfolio_User
FOREIGN KEY (uid) REFERENCES Users(uid),

CONSTRAINT FK_Portfolio_Crypto
FOREIGN KEY (cid) REFERENCES Crypto(cid),

CONSTRAINT UQ_Portfolio_User_Crypto
UNIQUE (uid, cid)
);


