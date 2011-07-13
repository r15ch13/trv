/*Tabellen
-----------
Stoerung
Massnahme
Lehrer
Schueler
Klasse
Schuelerklasse
Lehrerklasse
Ueberweisung

Elternbrief
Aufenthalt
*/

--CREATE DATABASE TRDB

--Tabelle Stoerung
CREATE TABLE Stoerung
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Beschreibung nvarchar(250) NOT NULL,
	Aktiv bit DEFAULT 1,
	Custom bit DEFAULT 0
/*
Aktive sind im Programm wählbar wenn
Custom 0 ist; Custom = Eigene Beschreibung
vom Überweisungsschein
*/
)

CREATE TABLE Massnahme
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Stufe int, --(Ausmaß der Massnahme, beim 1-sten, 3-ten, 5-ten Besuch des Schülers)
	Beschreibung nvarchar(500),
	Textbaustein nvarchar(4000) --(RTF Textbaustein mit Parametern zum erstellen von Elternbriefen)
)

CREATE TABLE Lehrer
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Vorname nvarchar(100),
	Nachname nvarchar(100),
	Kuerzel nvarchar(10) NOT NULL
)

CREATE TABLE Schueler
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Vorname nvarchar(100),
	Nachname nvarchar(100)
)

CREATE TABLE Klasse
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Bezeichnung nvarchar(100)
)

CREATE TABLE Lehrerklasse
(
	LehrerID int REFERENCES Lehrer(ID),
	KlasseID int REFERENCES Klasse(ID),
	Datum datetime
)

CREATE TABLE Schuelerklasse
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	SchuelerID int REFERENCES Schueler(ID),
	KlasseID int REFERENCES Klasse(ID),
	Aktiv int NOT NULL DEFAULT 1,
	Datum datetime
)

CREATE TABLE Ueberweisung
(
	UeberweisungNr int PRIMARY KEY,
	SchuelerID int REFERENCES Schueler(ID),
	Vorname nvarchar(100) NOT NULL,
	Nachname nvarchar(100) NOT NULL,
	Klasse nvarchar(100), --Bezeichnung der Klasse
	Fach nvarchar(50),
	[Std] int,
	Datum datetime NOT NULL,
	Abmahnritual bit DEFAULT 1, --Abmahnritual eingehalten ja/nein
	StoerungID int REFERENCES Stoerung(ID),
	LehrerID int REFERENCES Lehrer(ID),
	Ritualgrund nvarchar(4000), --Grund fuer nicht-einhaltung des Abmahnrituals
	SonstBemerkung nvarchar(4000) --Freies Feld fuer zusaetzliche Bemerkungen
)

CREATE TABLE Elternbrief
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	SchuelerID int REFERENCES Schueler(ID),
	MassnahmeID int REFERENCES Massnahme(ID),
	Versanddatum datetime,
	Gespraechstermin datetime --Vorladung zum Gespraech am Datum um Uhrzeit
);

CREATE TABLE Aufenthalt
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	SchuelerID int REFERENCES Schueler(ID),
	KlasseID int REFERENCES Klasse(ID),
	ULehrerID int REFERENCES Lehrer(ID), --ID des ueberweisenden Lehrers
	TRLehrerID int REFERENCES Lehrer(ID), --ID des beaufsichtigenden Lehrers
	UNr int REFERENCES Ueberweisung(UeberweisungNr),
	Ankunft datetime NOT NULL, --Ankunftszeit des Schuelers
	Verlassen datetime
);

CREATE VIEW test AS
(
	SELECT * FROM Elternbrief
)