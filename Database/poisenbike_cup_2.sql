-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.5.9-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Exportiere Datenbank Struktur für poisenbike_cup
CREATE DATABASE IF NOT EXISTS `poisenbike_cup` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_german2_ci */;
USE `poisenbike_cup`;

-- Exportiere Struktur von Tabelle poisenbike_cup.fahrer
CREATE TABLE IF NOT EXISTS `fahrer` (
  `FahrerID` int(10) NOT NULL AUTO_INCREMENT,
  `VName` varchar(50) DEFAULT NULL,
  `NName` varchar(50) DEFAULT NULL,
  `GebDat` date DEFAULT NULL,
  `Plz` varchar(50) DEFAULT NULL,
  `Ort` varchar(50) DEFAULT NULL,
  `Strasse` varchar(50) DEFAULT NULL,
  `Hausnummer` varchar(50) DEFAULT NULL,
  `TeamID` int(11) DEFAULT NULL,
  PRIMARY KEY (`FahrerID`),
  KEY `FK_fahrer_team` (`TeamID`),
  CONSTRAINT `FK_fahrer_team` FOREIGN KEY (`TeamID`) REFERENCES `team` (`TeamID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Prozedur poisenbike_cup.getAlleBestenlistenProWettkampf
DELIMITER //
CREATE PROCEDURE `getAlleBestenlistenProWettkampf`(
	IN `WettkampfID` INT
)
BEGIN
	SELECT * FROM wettkampf_fahrer
	WHERE wettkampf_fahrer.WettkampfID =  WettkampfID;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getBestenliste
DELIMITER //
CREATE PROCEDURE `getBestenliste`(
	IN `Name` VARCHAR(50),
	IN `wettkampfID` INT
)
BEGIN
SELECT *
FROM wettkampf w
INNER JOIN wettkampf_fahrer wf
ON w.WettkampfID = wf.WettkampfID
INNER JOIN fahrer f
ON f.FahrerID = wf.FahrerID
WHERE w.Name = _Name 
	AND w.WettkampfID = wettkampfID;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getFahrer
DELIMITER //
CREATE PROCEDURE `getFahrer`()
BEGIN
	SELECT * FROM fahrer;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getStrecken
DELIMITER //
CREATE PROCEDURE `getStrecken`()
BEGIN
	SELECT * FROM strecke;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.GetTableByName
DELIMITER //
CREATE PROCEDURE `GetTableByName`(
	IN `_Name` VARCHAR(50)
)
BEGIN
	SELECT * FROM `_Name`;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getTables
DELIMITER //
CREATE PROCEDURE `getTables`()
BEGIN
SHOW TABLES from poisenbike_cup;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getTeams
DELIMITER //
CREATE PROCEDURE `getTeams`()
BEGIN
	SELECT * FROM team;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.getWettkaempfe
DELIMITER //
CREATE PROCEDURE `getWettkaempfe`()
BEGIN
	SELECT * FROM wettkampf;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.insertFahrer
DELIMITER //
CREATE PROCEDURE `insertFahrer`(
	IN `VName` VARCHAR(50),
	IN `NName` VARCHAR(50),
	IN `GebDat` DATE,
	IN `Plz` VARCHAR(50),
	IN `Ort` VARCHAR(50),
	IN `Strasse` VARCHAR(50),
	IN `TeamID` INT,
	IN `Hausnummer` VARCHAR(50)
)
BEGIN
	INSERT INTO fahrer (VName, NName, GebDat, Plz, Ort, Strasse, TeamID, Hausnummer) VALUES(VName, NName, GebDat, Plz, Ort, Strasse, TeamID, Hausnummer);
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.insertStrecke
DELIMITER //
CREATE PROCEDURE `insertStrecke`(
	IN `Distanz_KM` FLOAT,
	IN `Höhenmeter` INT,
	IN `Startgeld` DECIMAL(10,0),
	IN `Name` VARCHAR(50)
)
BEGIN
	INSERT INTO strecke(Distanz_KM, `Höhenmeter`, Startgeld, `Name`) VALUES(Distanz_KM, `Höhenmeter`, Startgeld,`Name`);
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.insertTeam
DELIMITER //
CREATE PROCEDURE `insertTeam`(
	IN `Teamname` VARCHAR(50),
	IN `Email` VARCHAR(50),
	IN `Plz` VARCHAR(50),
	IN `Ort` VARCHAR(50),
	IN `Strasse` VARCHAR(50),
	IN `Hausnummer` VARCHAR(50)
)
BEGIN
	INSERT INTO team (Teamname, Email, Plz, Ort, Strasse, Hausnummer) VALUES (Teamname, Email, Plz, Ort, Strasse, Hausnummer);
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.insertWettkampf
DELIMITER //
CREATE PROCEDURE `insertWettkampf`(
	IN `Datum` DATETIME,
	IN `Name` VARCHAR(50),
	IN `StreckenID` INT
)
BEGIN
	INSERT INTO wettkampf(Datum, `NAME`, StreckenID) VALUES(Datum, `NAME`, StreckenID);
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.insertWettkmapf_Fahrer
DELIMITER //
CREATE PROCEDURE `insertWettkmapf_Fahrer`(
	IN `FahrerID` INT,
	IN `WettkampfID` INT,
	IN `Fahrer_Startnummer` INT,
	IN `Zeit` TIME
)
BEGIN
	INSERT INTO wettkampf_fahrer(Fahrer_Startnummer, FahrerID, WettkampfID, Zeit) VALUES(Fahrer_Startnummer, FahrerID, WettkampfID, Zeit);
END//
DELIMITER ;

-- Exportiere Struktur von Tabelle poisenbike_cup.strecke
CREATE TABLE IF NOT EXISTS `strecke` (
  `StreckenID` int(11) NOT NULL AUTO_INCREMENT,
  `Distanz_KM` float DEFAULT NULL,
  `Höhenmeter` int(11) DEFAULT NULL,
  `Startgeld` decimal(10,0) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`StreckenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle poisenbike_cup.team
CREATE TABLE IF NOT EXISTS `team` (
  `TeamID` int(11) NOT NULL AUTO_INCREMENT,
  `Adresse` varchar(50) NOT NULL DEFAULT '',
  `Teamname` varchar(50) NOT NULL DEFAULT '',
  `Email` varchar(50) NOT NULL DEFAULT '',
  `Plz` varchar(50) DEFAULT NULL,
  `Ort` varchar(50) DEFAULT NULL,
  `Strasse` varchar(50) DEFAULT NULL,
  `Hausnummer` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`TeamID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Prozedur poisenbike_cup.tschau
DELIMITER //
CREATE PROCEDURE `tschau`()
BEGIN
	DELETE FROM wettkampf_fahrer;
	DELETE FROM wettkampf;
	DELETE FROM team;
	DELETE FROM fahrer;
	DELETE FROM strecke;
	
	
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur poisenbike_cup.updateErreichteZeit
DELIMITER //
CREATE PROCEDURE `updateErreichteZeit`(
	IN `WfID` INT,
	IN `Zeit` TIME
)
BEGIN
	UPDATE wettkampf_fahrer
	SET  wettkampf_fahrer.Zeit = Zeit
	WHERE wettkampf_fahrer.WfID = WfID;
END//
DELIMITER ;

-- Exportiere Struktur von Tabelle poisenbike_cup.wettkampf
CREATE TABLE IF NOT EXISTS `wettkampf` (
  `WettkampfID` int(11) NOT NULL AUTO_INCREMENT,
  `Datum` datetime DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `StreckenID` int(11) DEFAULT NULL,
  PRIMARY KEY (`WettkampfID`),
  KEY `FK_wettkampf_strecke` (`StreckenID`),
  CONSTRAINT `FK_wettkampf_strecke` FOREIGN KEY (`StreckenID`) REFERENCES `strecke` (`StreckenID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle poisenbike_cup.wettkampf_fahrer
CREATE TABLE IF NOT EXISTS `wettkampf_fahrer` (
  `WfID` int(11) NOT NULL AUTO_INCREMENT,
  `WettkampfID` int(11) DEFAULT NULL,
  `FahrerID` int(11) DEFAULT NULL,
  `Fahrer_Startnummer` int(11) DEFAULT NULL,
  `Zeit` time DEFAULT NULL,
  PRIMARY KEY (`WfID`),
  KEY `FK__fahrer` (`FahrerID`),
  KEY `FK__wettkampf` (`WettkampfID`),
  CONSTRAINT `FK__fahrer` FOREIGN KEY (`FahrerID`) REFERENCES `fahrer` (`FahrerID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK__wettkampf` FOREIGN KEY (`WettkampfID`) REFERENCES `wettkampf` (`WettkampfID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
