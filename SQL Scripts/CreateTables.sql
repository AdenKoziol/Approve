CREATE TABLE tblEmployee (
	ID INT,
	Name VARCHAR(50),
	Email VARCHAR(50),
	TeamID INT,
	Username VARCHAR(50),
	Password VARCHAR(50),
	PRIMARY KEY(ID)
)

CREATE TABLE tblTeam (
	ID INT,
	NAME VARCHAR(50),
	PRIMARY KEY(ID)
)

CREATE TABLE tblMachine (
	ID INT,
	NAME VARCHAR(50),
	PRIMARY KEY(ID)
)

CREATE TABLE tblRequest (
	ID INT,
	MachineID INT,
	Description VARCHAR(500),
	PosterID INT,
	DatePosted DateTime,
	Team1ID INT,
	Team2ID INT,
	Team3ID INT,
	Team4ID INT,
	Team5ID INT,
	IsCompleted INT,
	PRIMARY KEY(ID)
)

CREATE TABLE tblEmail (
	ID INT,
	RequestID INT,
	TeamID INT,
	EmployeeID INT,
	IsApproved INT,
	PRIMARY KEY(ID)
)