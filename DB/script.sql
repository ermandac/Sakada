USE [SakadaDB]
GO
/****** Object:  Table [dbo].[tblCashAdvance]    Script Date: 25/05/2022 5:20:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCashAdvance](
	[caID]  AS ('CA-'+CONVERT([varchar](25),[ID])) PERSISTED NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[caSupervisor] [int] NULL,
	[caEmployee] [int] NULL,
	[caDate] [varchar](25) NULL,
	[caStatus] [varchar](max) NULL,
	[caAmount] [varchar](max) NULL,
	[empID] [varchar](29) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmployee]    Script Date: 25/05/2022 5:20:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ARITHABORT ON
GO
CREATE TABLE [dbo].[tblEmployee](
	[empID]  AS ('EMP-'+CONVERT([varchar](25),[ID])) PERSISTED NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[empFirstName] [varchar](max) NULL,
	[empMiddleName] [varchar](max) NULL,
	[empLastName] [varchar](max) NULL,
	[empAddress] [varchar](max) NULL,
	[empMobileNo] [varchar](11) NULL,
	[empEmailAddress] [varchar](50) NULL,
	[empBirthday] [varchar](50) NULL,
	[empAge] [int] NULL,
	[empSupervisor] [int] NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[empID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 25/05/2022 5:20:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[user_login] [varchar](50) NULL,
	[user_pass] [varchar](50) NULL,
	[data_control] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupervisor]    Script Date: 25/05/2022 5:20:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupervisor](
	[supID]  AS ('SUP-'+CONVERT([varchar](25),[ID])) PERSISTED NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[supFirstName] [varchar](max) NULL,
	[supMiddleName] [varchar](max) NULL,
	[supLastName] [varchar](max) NULL,
	[supAddress] [varchar](max) NULL,
	[supMobileNo] [varchar](11) NULL,
	[supEmailAddress] [varchar](50) NULL,
	[supBirthday] [varchar](50) NULL,
	[supAge] [int] NULL,
	[isDeleted] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserAccess]    Script Date: 25/05/2022 5:20:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserAccess](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idLogin] [int] NULL,
	[viewAccess] [bit] NULL,
	[editAccess] [bit] NULL,
	[adminAccess] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCashAdvance] ADD  DEFAULT (getdate()) FOR [caDate]
GO
ALTER TABLE [dbo].[tblCashAdvance] ADD  DEFAULT ('Pending') FOR [caStatus]
GO
ALTER TABLE [dbo].[tblEmployee] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[tblLogin] ADD  DEFAULT ((1)) FOR [data_control]
GO
ALTER TABLE [dbo].[tblSupervisor] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [viewAccess]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [editAccess]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [adminAccess]
GO
