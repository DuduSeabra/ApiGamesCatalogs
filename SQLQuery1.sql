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

create table OrdersItem(
Id uniqueidentifier primary key,
OrderId uniqueidentifier,
GameId uniqueidentifier
);

alter table Orders
add constraint FK_Username
foreign key (Username) references Clients(Username);

alter table OrdersItem
add constraint FK_OrderId
foreign key (OrderId) references Orders(Id);

alter table OrdersItem
add constraint FK_GameId
foreign key (GameId) references Games(Id);