CREATE DATABASE [SakadaDB]
GO
USE [SakadaDB]
GO
/****** Object:  Table [dbo].[tblCashAdvance]    Script Date: 28/05/2022 10:49:01 am ******/
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
	[empID] [varchar](29) NULL,
	[isDeleted] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmployee]    Script Date: 28/05/2022 10:49:01 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 28/05/2022 10:49:01 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[user_login] [varchar](50) NULL,
	[user_pass] [varchar](50) NULL,
	[access_level] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupervisor]    Script Date: 28/05/2022 10:49:01 am ******/
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
/****** Object:  Table [dbo].[tblUserAccess]    Script Date: 28/05/2022 10:49:01 am ******/
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
SET IDENTITY_INSERT [dbo].[tblCashAdvance] ON 

INSERT [dbo].[tblCashAdvance] ([ID], [caSupervisor], [caEmployee], [caDate], [caStatus], [caAmount], [empID], [isDeleted]) VALUES (1, 1, 1, N'05/26/2022', N'Approve', N'2000', N'1', 0)
SET IDENTITY_INSERT [dbo].[tblCashAdvance] OFF
GO
SET IDENTITY_INSERT [dbo].[tblEmployee] ON 

INSERT [dbo].[tblEmployee] ([ID], [empFirstName], [empMiddleName], [empLastName], [empAddress], [empMobileNo], [empEmailAddress], [empBirthday], [empAge], [empSupervisor], [isDeleted]) VALUES (1, N'Joan', N'', N'Palaganas', N'N/A', N'09281986669', N'joan.palaganas@sakada.com', N'05/26/2022', 24, 1, 0)
INSERT [dbo].[tblEmployee] ([ID], [empFirstName], [empMiddleName], [empLastName], [empAddress], [empMobileNo], [empEmailAddress], [empBirthday], [empAge], [empSupervisor], [isDeleted]) VALUES (2, N'', N'', N'', N'', N'', N'', N'', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[tblEmployee] OFF
GO
SET IDENTITY_INSERT [dbo].[tblLogin] ON 

INSERT [dbo].[tblLogin] ([id], [username], [user_login], [user_pass], [access_level]) VALUES (1, N'Admin', N'admin', N'p@ssw0rd', N'Admin')
INSERT [dbo].[tblLogin] ([id], [username], [user_login], [user_pass], [access_level]) VALUES (2, N'Supervisor 1', N'sup1', N'p@ssw0rd', N'Supervisor')
SET IDENTITY_INSERT [dbo].[tblLogin] OFF
GO
SET IDENTITY_INSERT [dbo].[tblSupervisor] ON 

INSERT [dbo].[tblSupervisor] ([ID], [supFirstName], [supMiddleName], [supLastName], [supAddress], [supMobileNo], [supEmailAddress], [supBirthday], [supAge], [isDeleted]) VALUES (1, N'Edward', N'', N'Mandac', N'N/A', N'09281986669', N'edward.mandac@sakada.com', N'05/26/2022', 24, 0)
INSERT [dbo].[tblSupervisor] ([ID], [supFirstName], [supMiddleName], [supLastName], [supAddress], [supMobileNo], [supEmailAddress], [supBirthday], [supAge], [isDeleted]) VALUES (2, N'', N'', N'', N'', N'', N'', N'', 0, 1)
SET IDENTITY_INSERT [dbo].[tblSupervisor] OFF
GO
ALTER TABLE [dbo].[tblCashAdvance] ADD  DEFAULT (getdate()) FOR [caDate]
GO
ALTER TABLE [dbo].[tblCashAdvance] ADD  DEFAULT ('Pending') FOR [caStatus]
GO
ALTER TABLE [dbo].[tblCashAdvance] ADD  CONSTRAINT [DF_tblCashAdvance_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[tblEmployee] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[tblSupervisor] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [viewAccess]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [editAccess]
GO
ALTER TABLE [dbo].[tblUserAccess] ADD  DEFAULT ((0)) FOR [adminAccess]
GO
