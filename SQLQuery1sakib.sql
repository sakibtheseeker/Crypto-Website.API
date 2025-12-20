-- 11/12/2025
create table emp(id int primary key identity,name varchar(50),salary decimal(10,2));
truncate table emp;
truncate table emplogs;
create table emp1(id int primary key identity,name varchar(50),salary decimal(10,2), netsalary decimal(10,2));

create table emplogs(id int primary key identity,name varchar(50),salary decimal(10,2), CreatedBy varchar(50),CreatedAt varchar(50));


-- Procedure - Fetch All Emp 
create proc FetchAllEmp
as
begin
 select * from emp;
end

exec FetchAllEmp;

-- Procedure - Execution of multiple queries in a single procedure
create proc SaveFetchDeleteEmp
@Id int=null,
@Salary decimal (10,2)=null,
@Name varchar(100)=null,
@Choice int
as
begin
 if(@Choice=1)
 begin
  insert into emp values(@Name,@Salary);
 end
 else if(@Choice=2)
 begin 
  select * from emp;
 end
 else
 begin
  delete from emp where id=@Id;
 end
end

exec SaveFetchDeleteEmp @Choice=2;
exec SaveFetchDeleteEmp @Id=1,@Choice=1;


-- Trigger -After insert trigger
create trigger AfterInsert
on emp
after insert
as
begin
 print 'Hello Employee details added successfully';
end

-- Trigger -After insert trigger
create trigger AfterInsert2
on emp
after insert
as
begin
 insert into emplogs(name,salary,CreatedBy,CreatedAt)
 select name,salary,'Masstech',getdate() from inserted;
end


-- 12/12/2025

-- Trigger -After update trigger  
select * from emplogs;
select * from emp;
alter table emplogs add  ModifyBy varchar(50),ModifyAt varchar(50);



alter trigger AfterUpdate
on emp
after update
as
begin
 declare @name varchar(100)
 declare @id int
 declare @salary decimal(10,2)
 select @name=name,@salary=salary ,@id=id from inserted;
 update emplogs set name=@name , salary=@salary,ModifyBy='Sakib',ModifyAt=getdate() where id=@id;
end


insert into emp (name,salary) values ('Sakib',10000);
update emp set name='Sakib 11' where id=4;
update emp set salary=18000 where id=1;



alter table emplogs add  DeletedBy varchar(50),DeletedAt varchar(50);


alter trigger AfterDelete
on emp
after delete
as
begin
 declare @id int
 select @id=id from deleted;
 update emplogs set DeletedBy='Sakib5',DeletedAt=getdate() where id=@id;
end


delete from emp where id=4;



-- before insert  --------------------------------------------------------------------------

alter trigger SalaryValidation
on emp
instead of insert
as
begin
 declare @name varchar(50)
 declare @salary decimal(10,2)
  select @name=name,@salary=salary from inserted;
 if(@salary<10000)
  begin
   print 'Salary is less'
  end
 else
 begin
   print'Data inserted successfully';
   insert into emp values(@name ,@salary);
 end
end


insert into emp (name,salary) values ('Sakib101',29000);

select * from emplogs;
select * from emp;


alter trigger aftercreate
on database
for create_table
as 
begin
 print 'Hello Table created successfully ';
end

create trigger afterdrop
on database
for drop_table
as 
begin
 print 'Hello Table dropped successfully ';
end

create trigger afterrename
on database
for rename_table
as 
begin
 print 'Hello Table renamed successfully ';
end



drop table mass;
create table mass(id int,name varchar(50));

EXEC sp_rename 'mass', 'Masstech';
