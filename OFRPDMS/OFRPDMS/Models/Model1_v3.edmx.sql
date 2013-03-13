
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/12/2013 17:47:49
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

IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianBorrowBorrowableItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowableItems] DROP CONSTRAINT [FK_PrimaryGuardianBorrowBorrowableItem];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryGuardianPrimaryGuardianBorrow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrimaryGuardianBorrows] DROP CONSTRAINT [FK_PrimaryGuardianPrimaryGuardianBorrow];
GO
IF OBJECT_ID(N'[dbo].[FK_SecondaryGuardianPrimaryGuardian]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SecondaryGuardians] DROP CONSTRAINT [FK_SecondaryGuardianPrimaryGuardian];
GO
IF OBJECT_ID(N'[dbo].[FK_eventparticipantprimaryguardians_EventParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[eventparticipantprimaryguardians] DROP CONSTRAINT [FK_eventparticipantprimaryguardians_EventParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_eventparticipantprimaryguardians_PrimaryGuardian]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[eventparticipantprimaryguardians] DROP CONSTRAINT [FK_eventparticipantprimaryguardians_PrimaryGuardian];
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
IF OBJECT_ID(N'[dbo].[FK_CenterBorrowableItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowableItems] DROP CONSTRAINT [FK_CenterBorrowableItem];
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
IF OBJECT_ID(N'[dbo].[FK_CenterFreeResourceFreeResource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FreeResources] DROP CONSTRAINT [FK_CenterFreeResourceFreeResource];
GO
IF OBJECT_ID(N'[dbo].[FK_Video_inherits_BorrowableItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowableItems_Video] DROP CONSTRAINT [FK_Video_inherits_BorrowableItem];
GO
IF OBJECT_ID(N'[dbo].[FK_Toy_inherits_BorrowableItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BorrowableItems_Toy] DROP CONSTRAINT [FK_Toy_inherits_BorrowableItem];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_SpecialEvent] DROP CONSTRAINT [FK_SpecialEvent_inherits_Event];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BorrowableItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BorrowableItems];
GO
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
IF OBJECT_ID(N'[dbo].[FreeResources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FreeResources];
GO
IF OBJECT_ID(N'[dbo].[BorrowableItems_Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BorrowableItems_Video];
GO
IF OBJECT_ID(N'[dbo].[BorrowableItems_Toy]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BorrowableItems_Toy];
GO
IF OBJECT_ID(N'[dbo].[Events_SpecialEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_SpecialEvent];
GO
IF OBJECT_ID(N'[dbo].[eventparticipantprimaryguardians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[eventparticipantprimaryguardians];
GO
IF OBJECT_ID(N'[dbo].[EventParticipantChild]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventParticipantChild];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BorrowableItems'
CREATE TABLE [dbo].[BorrowableItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsDefective] bit  NULL,
    [InInventory] bit  NOT NULL,
    [Value] decimal(18,0)  NULL,
    [Image] varbinary(max)  NULL,
    [Note] nvarchar(max)  NULL,
    [LendingPeriodDays] int  NULL,
    [ItemType] nvarchar(max)  NULL,
    [PrimaryGuardianBorrowId] int  NOT NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'PrimaryGuardianBorrows'
CREATE TABLE [dbo].[PrimaryGuardianBorrows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BorrowedDate] datetime  NOT NULL,
    [PrimaryGuardianId] int  NOT NULL
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
    [DateCreated] nvarchar(max)  NOT NULL,
    [Language] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [RelationshipToChild] nvarchar(max)  NOT NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'SecondaryGuardians'
CREATE TABLE [dbo].[SecondaryGuardians] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [RelationshipToChild] nvarchar(max)  NOT NULL,
    [Phone] int  NULL,
    [PrimaryGuardianId] int  NOT NULL
);
GO

-- Creating table 'EventParticipants'
CREATE TABLE [dbo].[EventParticipants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NOT NULL,
    [ParticipantId] smallint  NOT NULL,
    [ParticipantType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Children'
CREATE TABLE [dbo].[Children] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Birthdate] datetime  NOT NULL,
    [PrimaryGuardianId] int  NOT NULL
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
    [Date] datetime  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'Referrals'
CREATE TABLE [dbo].[Referrals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CenterReferralId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
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
    [Date] datetime  NULL,
    [Count] int  NULL,
    [CenterId] int  NOT NULL
);
GO

-- Creating table 'FreeResources'
CREATE TABLE [dbo].[FreeResources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Marker] nvarchar(max)  NULL,
    [Language] nvarchar(max)  NULL,
    [CenterFreeResourceId] int  NOT NULL
);
GO

-- Creating table 'BorrowableItems_Video'
CREATE TABLE [dbo].[BorrowableItems_Video] (
    [VideoMarker] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'BorrowableItems_Toy'
CREATE TABLE [dbo].[BorrowableItems_Toy] (
    [Sanitized] bit  NULL,
    [ToyMarker] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Events_SpecialEvent'
CREATE TABLE [dbo].[Events_SpecialEvent] (
    [Name] nvarchar(max)  NOT NULL,
    [GuestSpeaker] nvarchar(max)  NULL,
    [GuestSpeakerType] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'eventparticipantprimaryguardians'
CREATE TABLE [dbo].[eventparticipantprimaryguardians] (
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

-- Creating primary key on [Id] in table 'BorrowableItems'
ALTER TABLE [dbo].[BorrowableItems]
ADD CONSTRAINT [PK_BorrowableItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

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

-- Creating primary key on [Id] in table 'FreeResources'
ALTER TABLE [dbo].[FreeResources]
ADD CONSTRAINT [PK_FreeResources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BorrowableItems_Video'
ALTER TABLE [dbo].[BorrowableItems_Video]
ADD CONSTRAINT [PK_BorrowableItems_Video]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BorrowableItems_Toy'
ALTER TABLE [dbo].[BorrowableItems_Toy]
ADD CONSTRAINT [PK_BorrowableItems_Toy]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events_SpecialEvent'
ALTER TABLE [dbo].[Events_SpecialEvent]
ADD CONSTRAINT [PK_Events_SpecialEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EventParticipants_Id], [PrimaryGuardians_Id] in table 'eventparticipantprimaryguardians'
ALTER TABLE [dbo].[eventparticipantprimaryguardians]
ADD CONSTRAINT [PK_eventparticipantprimaryguardians]
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

-- Creating foreign key on [PrimaryGuardianBorrowId] in table 'BorrowableItems'
ALTER TABLE [dbo].[BorrowableItems]
ADD CONSTRAINT [FK_PrimaryGuardianBorrowBorrowableItem]
    FOREIGN KEY ([PrimaryGuardianBorrowId])
    REFERENCES [dbo].[PrimaryGuardianBorrows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryGuardianBorrowBorrowableItem'
CREATE INDEX [IX_FK_PrimaryGuardianBorrowBorrowableItem]
ON [dbo].[BorrowableItems]
    ([PrimaryGuardianBorrowId]);
GO

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

-- Creating foreign key on [EventParticipants_Id] in table 'eventparticipantprimaryguardians'
ALTER TABLE [dbo].[eventparticipantprimaryguardians]
ADD CONSTRAINT [FK_eventparticipantprimaryguardians_EventParticipant]
    FOREIGN KEY ([EventParticipants_Id])
    REFERENCES [dbo].[EventParticipants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PrimaryGuardians_Id] in table 'eventparticipantprimaryguardians'
ALTER TABLE [dbo].[eventparticipantprimaryguardians]
ADD CONSTRAINT [FK_eventparticipantprimaryguardians_PrimaryGuardian]
    FOREIGN KEY ([PrimaryGuardians_Id])
    REFERENCES [dbo].[PrimaryGuardians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_eventparticipantprimaryguardians_PrimaryGuardian'
CREATE INDEX [IX_FK_eventparticipantprimaryguardians_PrimaryGuardian]
ON [dbo].[eventparticipantprimaryguardians]
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

-- Creating foreign key on [CenterId] in table 'BorrowableItems'
ALTER TABLE [dbo].[BorrowableItems]
ADD CONSTRAINT [FK_CenterBorrowableItem]
    FOREIGN KEY ([CenterId])
    REFERENCES [dbo].[Centers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterBorrowableItem'
CREATE INDEX [IX_FK_CenterBorrowableItem]
ON [dbo].[BorrowableItems]
    ([CenterId]);
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

-- Creating foreign key on [CenterFreeResourceId] in table 'FreeResources'
ALTER TABLE [dbo].[FreeResources]
ADD CONSTRAINT [FK_CenterFreeResourceFreeResource]
    FOREIGN KEY ([CenterFreeResourceId])
    REFERENCES [dbo].[CenterFreeResources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CenterFreeResourceFreeResource'
CREATE INDEX [IX_FK_CenterFreeResourceFreeResource]
ON [dbo].[FreeResources]
    ([CenterFreeResourceId]);
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

-- Creating foreign key on [Id] in table 'BorrowableItems_Video'
ALTER TABLE [dbo].[BorrowableItems_Video]
ADD CONSTRAINT [FK_Video_inherits_BorrowableItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BorrowableItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'BorrowableItems_Toy'
ALTER TABLE [dbo].[BorrowableItems_Toy]
ADD CONSTRAINT [FK_Toy_inherits_BorrowableItem]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BorrowableItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Events_SpecialEvent'
ALTER TABLE [dbo].[Events_SpecialEvent]
ADD CONSTRAINT [FK_SpecialEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------