-- SP FOR Users

create proc FetchAllUsers
as
begin
select * from Users;
end

exec  FetchAllUsers;

create proc DeleteUser
@id int
as
 begin
  delete from Users where uid=@id;
 end

 create proc InsertUser
 @uname varchar(50),
 @uemail varchar(50),
 @upasssword varchar(255)
 as
 begin
 insert into Users(uname,uemail,upassword) values (@uname,@uemail,@upasssword);
 end

 exec InsertUser 'Sakib','sakib@gmail.com','12346';



-- SP FOR Crypto

create proc FetchAllCryptos
as 
begin
select * from Crypto;
end

exec FetchAllCryptos;

create proc DeleteCrypto
@id int
as
 begin
  delete from Crypto where cid=@id;
 end

create proc InsertCrypto
 @cname varchar(50),
 @currentprice varchar(50)
 as
 begin
 insert into crypto(cname,currentprice) values (@cname,@currentprice);
 end

 exec InsertCrypto 'Bitcoin','sakib@gmail.com','12346';



-- SP FOR FavouriteCrypto


create proc FetchWatchlist
as
begin
select * from Watchlist;
end

exec FetchWatchlist;

create proc DeleteWatchlist
@id int
as
 begin
  delete from Watchlist where fid=@id;
 end


-- SP FOR TransactionsHistory

create proc FetchTransactionsHistory
as
begin
select * from TransactionsHistory;
end

exec FetchTransactionsHistory;

create proc DeleteTransactionsHistory
@id int
as
 begin
  delete from TransactionsHistory where tid=@id;
 end



-- SP FOR Wallet

create proc FetchAllWallets
as
begin
select * from Wallet;
end

exec FetchAllWallets;

create proc DeleteWallet
@id int
as
 begin
  delete from Wallet where wid=@id;
 end


-- SP FOR WalletTransaction

create proc FetchAllWalletTransactions
as
begin
select * from WalletTransactions;
end

exec FetchAllWalletTransactions;


create proc DeleteWalletTransactions
@id int
as
 begin
  delete from WalletTransactions where wtid=@id;
 end


-- SP FOR PORTFOLIO

create proc FetchAllPortfolio
as
begin
select * from Portfolio;
end

create proc DeletePortfolio
@id int
as
 begin
  delete from Portfolio where pid=@id;
 end




-- ***********************************  TRIGGERS ***********************************

-- General Triggers


create trigger AfterTableCreate
on database
for create_table
as
begin
 print 'Table Created Successfully in Database'
end



create trigger AfterTableDrop
on database
for drop_table
as
begin
 print 'Table Dropped Successfully from Database'
end

create trigger AfterTableRename
on database
for alter_table
as
begin
 print 'Table Renamed Successfully'
end




-- Triggers for Users
select * from Users;


create trigger AfterUserDelete
on Users
after delete
as
begin
 print 'User Deleted Successfully in User table'
end


-- Triggers for Crypto

create trigger AfterCryptoInsert
on Crypto
after insert
as
begin
 print 'Crypto Inserted Successfully in Crypto table'
end


-- Triggers for FavCryto

create trigger AfterWishlistInsert
on Watchlist
after insert
as
begin
 print 'Watchlist Inserted Successfully in Fav Crypto table'
end

-- Triggers for TransactionHistory
create trigger AfterTransactionHistoryInsert
on TransactionsHistory
after insert
as
begin
 print 'Transaction History Inserted Successfully in TransactionHistory table'
end

-- Triggers for Wallet
create trigger AfterWalletInsert
on Wallet
after insert
as
begin
 print 'Wallet details Inserted Successfully in Wallet table'
end

-- Triggers for WalletTransactions
create trigger AfterWalletTransactionsInsert
on  WalletTransactions
after insert
as
begin
 print ' WalletTransactions details Inserted Successfully in  WalletTransactions table'
end





