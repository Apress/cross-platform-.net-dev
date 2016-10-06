--create_database.sql
--Run drop_database.sql to delete this database

CREATE DATABASE ApplicationStore;
USE ApplicationStore;

CREATE TABLE DataKeyValue (
    ApplicationName VARCHAR(32) NOT NULL, 
    DataKey VARCHAR(32) NOT NULL,
    DataValue VARCHAR(32), 
    PRIMARY KEY(ApplicationName, DataKey)
);

GRANT ALL ON ApplicationStore.* TO 'Chapter6' IDENTIFIED BY 'LastRun';