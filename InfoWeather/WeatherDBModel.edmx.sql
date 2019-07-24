
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/19/2019 06:53:19
-- Generated from EDMX file: C:\Users\danis\source\repos\InfoWeather\InfoWeather\WeatherDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WeatherInfo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[WeatherEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeatherEntries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'WeatherEntries'
CREATE TABLE [dbo].[WeatherEntries] (
    [EntryID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Time] time  NOT NULL,
    [Temp] float  NOT NULL,
    [Humidity] float  NOT NULL,
    [Td] float  NOT NULL,
    [Pressure] smallint  NOT NULL,
    [WindDir] nvarchar(10)  NULL,
    [WindSpd] smallint  NULL,
    [Cloudness] smallint  NULL,
    [h] smallint  NULL,
    [VV] smallint  NULL,
    [WeatherPhen] nvarchar(200)  NULL
);
GO

-- -------------------------------------------------
-- Creating a clustered index 
-- -------------------------------------------------
CREATE CLUSTERED INDEX cdx_WeatherEntries
    ON [dbo].[WeatherEntries] ([Date], [Time]);
GO
-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EntryID] in table 'WeatherEntries'
ALTER TABLE [dbo].[WeatherEntries]
ADD CONSTRAINT [PK_WeatherEntries]
    PRIMARY KEY ([EntryID] ASC);
GO


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------