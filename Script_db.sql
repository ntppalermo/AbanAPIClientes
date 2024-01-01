USE [master]
GO
/****** Object:  Database [AbanDB]  ******/
CREATE DATABASE [AbanDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AbanDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AbanDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AbanDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AbanDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AbanDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AbanDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AbanDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AbanDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AbanDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AbanDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AbanDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AbanDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AbanDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AbanDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AbanDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AbanDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AbanDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AbanDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AbanDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AbanDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AbanDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AbanDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AbanDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AbanDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AbanDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AbanDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AbanDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AbanDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AbanDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AbanDB] SET  MULTI_USER 
GO
ALTER DATABASE [AbanDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AbanDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AbanDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AbanDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AbanDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AbanDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AbanDB] SET QUERY_STORE = OFF
GO
USE [AbanDB]
GO
/****** Object:  Table [dbo].[Clientes]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] NOT NULL,
	[Nombres] [varchar](255) NOT NULL,
	[Apellidos] [varchar](255) NOT NULL,
	[FechaNacimiento] [date] NULL,
	[CUIT] [varchar](20) NULL,
	[Domicilio] [varchar](255) NULL,
	[Celular] [varchar](20) NULL,
	[Email] [varchar](255) NULL,
 CONSTRAINT [PK__Clientes__3214EC071536B818] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (1, N'Juan', N'Perez', CAST(N'1990-05-15' AS Date), N'20-12345678-9', N'Calle 123', N'123-456-7890', N'juan@gmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (2, N'Maria', N'Lopez', CAST(N'1985-08-22' AS Date), N'27-98765432-1', N'Avenida 456', N'987-654-3210', N'maria@yahoo.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (3, N'Carlos', N'Gomez', CAST(N'1978-12-10' AS Date), N'30-87654321-0', N'Plaza Principal', N'555-123-4567', N'carlos@hotmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (4, N'Laura', N'Rodriguez', CAST(N'1995-03-18' AS Date), N'22-34567890-1', N'Callejon 789', N'333-999-8888', N'laura@email.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (5, N'Alejandro', N'Martinez', CAST(N'1980-07-05' AS Date), N'25-67890123-4', N'Camino 567', N'777-444-5555', N'alejandro@gmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (6, N'Ana', N'Fernandez', CAST(N'1988-11-30' AS Date), N'21-23456789-0', N'Paseo 890', N'888-555-6666', N'ana@outlook.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (7, N'Pedro', N'Sanchez', CAST(N'1972-09-25' AS Date), N'29-01234567-8', N'Ruta 123', N'666-777-8888', N'pedro@gmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (8, N'Sofia', N'Ramirez', CAST(N'1992-01-12' AS Date), N'29-01234544-3', N'Calle Mayor', N'111-222-3333', N'sofia@hotmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (10, N'Daniel', N'Pereira', CAST(N'1985-10-12' AS Date), N'29-01234467-1', N'Rivadavia 1212', N'111-5555-777', N'mail@yahoo.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Apellidos], [FechaNacimiento], [CUIT], [Domicilio], [Celular], [Email]) VALUES (12, N'Domingo', N'Osvaldo', CAST(N'1999-12-28' AS Date), N'20-06464653-3', N'Federico Lacroze 1546', N'156-77777-222', N'do@email.com')
GO
USE [master]
GO
ALTER DATABASE [AbanDB] SET  READ_WRITE 
GO
