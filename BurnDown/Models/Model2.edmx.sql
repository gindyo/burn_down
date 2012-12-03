
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/03/2012 10:32:10
-- Generated from EDMX file: C:\Users\Limited\Documents\Visual Studio 2010\Projects\BurnDown\BurnDown\Models\Model2.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bdownDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[agendaItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[agendaItems];
GO
IF OBJECT_ID(N'[dbo].[developers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[developers];
GO
IF OBJECT_ID(N'[dbo].[projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[projects];
GO
IF OBJECT_ID(N'[dbo].[tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tasks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'agendaItems'
CREATE TABLE [dbo].[agendaItems] (
    [agendaItemId] int IDENTITY(1,1) NOT NULL,
    [agendaItemName] nchar(50)  NOT NULL,
    [completed] bit  NOT NULL,
    [notes] nchar(2000)  NULL,
    [task_taskId] int  NOT NULL
);
GO

-- Creating table 'developers'
CREATE TABLE [dbo].[developers] (
    [developerId] int IDENTITY(1,1) NOT NULL,
    [firstName] nchar(20)  NOT NULL,
    [lastName] nchar(20)  NULL,
    [email] nchar(30)  NULL,
    [phone] nchar(12)  NULL,
    [hoursPerDayAvailable] int  NULL
);
GO

-- Creating table 'projects'
CREATE TABLE [dbo].[projects] (
    [projectId] int IDENTITY(1,1) NOT NULL,
    [projectName] nchar(50)  NOT NULL,
    [priority] int  NOT NULL,
    [leadDeveloper] int  NOT NULL
);
GO

-- Creating table 'tasks'
CREATE TABLE [dbo].[tasks] (
    [taskId] int IDENTITY(1,1) NOT NULL,
    [originalEstimatedHours] int  NOT NULL,
    [hoursSpentOnTask] int  NOT NULL,
    [shareOfProject] int  NULL,
    [percentCompleted] int  NULL,
    [priority] int  NOT NULL,
    [taskName] nchar(50)  NOT NULL,
    [startDate] datetime  NOT NULL,
    [dueDate] datetime  NOT NULL,
    [hoursForTasksWithHigherPriority] int  NULL,
    [notes] nchar(2000)  NULL,
    [developer_developerId] int  NOT NULL,
    [project_projectId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [agendaItemId] in table 'agendaItems'
ALTER TABLE [dbo].[agendaItems]
ADD CONSTRAINT [PK_agendaItems]
    PRIMARY KEY CLUSTERED ([agendaItemId] ASC);
GO

-- Creating primary key on [developerId] in table 'developers'
ALTER TABLE [dbo].[developers]
ADD CONSTRAINT [PK_developers]
    PRIMARY KEY CLUSTERED ([developerId] ASC);
GO

-- Creating primary key on [projectId] in table 'projects'
ALTER TABLE [dbo].[projects]
ADD CONSTRAINT [PK_projects]
    PRIMARY KEY CLUSTERED ([projectId] ASC);
GO

-- Creating primary key on [taskId] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [PK_tasks]
    PRIMARY KEY CLUSTERED ([taskId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [developer_developerId] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [FK_taskdeveloper]
    FOREIGN KEY ([developer_developerId])
    REFERENCES [dbo].[developers]
        ([developerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_taskdeveloper'
CREATE INDEX [IX_FK_taskdeveloper]
ON [dbo].[tasks]
    ([developer_developerId]);
GO

-- Creating foreign key on [task_taskId] in table 'agendaItems'
ALTER TABLE [dbo].[agendaItems]
ADD CONSTRAINT [FK_taskagendaItem]
    FOREIGN KEY ([task_taskId])
    REFERENCES [dbo].[tasks]
        ([taskId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_taskagendaItem'
CREATE INDEX [IX_FK_taskagendaItem]
ON [dbo].[agendaItems]
    ([task_taskId]);
GO

-- Creating foreign key on [project_projectId] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [FK_projecttask]
    FOREIGN KEY ([project_projectId])
    REFERENCES [dbo].[projects]
        ([projectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_projecttask'
CREATE INDEX [IX_FK_projecttask]
ON [dbo].[tasks]
    ([project_projectId]);
GO

-- Creating foreign key on [leadDeveloper] in table 'projects'
ALTER TABLE [dbo].[projects]
ADD CONSTRAINT [FK_developerproject]
    FOREIGN KEY ([leadDeveloper])
    REFERENCES [dbo].[developers]
        ([developerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_developerproject'
CREATE INDEX [IX_FK_developerproject]
ON [dbo].[projects]
    ([leadDeveloper]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------