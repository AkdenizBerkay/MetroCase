USE [master]
GO

/****** Object:  Database [MetroCase]    Script Date: 15.11.2018 01:15:02 ******/
CREATE DATABASE [MetroCase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MetroCase', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MetroCase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MetroCase_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MetroCase_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [MetroCase] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MetroCase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MetroCase] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MetroCase] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MetroCase] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MetroCase] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MetroCase] SET ARITHABORT OFF 
GO

ALTER DATABASE [MetroCase] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MetroCase] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MetroCase] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MetroCase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MetroCase] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MetroCase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MetroCase] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MetroCase] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MetroCase] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MetroCase] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MetroCase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MetroCase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MetroCase] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MetroCase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MetroCase] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MetroCase] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MetroCase] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MetroCase] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [MetroCase] SET  MULTI_USER 
GO

ALTER DATABASE [MetroCase] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MetroCase] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MetroCase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MetroCase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [MetroCase] SET  READ_WRITE 
GO

