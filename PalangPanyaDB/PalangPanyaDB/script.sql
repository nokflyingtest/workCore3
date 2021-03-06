USE [master]
GO
/****** Object:  Database [PalangPanyaDB]    Script Date: 06-06-2559 7:24:13 ******/
CREATE DATABASE [PalangPanyaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PalangPanyaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PalangPanyaDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PalangPanyaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PalangPanyaDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PalangPanyaDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PalangPanyaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PalangPanyaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PalangPanyaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PalangPanyaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PalangPanyaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PalangPanyaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PalangPanyaDB] SET  MULTI_USER 
GO
ALTER DATABASE [PalangPanyaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PalangPanyaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PalangPanyaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PalangPanyaDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PalangPanyaDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PalangPanyaDB]
GO
/****** Object:  User [IIS APPPOOL\PalangPanya]    Script Date: 06-06-2559 7:24:14 ******/
CREATE USER [IIS APPPOOL\PalangPanya] FOR LOGIN [IIS APPPOOL\PalangPanya] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\PalangPanya]
GO
/****** Object:  Table [dbo].[album]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[album](
	[album_code] [varchar](30) NOT NULL,
	[album_name] [nvarchar](100) NOT NULL,
	[album_desc] [nvarchar](200) NULL,
	[created_by] [varchar](30) NOT NULL,
	[album_date] [datetime] NOT NULL DEFAULT (getdate()),
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_album] PRIMARY KEY CLUSTERED 
(
	[album_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[album_image]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[album_image](
	[album_code] [varchar](30) NOT NULL,
	[image_code] [varchar](30) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
 CONSTRAINT [pk_album_image] PRIMARY KEY CLUSTERED 
(
	[album_code] ASC,
	[image_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_grade]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_grade](
	[cgrade_code] [char](1) NOT NULL,
	[cgrade_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_course_grade] PRIMARY KEY CLUSTERED 
(
	[cgrade_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_group]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_group](
	[cgroup_code] [char](3) NOT NULL,
	[cgroup_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_course_group] PRIMARY KEY CLUSTERED 
(
	[cgroup_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_instructor]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_instructor](
	[instructor_code] [varchar](30) NOT NULL,
	[course_code] [varchar](30) NOT NULL,
	[confirm_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[instructor_cost] [money] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_course_instructor] PRIMARY KEY CLUSTERED 
(
	[instructor_code] ASC,
	[course_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_train_place]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_train_place](
	[place_code] [varchar](30) NOT NULL,
	[course_code] [varchar](30) NOT NULL,
	[confirm_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[place_cost] [money] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_course_train_place] PRIMARY KEY CLUSTERED 
(
	[place_code] ASC,
	[course_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_type]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_type](
	[cgroup_code] [char](3) NOT NULL,
	[ctype_code] [char](3) NOT NULL,
	[ctype_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_course_type] PRIMARY KEY CLUSTERED 
(
	[ctype_code] ASC,
	[cgroup_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_config]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_config](
	[client_code] [varchar](30) NOT NULL,
	[system] [nvarchar](50) NOT NULL,
	[module] [nvarchar](50) NOT NULL,
	[cnfig_item] [nvarchar](50) NOT NULL,
	[text_value] [nvarchar](500) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_config] PRIMARY KEY CLUSTERED 
(
	[client_code] ASC,
	[system] ASC,
	[module] ASC,
	[cnfig_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_country]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_country](
	[country_code] [int] NOT NULL,
	[country_desc] [nvarchar](100) NOT NULL,
	[area_part] [varchar](30) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_country] PRIMARY KEY CLUSTERED 
(
	[country_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_district]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_district](
	[country_code] [int] NOT NULL,
	[province_code] [char](8) NOT NULL,
	[district_code] [char](8) NOT NULL,
	[dist_desc] [nvarchar](100) NOT NULL,
	[area_part] [varchar](30) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_district] PRIMARY KEY CLUSTERED 
(
	[district_code] ASC,
	[province_code] ASC,
	[country_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_list_zip]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_list_zip](
	[province_code] [char](8) NOT NULL,
	[country_code] [int] NOT NULL,
	[district_code] [char](8) NOT NULL,
	[subdistrict_code] [char](8) NOT NULL,
	[zip_code] [char](5) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_list_zip] PRIMARY KEY CLUSTERED 
(
	[province_code] ASC,
	[country_code] ASC,
	[district_code] ASC,
	[subdistrict_code] ASC,
	[zip_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_province]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_province](
	[country_code] [int] NOT NULL,
	[province_code] [char](8) NOT NULL,
	[pro_desc] [nvarchar](100) NOT NULL,
	[area_part] [varchar](30) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_province] PRIMARY KEY CLUSTERED 
(
	[province_code] ASC,
	[country_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ini_subdistrict]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ini_subdistrict](
	[country_code] [int] NOT NULL,
	[province_code] [char](8) NOT NULL,
	[district_code] [char](8) NOT NULL,
	[subdistrict_code] [char](8) NOT NULL,
	[dist_desc] [nvarchar](100) NOT NULL,
	[area_part] [varchar](30) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_ini_subdistrict] PRIMARY KEY CLUSTERED 
(
	[province_code] ASC,
	[country_code] ASC,
	[district_code] ASC,
	[subdistrict_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[instructor]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[instructor](
	[instructor_code] [varchar](30) NOT NULL,
	[instructor_desc] [nvarchar](100) NOT NULL,
	[confirm_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[contactor] [nvarchar](100) NULL,
	[contactor_detail] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_instructor] PRIMARY KEY CLUSTERED 
(
	[instructor_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_education]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_education](
	[member_code] [varchar](30) NOT NULL,
	[rec_no] [int] NOT NULL,
	[degree] [nvarchar](100) NOT NULL,
	[colledge_name] [nvarchar](500) NULL,
	[faculty] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_education] PRIMARY KEY CLUSTERED 
(
	[rec_no] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_group]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_group](
	[mem_group_code] [char](3) NOT NULL,
	[mem_group_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_group] PRIMARY KEY CLUSTERED 
(
	[mem_group_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_health]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_health](
	[member_code] [varchar](30) NOT NULL,
	[medical_history] [nvarchar](500) NULL,
	[blood_group] [char](1) NULL,
	[hobby] [nvarchar](500) NULL,
	[restrict_food] [nvarchar](500) NULL,
	[special_skill] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_health] PRIMARY KEY CLUSTERED 
(
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_level]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_level](
	[mlevel_code] [char](3) NOT NULL,
	[mlevel_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_level] PRIMARY KEY CLUSTERED 
(
	[mlevel_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_reward]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_reward](
	[member_code] [varchar](30) NOT NULL,
	[rec_no] [int] NOT NULL,
	[reward_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_reward] PRIMARY KEY CLUSTERED 
(
	[rec_no] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_site_visit]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_site_visit](
	[member_code] [varchar](30) NOT NULL,
	[rec_no] [int] NOT NULL,
	[site_visit_desc] [nvarchar](500) NOT NULL,
	[country_code] [int] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_site_visit] PRIMARY KEY CLUSTERED 
(
	[rec_no] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_social]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_social](
	[member_code] [varchar](30) NOT NULL,
	[rec_no] [int] NOT NULL,
	[social_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_social] PRIMARY KEY CLUSTERED 
(
	[rec_no] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_status]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_status](
	[mstatus_code] [char](3) NOT NULL,
	[mstatus_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_status] PRIMARY KEY CLUSTERED 
(
	[mstatus_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_train_record]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_train_record](
	[course_code] [varchar](30) NOT NULL,
	[member_code] [varchar](30) NOT NULL,
	[course_grade] [char](1) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
 CONSTRAINT [pk_mem_train_record] PRIMARY KEY CLUSTERED 
(
	[course_code] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_type]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_type](
	[mem_group_code] [char](3) NOT NULL,
	[mem_type_code] [char](3) NOT NULL,
	[mem_type_desc] [nvarchar](100) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_type] PRIMARY KEY CLUSTERED 
(
	[mem_type_code] ASC,
	[mem_group_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mem_worklist]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mem_worklist](
	[rec_no] [int] NOT NULL,
	[member_code] [varchar](30) NULL,
	[company_name_th] [nvarchar](100) NULL,
	[company_name_eng] [nvarchar](100) NULL,
	[position_name_th] [nvarchar](100) NULL,
	[position_name_eng] [nvarchar](100) NULL,
	[work_year] [char](4) NULL,
	[office_address] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_mem_worklist] PRIMARY KEY CLUSTERED 
(
	[rec_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[member]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[member](
	[member_code] [varchar](30) NOT NULL,
	[fname] [nvarchar](100) NULL,
	[lname] [nvarchar](100) NULL,
	[sex] [char](1) NULL,
	[nationality] [nvarchar](30) NULL,
	[mem_photo] [varchar](30) NULL,
	[cid_type] [char](1) NULL,
	[cid_card] [varchar](30) NULL,
	[cid_card_pic] [varchar](30) NULL,
	[birthdate] [datetime] NULL,
	[current_age] [smallint] NULL,
	[religion] [nvarchar](30) NULL,
	[place_name] [nvarchar](50) NULL,
	[marry_status] [char](1) NULL,
	[h_no] [nvarchar](20) NULL,
	[lot_no] [nvarchar](20) NULL,
	[village] [nvarchar](50) NULL,
	[building] [nvarchar](50) NULL,
	[floor] [nvarchar](20) NULL,
	[room] [nvarchar](20) NULL,
	[lane] [nvarchar](50) NULL,
	[street] [nvarchar](50) NULL,
	[subdistrict_code] [char](8) NULL,
	[district_code] [char](8) NULL,
	[province_code] [char](8) NULL,
	[country_code] [int] NULL,
	[zip_code] [char](5) NULL,
	[mstatus_code] [char](3) NULL,
	[mem_type_code] [char](3) NULL,
	[mem_group_code] [char](3) NULL,
	[mlevel_code] [char](3) NULL,
	[zone] [nvarchar](30) NULL,
	[latitude] [decimal](9, 6) NULL,
	[longitude] [decimal](9, 6) NULL,
	[texta_address] [nvarchar](200) NULL,
	[textb_address] [nvarchar](200) NULL,
	[textc_address] [nvarchar](200) NULL,
	[tel] [nvarchar](50) NULL,
	[mobile] [nvarchar](50) NULL,
	[fax] [nvarchar](50) NULL,
	[social_app_data] [nvarchar](500) NULL,
	[email] [nvarchar](100) NULL,
	[parent_code] [varchar](30) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[rowversion] [timestamp] NULL,
 CONSTRAINT [pk_member] PRIMARY KEY CLUSTERED 
(
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pic_image]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pic_image](
	[image_code] [varchar](30) NOT NULL,
	[image_name] [nvarchar](50) NULL,
	[ref_doc_type] [varchar](30) NULL,
	[ref_doc_code] [varchar](30) NULL,
	[image_file] [text] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
 CONSTRAINT [pk_pic_image] PRIMARY KEY CLUSTERED 
(
	[image_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project](
	[project_code] [varchar](30) NOT NULL,
	[project_desc] [nvarchar](100) NOT NULL,
	[project_date] [datetime] NULL,
	[project_approve_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[budget] [money] NULL,
	[project_manager] [nvarchar](100) NULL,
	[target_member_join] [int] NULL,
	[active_member_join] [int] NULL,
	[passed_member] [int] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_project] PRIMARY KEY CLUSTERED 
(
	[project_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project_course]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project_course](
	[course_code] [varchar](30) NOT NULL,
	[project_code] [varchar](30) NULL,
	[ctype_code] [char](3) NULL,
	[cgroup_code] [char](3) NULL,
	[course_desc] [nvarchar](100) NOT NULL,
	[course_date] [datetime] NULL,
	[course_approve_date] [datetime] NULL,
	[course_begin] [datetime] NULL,
	[course_end] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[budget] [money] NULL,
	[charge_head] [char](10) NULL,
	[support_head] [char](10) NULL,
	[project_manager] [nvarchar](100) NULL,
	[target_member_join] [int] NULL,
	[active_member_join] [int] NULL,
	[passed_member] [int] NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
 CONSTRAINT [pk_project_course] PRIMARY KEY CLUSTERED 
(
	[course_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project_course_register]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project_course_register](
	[course_code] [varchar](30) NOT NULL,
	[member_code] [varchar](30) NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_project_course_register] PRIMARY KEY CLUSTERED 
(
	[course_code] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project_daily_checklist]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project_daily_checklist](
	[course_code] [varchar](30) NOT NULL,
	[member_code] [varchar](30) NOT NULL,
	[course_date] [datetime] NOT NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_project_daily_checklist] PRIMARY KEY CLUSTERED 
(
	[course_date] ASC,
	[course_code] ASC,
	[member_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project_sponsor]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project_sponsor](
	[spon_code] [varchar](30) NOT NULL,
	[spon_desc] [nvarchar](100) NOT NULL,
	[confirm_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[contactor] [nvarchar](100) NULL,
	[contactor_detail] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_project_sponsor] PRIMARY KEY CLUSTERED 
(
	[spon_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[project_supporter]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[project_supporter](
	[project_code] [varchar](30) NOT NULL,
	[spon_code] [varchar](30) NOT NULL,
	[ref_doc] [varchar](30) NULL,
	[support_budget] [money] NULL,
	[contactor] [nvarchar](100) NULL,
	[contactor_detail] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_project_supporter] PRIMARY KEY CLUSTERED 
(
	[spon_code] ASC,
	[project_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[train_place]    Script Date: 06-06-2559 7:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[train_place](
	[place_code] [varchar](30) NOT NULL,
	[instructor_desc] [nvarchar](100) NOT NULL,
	[confirm_date] [datetime] NULL,
	[ref_doc] [varchar](30) NULL,
	[contactor] [nvarchar](100) NULL,
	[contactor_detail] [nvarchar](500) NULL,
	[x_status] [char](1) NULL,
	[x_note] [nvarchar](50) NULL,
	[x_log] [nvarchar](500) NULL,
	[id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [pk_train_place] PRIMARY KEY CLUSTERED 
(
	[place_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[course_group] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[course_instructor] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[course_train_place] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[course_type] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ini_config] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ini_district] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ini_list_zip] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ini_province] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[ini_subdistrict] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[instructor] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[mem_education] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[mem_health] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[mem_reward] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[mem_social] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[mem_worklist] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[project] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[project_course_register] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[project_daily_checklist] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[project_sponsor] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[project_supporter] ADD  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[train_place] ADD  DEFAULT (newid()) FOR [id]
GO
USE [master]
GO
ALTER DATABASE [PalangPanyaDB] SET  READ_WRITE 
GO
