--create_database.sql
--Run drop_database.sql to delete this database

CREATE DATABASE MessageStore;
USE MessageStore;

CREATE TABLE Message (
    MessageId INT(4) NOT NULL AUTO_INCREMENT, 
    Name VARCHAR(32) NOT NULL,
    Comments VARCHAR(255) NOT NULL,
    LoggedDate DATE NOT NULL,
    PRIMARY KEY(MessageId)
);

GRANT ALL ON MessageStore.* TO 'Chapter' IDENTIFIED BY 'Six';