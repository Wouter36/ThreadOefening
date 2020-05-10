use boeki;

create table WoodRecords(

recordID int IDENTITY(1,1) NOT NULL  PRIMARY KEY,
woodID int NOT NULL,
treeID int NOT NULL,
x int NOT NULL,
y int NOT NULL,

);

create table MonkeyRecords(

recordID int IDENTITY(1,1) NOT NULL  PRIMARY KEY,
monkeyID int NOT NULL,
woodID int NOT NULL,
seqnr int NOT NULL,
treeID int NOT NULL,
x int NOT NULL,
y int NOT NULL,

);

create table Logs(

Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
monkeyID int NOT NULL,
woodID int NOT NULL,
message varchar(255) NOT NULL,

);

--drop table MonkeyRecords;
--drop table Logs;
--drop table WoodRecords;