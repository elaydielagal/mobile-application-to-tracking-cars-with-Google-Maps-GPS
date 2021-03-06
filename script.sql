USE [testDB]
GO
/****** Object:  User [elaydi]    Script Date: 20/02/2021 19:33:03 ******/
CREATE USER [elaydi] FOR LOGIN [elaydi] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_datareader] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [elaydi]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [elaydi]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 20/02/2021 19:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[id_Car] [int] NOT NULL,
	[Brand] [varchar](255) NULL,
	[Model] [varchar](25) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[id_Car] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 20/02/2021 19:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[id_driver] [int] IDENTITY(1,1) NOT NULL,
	[Fname] [varchar](255) NULL,
	[Lname] [varchar](255) NULL,
	[password] [nvarchar](10) NULL,
	[Phone] [int] NULL,
	[username] [nvarchar](10) NOT NULL,
	[Adress] [varchar](255) NULL,
 CONSTRAINT [PK__Driver] PRIMARY KEY CLUSTERED 
(
	[id_driver] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Geolocalisation]    Script Date: 20/02/2021 19:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Geolocalisation](
	[id_geo] [int] IDENTITY(1,1) NOT NULL,
	[longitude] [float] NULL,
	[latitude] [float] NULL,
	[id_mission] [int] NULL,
 CONSTRAINT [PK_Geolocalisation] PRIMARY KEY CLUSTERED 
(
	[id_geo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mission]    Script Date: 20/02/2021 19:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mission](
	[Datetime_Departure] [date] NULL,
	[Datetime_Arrival] [date] NULL,
	[id_mission] [int] IDENTITY(1,1) NOT NULL,
	[id_driver] [int] NOT NULL,
	[name_mission] [nvarchar](10) NULL,
	[id_Car] [int] NOT NULL,
 CONSTRAINT [PK_Mission] PRIMARY KEY CLUSTERED 
(
	[id_mission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 20/02/2021 19:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[id_report] [int] IDENTITY(1,1) NOT NULL,
	[id_mission] [int] NULL,
	[name_rapport] [nvarchar](10) NULL,
	[commentary_problem] [varchar](255) NULL,
 CONSTRAINT [PK__Report] PRIMARY KEY CLUSTERED 
(
	[id_report] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Geolocalisation]  WITH CHECK ADD  CONSTRAINT [FK_Geolocalisation_Mission] FOREIGN KEY([id_mission])
REFERENCES [dbo].[Mission] ([id_mission])
GO
ALTER TABLE [dbo].[Geolocalisation] CHECK CONSTRAINT [FK_Geolocalisation_Mission]
GO
ALTER TABLE [dbo].[Mission]  WITH CHECK ADD  CONSTRAINT [FK_Mission_Car1] FOREIGN KEY([id_Car])
REFERENCES [dbo].[Car] ([id_Car])
GO
ALTER TABLE [dbo].[Mission] CHECK CONSTRAINT [FK_Mission_Car1]
GO
ALTER TABLE [dbo].[Mission]  WITH CHECK ADD  CONSTRAINT [FK_Mission_Driver] FOREIGN KEY([id_driver])
REFERENCES [dbo].[Driver] ([id_driver])
GO
ALTER TABLE [dbo].[Mission] CHECK CONSTRAINT [FK_Mission_Driver]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Mission] FOREIGN KEY([id_mission])
REFERENCES [dbo].[Mission] ([id_mission])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Mission]
GO
