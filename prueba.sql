create schema Invoices;
use invoices;
create table Clients(
Id int not null primary key,
Namei varchar(80),
Last_Name varchar(80),
Document_Id int
);
create table Invoice(
Id int not null primary key,
Id_Client int,
Cod varchar(80),
foreign key (Id_Client) references Clients(Id)
);
create table InvoicesDetails(
Id int not null primary key,
Id_Invoice int,
Descriptioni varchar(80),
Valuei float,
foreign key (Id_Invoice) references Invoice(Id)
);