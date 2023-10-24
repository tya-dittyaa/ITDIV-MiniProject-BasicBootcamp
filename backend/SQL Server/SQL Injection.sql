CREATE DATABASE WearHouse

USE WearHouse

CREATE TABLE MsUser (
	UserId CHAR(36) PRIMARY KEY DEFAULT(NEWID()),
	Name VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL
)

CREATE TABLE MsCategory (
	CategoryId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
)

INSERT INTO MsUser (Name, Email, Password)
VALUES
	('Andi Budiman', 'andi.budiman@gmail.com', 'password123'),
	('Rina Wijaya', 'rina.wijaya@yahoo.com', 'password456'),
	('Budi Hartono', 'budi.hartono@outlook.com', 'password789');

INSERT INTO MsCategory (Name)
VALUES
	('Pants'),
	('Clothes'),
	('Underwear'),
	('Shoes'),
	('Hats')