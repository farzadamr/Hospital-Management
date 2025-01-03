CREATE TABLE PERSON(
NATIONALCODE INT PRIMARY KEY,
FIRSTNAME CHAR(50) NOT NULL,
LASTNAME CHAR(50) NOT NULL,
PASSWORD NVARCHAR(20));
GO

CREATE TABLE TEL(
NATIONALCODE INT,
TEL CHAR(14),
PRIMARY KEY (NATIONALCODE,TEL),
FOREIGN KEY (NATIONALCODE) REFERENCES PERSON(NATIONALCODE)
);
GO

CREATE TABLE ADMINN(
ID INT IDENTITY(1,1) PRIMARY KEY,
USERNAME NVARCHAR(20) NOT NULL,
NATIONALCODE INT,
FOREIGN KEY (NATIONALCODE) REFERENCES PERSON(NATIONALCODE)
);
GO

CREATE TABLE DEPARTMENT(
ID INT IDENTITY(1,1) PRIMARY KEY,
TITLE CHAR(20) NOT NULL,
DESCRIPTION_ NVARCHAR(MAX)
);
GO

CREATE TABLE DOCTOR(
M_S_N INT PRIMARY KEY,
RATE TINYINT,
RESUME_ CHAR(200),
NATIONALCODE INT,
DEPARTMENTID INT,
FOREIGN KEY (NATIONALCODE) REFERENCES PERSON(NATIONALCODE),
FOREIGN KEY (DEPARTMENTID) REFERENCES DEPARTMENT(ID)
);
GO

CREATE TABLE PATIENT(
ID INT IDENTITY(1,1) PRIMARY KEY,
GENDER CHAR(1),
BLOOD_TYPE CHAR(3),
BIRTH_DATE DATE,
NATIONALCODE INT,
FOREIGN KEY (NATIONALCODE) REFERENCES PERSON(NATIONALCODE)
);
GO

CREATE TABLE ADDRES(
ID INT IDENTITY(1,1) PRIMARY KEY,
POSTALCODE INT NOT NULL,
PLAK INT NOT NULL,
STREET CHAR(50),
PATIENTID INT,
FOREIGN KEY (PATIENTID) REFERENCES PATIENT(ID)
);
GO

CREATE TABLE PRESCRIOTION(
ID INT IDENTITY(1,1) PRIMARY KEY,
TIME_ DATETIME NOT NULL,
STATUS_ CHAR(50),
PATIENTID INT,
DOCTORID INT,
FOREIGN KEY (PATIENTID) REFERENCES PATIENT(ID),
FOREIGN KEY (DOCTORID) REFERENCES DOCTOR(M_S_N)
);
GO

CREATE TABLE APPOINTMENT(
ID INT IDENTITY(1,1) PRIMARY KEY,
DESCRIPTIONN CHAR(200),
TIME_ DATE NOT NULL,
DOCTORID INT,
FOREIGN KEY (DOCTORID) REFERENCES DOCTOR(M_S_N)
);
GO

CREATE TABLE VISIT(
STATUS_ CHAR(200),
PATIENTID INT,
APPOINTMENTID INT,
PRESCRIOTIONID INT,
PRIMARY KEY (PATIENTID,APPOINTMENTID),
FOREIGN KEY (PATIENTID) REFERENCES PATIENT(ID),
FOREIGN KEY (APPOINTMENTID) REFERENCES APPOINTMENT(ID),
FOREIGN KEY (PRESCRIOTIONID) REFERENCES PRESCRIOTION(ID)
);
GO