use GamesCatalogs;



create table Games(
Id uniqueidentifier primary key,
Name varchar,
Producer varchar,
Price float
);

create table Clients(
Username varchar primary key,
Password varchar
);

create table Orders(
Id uniqueidentifier primary key,
Username varchar
);

alter table Orders
add constraint FK_Username
foreign key (Username) references Clients(Username);