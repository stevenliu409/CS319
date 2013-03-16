-- don't drop the database unless you need to recreate your database
-- this sql script will drop all foreign keys and tables it created
-- and then recreate tables

--ALTER DATABASE [OFRPDMS.Models.OFRPDMSContext]
--SET SINGLE_USER --or RESTRICTED_USER
--WITH ROLLBACK IMMEDIATE;
--GO
--DROP DATABASE [OFRPDMS.Models.OFRPDMSContext];
--GO

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/15/2013 23:30:27
-- Generated from EDMX file: D:\cs319\CS319\OFRPDMS\OFRPDMS\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OFRPDMS.Models.OFRPDMSContext];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GivenResourceCenter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GivenResources] DROP CONSTRAINT [FK_GivenResourceCenter];
GO
IF OBJECT_ID(N'[dbo].[FK_GivenResourceCenterFreeResource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GivenResources] DROP CONSTRAINT [FK_GivenResourceCenterFreeResource];
GO

IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianPrimaryGuardianBorrow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrimaryGuardianBorrows] DROP CONSTRAINT [FK_PrimaryGuardianPrimaryGuardianBorrow];
GO
IF OBJECT_ID(N'[dbo].[FK_EventParticipantPrimaryGuardian_EventParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipantPrimaryGuardian] DROP CONSTRAINT [FK_EventParticipantPrimaryGuardian_EventParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_EventParticipantPrimaryGuardian_PrimaryGuardian]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipantPrimaryGuardian] DROP CONSTRAINT [FK_EventParticipantPrimaryGuardian_PrimaryGuardian];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianChild]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_PrimaryGuardianChild];
GO
IF OBJECT_ID(N'[dbo].[FK_EventParticipantChild_EventParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipantChild] DROP CONSTRAINT [FK_EventParticipantChild_EventParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_EventParticipantChild_Child]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipantChild] DROP CONSTRAINT [FK_EventParticipantChild_Child];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianAllergy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Allergies] DROP CONSTRAINT [FK_PrimaryGuardianAllergy];
GO
IF OBJECT_ID(N'[dbo].[FK_ChildAllergy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Allergies] DROP CONSTRAINT [FK_ChildAllergy];
GO
IF OBJECT_ID(N'[dbo].[FK_EventEventParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipants] DROP CONSTRAINT [FK_EventEventParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_CenterEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterReferralReferral]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referrals] DROP CONSTRAINT [FK_CenterReferralReferral];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterCenterReferral]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CenterReferrals] DROP CONSTRAINT [FK_CenterCenterReferral];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterCenterAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CenterAccounts] DROP CONSTRAINT [FK_CenterCenterAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterAccountAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_CenterAccountAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_CenterCenterFreeResource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CenterFreeResources] DROP CONSTRAINT [FK_CenterCenterFreeResource];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianSecondaryGuardian]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SecondaryGuardians] DROP CONSTRAINT [FK_PrimaryGuardianSecondaryGuardian];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianCenter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrimaryGuardians] DROP CONSTRAINT [FK_PrimaryGuardianCenter];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialEventEventParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventParticipants] DROP CONSTRAINT [FK_SpecialEventEventParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialEventCenter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialEvents] DROP CONSTRAINT [FK_SpecialEventCenter];
GO
IF OBJECT_ID(N'[dbo].[FK_LibraryItemPrimaryGuardianBorrow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LibraryItems] DROP CONSTRAINT [FK_LibraryItemPrimaryGuardianBorrow];
IF OBJECT_ID(N'[dbo].[FK_CenterFreeResourceGivenResource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GivenResources] DROP CONSTRAINT [FK_CenterFreeResourceGivenResource];
GO
IF OBJECT_ID(N'[dbo].[FK_LibraryItemCenter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LibraryItems] DROP CONSTRAINT [FK_LibraryItemCenter];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianBorrowLibraryItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrimaryGuardianBorrows] DROP CONSTRAINT [FK_PrimaryGuardianBorrowLibraryItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Video_inherits_LibraryItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LibraryItems_Video] DROP CONSTRAINT [FK_Video_inherits_LibraryItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Toy_inherits_LibraryItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LibraryItems_Toy] DROP CONSTRAINT [FK_Toy_inherits_LibraryItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Book_inherits_LibraryItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LibraryItems_Book] DROP CONSTRAINT [FK_Book_inherits_LibraryItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PrimaryGuardianBorrows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrimaryGuardianBorrows];
GO
IF OBJECT_ID(N'[dbo].[PrimaryGuardians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrimaryGuardians];
GO
IF OBJECT_ID(N'[dbo].[SecondaryGuardians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SecondaryGuardians];
GO
IF OBJECT_ID(N'[dbo].[EventParticipants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventParticipants];
GO
IF OBJECT_ID(N'[dbo].[Children]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Children];
GO
IF OBJECT_ID(N'[dbo].[Allergies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Allergies];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Centers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Centers];
GO
IF OBJECT_ID(N'[dbo].[CenterReferrals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CenterReferrals];
GO
IF OBJECT_ID(N'[dbo].[Referrals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Referrals];
GO
IF OBJECT_ID(N'[dbo].[CenterAccounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CenterAccounts];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[CenterFreeResources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CenterFreeResources];
GO
IF OBJECT_ID(N'[dbo].[SpecialEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecialEvents];
GO
IF OBJECT_ID(N'[dbo].[GivenResources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GivenResources];
GO
IF OBJECT_ID(N'[dbo].[LibraryItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LibraryItems];
GO
IF OBJECT_ID(N'[dbo].[LibraryItems_Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LibraryItems_Video];
GO
IF OBJECT_ID(N'[dbo].[LibraryItems_Toy]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LibraryItems_Toy];
GO
IF OBJECT_ID(N'[dbo].[LibraryItems_Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LibraryItems_Book];
GO
IF OBJECT_ID(N'[dbo].[EventParticipantPrimaryGuardian]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventParticipantPrimaryGuardian];
GO
IF OBJECT_ID(N'[dbo].[EventParticipantChild]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventParticipantChild];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PrimaryGuardianBorrows'
CREATE TABLE [dbo].[PrimaryGuardianBorrows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BorrowDate] datetime  NOT NULL,
    [PrimaryGuardianId] int  NOT NULL,
    [Returned] bit  NOT NULL,
    [LibraryItemId] int  NOT NULL,
    [DueDate] datetime  NOT NULL,
    [ReturnDate] datetime  NULL
);
GO

-- Creating table 'PrimaryGuardians'
CREATE TABLE [dbo].[PrimaryGuardians] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Phone] int  NULL,
    [PostalCodePrefix] nvarchar(max)  NULL,
    [DateCreated] datetime  NOT NULL,
    [Language] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'SecondaryGuardians'
CREATE TABLE [dbo].[SecondaryGuardians] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [RelationshipToChild] nvarchar(max)  NULL,
    [Phone] int  NULL,
    [PrimaryGuardianId] int  NOT NULL
);
GO

-- Creating table 'EventParticipants'
CREATE TABLE [dbo].[EventParticipants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NOT NULL,
    [ParticipantId] smallint  NOT NULL,
    [ParticipantType] nvarchar(max)  NOT NULL,
    [SpecialEvent_Id] int  NOT NULL
);
GO

-- Creating table 'Children'
CREATE TABLE [dbo].[Children] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Birthdate] datetime  NULL,
    [PrimaryGuardianId] int  NOT NULL,
    [GuardianRelationship] nvarchar(max)  NULL
);
GO

-- Creating table 'Allergies'
CREATE TABLE [dbo].[Allergies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrimaryGuardianId] int  NOT NULL,
    [ChildId] int  NOT NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'Centers'
CREATE TABLE [dbo].[Centers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CenterReferrals'
CREATE TABLE [dbo].[CenterReferrals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CenterId] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Referrals'
CREATE TABLE [dbo].[Referrals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CenterReferralId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateReferred] datetime  NOT NULL,
    [CountReferred] int  NOT NULL
);
GO

-- Creating table 'CenterAccounts'
CREATE TABLE [dbo].[CenterAccounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AccountName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [RoleId] nvarchar(max)  NOT NULL,
    [CenterAccountId] int  NOT NULL
);
GO

-- Creating table 'CenterFreeResources'
CREATE TABLE [dbo].[CenterFreeResources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumberAvailable] int  NULL,
    [CenterId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SpecialEvents'
CREATE TABLE [dbo].[SpecialEvents] (
    [Name] nvarchar(max)  NOT NULL,
    [GuestSpeaker] nvarchar(max)  NULL,
    [GuestSpeakerType] nvarchar(max)  NULL,
    [CenterId] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'GivenResources'
CREATE TABLE [dbo].[GivenResources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateGiven] datetime  NULL,
    [Count] int  NOT NULL,
    [CenterId] int  NOT NULL,
    [CenterFreeResourceId] int  NOT NULL
);
GO

-- Creating table 'LibraryItems'
CREATE TABLE [dbo].[LibraryItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Broken] nvarchar(max)  NOT NULL,
    [CheckedOut] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [Note] nvarchar(max)  NOT NULL,
    [LendingPeriod] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'LibraryItems_Video'
CREATE TABLE [dbo].[LibraryItems_Video] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'LibraryItems_Toy'
CREATE TABLE [dbo].[LibraryItems_Toy] (
    [Sanitized] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'LibraryItems_Book'
CREATE TABLE [dbo].[LibraryItems_Book] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventParticipantPrimaryGuardian'
CREATE TABLE [dbo].[EventParticipantPrimaryGuardian] (
    [EventParticipants_Id] int  NOT NULL,
    [PrimaryGuardians_Id] int  NOT NULL
);
GO

-- Creating table 'EventParticipantChild'
CREATE TABLE [dbo].[EventParticipantChild] (
    [EventParticipants_Id] int  NOT NULL,
    [Children_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PrimaryGuardianBorrows'
ALTER TABLE [dbo].[PrimaryGuardianBorrows]
ADD CONSTRAINT [PK_PrimaryGuardianBorrows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PrimaryGuardians'
ALTER TABLE [dbo].[PrimaryGuardians]
ADD CONSTRAINT [PK_PrimaryGuardians]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SecondaryGuardians'
ALTER TABLE [dbo].[SecondaryGuardians]
ADD CONSTRAINT [PK_SecondaryGuardians]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventParticipants'
ALTER TABLE [dbo].[EventParticipants]
ADD CONSTRAINT [PK_EventParticipants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [PK_Children]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Allergies'
ALTER TABLE [dbo].[Allergies]
ADD CONSTRAINT [PK_Allergies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Centers'
ALTER TABLE [dbo].[Centers]
ADD CONSTRAINT [PK_Centers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CenterReferrals'
ALTER TABLE [dbo].[CenterReferrals]
ADD CONSTRAINT [PK_CenterReferrals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [PK_Referrals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CenterAccounts'
ALTER TABLE [dbo].[CenterAccounts]
ADD CONSTRAINT [PK_CenterAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CenterFreeResources'
ALTER TABLE [dbo].[CenterFreeResources]
ADD CONSTRAINT [PK_CenterFreeResources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpecialEvents'
ALTER TABLE [dbo].[SpecialEvents]
ADD CONSTRAINT [PK_SpecialEvents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GivenResources'
ALTER TABLE [dbo].[GivenResources]
ADD CONSTRAINT [PK_GivenResources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LibraryItems'
ALTER TABLE [dbo].[LibraryItems]
ADD CONSTRAINT [PK_LibraryItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LibraryItems_Video'
ALTER TABLE [dbo].[LibraryItems_Video]
ADD CONSTRAINT [PK_LibraryItems_Video]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LibraryItems_Toy'
ALTER TABLE [dbo].[LibraryItems_Toy]
ADD CONSTRAINT [PK_LibraryItems_Toy]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LibraryItems_Book'
ALTER TABLE [dbo].[LibraryItems_Book]
ADD CONSTRAINT [PK_LibraryItems_Book]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EventParticipants_Id], [PrimaryGuardians_Id] in table 'EventParticipantPrimaryGuardian'
ALTER TABLE [dbo].[EventParticipantPrimaryGuardian]
ADD CONSTRAINT [PK_EventParticipantPrimaryGuardian]
    PRIMARY KEY NONCLUSTERED ([EventParticipants_Id], [PrimaryGuardians_Id] ASC);
GO

-- Creating primary key on [EventParticipants_Id], [Children_Id] in table 'EventParticipantChild'
ALTER TABLE [dbo].[EventParticipantChild]
ADD CONSTRAINT [PK_EventParticipantChild]
    PRIMARY KEY NONCLUSTERED ([EventParticipants_Id], [Children_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PrimaryGuardianId] in table 'PrimaryGuardianBorrows'
ALTER TABLE [dbo].[PrimaryGuardianBorrows]
ADD CONSTRAINT [FK_PrimaryGuardianPrimaryGuardianBorrow]
    FOREIGN KEY ([PrimaryGuardianId])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianPrimaryGuardianBorrow'
CREATE INDEX [IX_FK_PrimaryGuardianPrimaryGuardianBorrow]
ON [dbo].[PrimaryGuardianBorrows]
    ([PrimaryGuardianId]);
GO

-- Creating foreign key on [EventParticipants_Id] in table 'EventParticipantPrimaryGuardian'
ALTER TABLE [dbo].[EventParticipantPrimaryGuardian]
ADD CONSTRAINT [FK_EventParticipantPrimaryGuardian_EventParticipant]
    FOREIGN KEY ([EventParticipants_Id])
    REFERENCES [dbo].[EventParticipants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PrimaryGuardians_Id] in table 'EventParticipantPrimaryGuardian'
ALTER TABLE [dbo].[EventParticipantPrimaryGuardian]
ADD CONSTRAINT [FK_EventParticipantPrimaryGuardian_PrimaryGuardian]
    FOREIGN KEY ([PrimaryGuardians_Id])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventParticipantPrimaryGuardian_PrimaryGuardian'
CREATE INDEX [IX_FK_EventParticipantPrimaryGuardian_PrimaryGuardian]
ON [dbo].[EventParticipantPrimaryGuardian]
    ([PrimaryGuardians_Id]);
GO

-- Creating foreign key on [PrimaryGuardianId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_PrimaryGuardianChild]
    FOREIGN KEY ([PrimaryGuardianId])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianChild'
CREATE INDEX [IX_FK_PrimaryGuardianChild]
ON [dbo].[Children]
    ([PrimaryGuardianId]);
GO

-- Creating foreign key on [EventParticipants_Id] in table 'EventParticipantChild'
ALTER TABLE [dbo].[EventParticipantChild]
ADD CONSTRAINT [FK_EventParticipantChild_EventParticipant]
    FOREIGN KEY ([EventParticipants_Id])
    REFERENCES [dbo].[EventParticipants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Children_Id] in table 'EventParticipantChild'
ALTER TABLE [dbo].[EventParticipantChild]
ADD CONSTRAINT [FK_EventParticipantChild_Child]
    FOREIGN KEY ([Children_Id])
    REFERENCES [dbo].[Children]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventParticipantChild_Child'
CREATE INDEX [IX_FK_EventParticipantChild_Child]
ON [dbo].[EventParticipantChild]
    ([Children_Id]);
GO

-- Creating foreign key on [PrimaryGuardianId] in table 'Allergies'
ALTER TABLE [dbo].[Allergies]
ADD CONSTRAINT [FK_PrimaryGuardianAllergy]
    FOREIGN KEY ([PrimaryGuardianId])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianAllergy'
CREATE INDEX [IX_FK_PrimaryGuardianAllergy]
ON [dbo].[Allergies]
    ([PrimaryGuardianId]);
GO

-- Creating foreign key on [ChildId] in table 'Allergies'
ALTER TABLE [dbo].[Allergies]
ADD CONSTRAINT [FK_ChildAllergy]
    FOREIGN KEY ([ChildId])
    REFERENCES [dbo].[Children]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChildAllergy'
CREATE INDEX [IX_FK_ChildAllergy]
ON [dbo].[Allergies]
    ([ChildId]);
GO

-- Creating foreign key on [EventId] in table 'EventParticipants'
ALTER TABLE [dbo].[EventParticipants]
ADD CONSTRAINT [FK_EventEventParticipant]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventEventParticipant'
CREATE INDEX [IX_FK_EventEventParticipant]
ON [dbo].[EventParticipants]
    ([EventId]);
GO

-- Creating foreign key on [CenterId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_CenterEvent]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterEvent'
CREATE INDEX [IX_FK_CenterEvent]
ON [dbo].[Events]
    ([CenterId]);
GO

-- Creating foreign key on [CenterReferralId] in table 'Referrals'
ALTER TABLE [dbo].[Referrals]
ADD CONSTRAINT [FK_CenterReferralReferral]
    FOREIGN KEY ([CenterReferralId])
    REFERENCES [dbo].[CenterReferrals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterReferralReferral'
CREATE INDEX [IX_FK_CenterReferralReferral]
ON [dbo].[Referrals]
    ([CenterReferralId]);
GO

-- Creating foreign key on [CenterId] in table 'CenterReferrals'
ALTER TABLE [dbo].[CenterReferrals]
ADD CONSTRAINT [FK_CenterCenterReferral]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterCenterReferral'
CREATE INDEX [IX_FK_CenterCenterReferral]
ON [dbo].[CenterReferrals]
    ([CenterId]);
GO

-- Creating foreign key on [CenterId] in table 'CenterAccounts'
ALTER TABLE [dbo].[CenterAccounts]
ADD CONSTRAINT [FK_CenterCenterAccount]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterCenterAccount'
CREATE INDEX [IX_FK_CenterCenterAccount]
ON [dbo].[CenterAccounts]
    ([CenterId]);
GO

-- Creating foreign key on [CenterAccountId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_CenterAccountAccount]
    FOREIGN KEY ([CenterAccountId])
    REFERENCES [dbo].[CenterAccounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterAccountAccount'
CREATE INDEX [IX_FK_CenterAccountAccount]
ON [dbo].[Accounts]
    ([CenterAccountId]);
GO

-- Creating foreign key on [CenterId] in table 'CenterFreeResources'
ALTER TABLE [dbo].[CenterFreeResources]
ADD CONSTRAINT [FK_CenterCenterFreeResource]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterCenterFreeResource'
CREATE INDEX [IX_FK_CenterCenterFreeResource]
ON [dbo].[CenterFreeResources]
    ([CenterId]);
GO

-- Creating foreign key on [PrimaryGuardianId] in table 'SecondaryGuardians'
ALTER TABLE [dbo].[SecondaryGuardians]
ADD CONSTRAINT [FK_PrimaryGuardianSecondaryGuardian]
    FOREIGN KEY ([PrimaryGuardianId])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianSecondaryGuardian'
CREATE INDEX [IX_FK_PrimaryGuardianSecondaryGuardian]
ON [dbo].[SecondaryGuardians]
    ([PrimaryGuardianId]);
GO

-- Creating foreign key on [CenterId] in table 'PrimaryGuardians'
ALTER TABLE [dbo].[PrimaryGuardians]
ADD CONSTRAINT [FK_PrimaryGuardianCenter]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianCenter'
CREATE INDEX [IX_FK_PrimaryGuardianCenter]
ON [dbo].[PrimaryGuardians]
    ([CenterId]);
GO

-- Creating foreign key on [SpecialEvent_Id] in table 'EventParticipants'
ALTER TABLE [dbo].[EventParticipants]
ADD CONSTRAINT [FK_SpecialEventEventParticipant]
    FOREIGN KEY ([SpecialEvent_Id])
    REFERENCES [dbo].[SpecialEvents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialEventEventParticipant'
CREATE INDEX [IX_FK_SpecialEventEventParticipant]
ON [dbo].[EventParticipants]
    ([SpecialEvent_Id]);
GO

-- Creating foreign key on [CenterId] in table 'SpecialEvents'
ALTER TABLE [dbo].[SpecialEvents]
ADD CONSTRAINT [FK_SpecialEventCenter]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialEventCenter'
CREATE INDEX [IX_FK_SpecialEventCenter]
ON [dbo].[SpecialEvents]
    ([CenterId]);
GO

-- Creating foreign key on [CenterFreeResourceId] in table 'GivenResources'
ALTER TABLE [dbo].[GivenResources]
ADD CONSTRAINT [FK_CenterFreeResourceGivenResource]
    FOREIGN KEY ([CenterFreeResourceId])
    REFERENCES [dbo].[CenterFreeResources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterFreeResourceGivenResource'
CREATE INDEX [IX_FK_CenterFreeResourceGivenResource]
ON [dbo].[GivenResources]
    ([CenterFreeResourceId]);
GO

-- Creating foreign key on [CenterId] in table 'LibraryItems'
ALTER TABLE [dbo].[LibraryItems]
ADD CONSTRAINT [FK_LibraryItemCenter]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LibraryItemCenter'
CREATE INDEX [IX_FK_LibraryItemCenter]
ON [dbo].[LibraryItems]
    ([CenterId]);
GO

-- Creating foreign key on [LibraryItemId] in table 'PrimaryGuardianBorrows'
ALTER TABLE [dbo].[PrimaryGuardianBorrows]
ADD CONSTRAINT [FK_PrimaryGuardianBorrowLibraryItem]
    FOREIGN KEY ([LibraryItemId])
    REFERENCES [dbo].[LibraryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianBorrowLibraryItem'
CREATE INDEX [IX_FK_PrimaryGuardianBorrowLibraryItem]
ON [dbo].[PrimaryGuardianBorrows]
    ([LibraryItemId]);
GO

-- Creating foreign key on [Id] in table 'LibraryItems_Video'
ALTER TABLE [dbo].[LibraryItems_Video]
ADD CONSTRAINT [FK_Video_inherits_LibraryItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[LibraryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'LibraryItems_Toy'
ALTER TABLE [dbo].[LibraryItems_Toy]
ADD CONSTRAINT [FK_Toy_inherits_LibraryItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[LibraryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'LibraryItems_Book'
ALTER TABLE [dbo].[LibraryItems_Book]
ADD CONSTRAINT [FK_Book_inherits_LibraryItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[LibraryItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------