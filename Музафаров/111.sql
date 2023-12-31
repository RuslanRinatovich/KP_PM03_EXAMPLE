USE [master]
GO
/****** Object:  Database [AfishaBD]    Script Date: 19.01.2021 15:01:14 ******/
CREATE DATABASE [AfishaBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AfishaBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AfishaBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AfishaBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AfishaBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AfishaBD] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AfishaBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AfishaBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AfishaBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AfishaBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AfishaBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AfishaBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [AfishaBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AfishaBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AfishaBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AfishaBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AfishaBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AfishaBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AfishaBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AfishaBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AfishaBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AfishaBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AfishaBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AfishaBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AfishaBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AfishaBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AfishaBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AfishaBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AfishaBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AfishaBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AfishaBD] SET  MULTI_USER 
GO
ALTER DATABASE [AfishaBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AfishaBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AfishaBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AfishaBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AfishaBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AfishaBD] SET QUERY_STORE = OFF
GO
USE [AfishaBD]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 19.01.2021 15:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventTable]    Script Date: 19.01.2021 15:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTable](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](1000) NOT NULL,
	[Info] [nvarchar](1000) NOT NULL,
	[EventDate] [date] NOT NULL,
	[EventPlace] [nvarchar](100) NOT NULL,
	[Duration] [int] NOT NULL,
	[Photo] [nvarchar](100) NOT NULL,
	[StatusId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_EventTable] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 19.01.2021 15:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](100) NOT NULL,
	[Color] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19.01.2021 15:01:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Кино')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Театр')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Концерт')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Выставки')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (5, N'Спорт')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[EventTable] ON 

INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (1, N'Чудо-женщина: 1984', N'Галь Гадот борется с дикой кошкой и бизнесменом в 80-х
1984 год, принцесса Диана (Галь Гадот), покинувшая остров амазонок во времена Первой мировой, адаптировалась в мире людей и готова сражаться с, вероятно, неплохими личностями, у которых что-то пошло не так. В роли антагонистов ожидаются бизнесмен Максвел Лорд (Педро Паскаль из «Нарко»), награжденный телепатией, а также злодейка Гепарда (Кристен Уиг) — ранее антрополог по фамилии Минерва, которую древнее проклятие обратило в кровожадное животное. Снимает сиквел все та же Патти Дженкинс, за окном — навязшие на зубах 80-е (да, опять), в киновселенной DC — хаос и подготовка к перезапуску.L', CAST(N'2021-01-14' AS Date), N'Кинотеатры', 150, N'wonder.jpg', 1, 1)
INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (4, N'IOWA концерт', N'Сменившая в 2015 году могилевскую прописку на питерскую группа IOWA играет прифанкованный инди-поп с запоминающимися текстами солистки Екатерины Иванчиковой и является обладателем множества музыкальных премий страны. Коллектив стал широко известен своими песнями к отечественным сериалам «Физрук», «Кухня» и «Сладкая жизнь», а особенной популярностью в свое время пользовался их главный хит «Улыбайся».', CAST(N'2021-02-07' AS Date), N'Главклуб', 180, N'11IOWA.jpg', 3, 3)
INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (5, N'Скриптонит', N'Anacondaz осознанно идут на эпатаж — поют оды апокалипсису, ругаются, издеваются, восхваляют проституцию и отрабатывают многие другие трансгрессивные темы. Происходит все это под энергичные биты и громкие гитарные риффы. Впрочем, помимо всего прочего в их треках можно обнаружить обличение лицемерия, борьбу с нетерпимостью и ироничные гей-атаки на патриархальный порядок.', CAST(N'2021-04-25' AS Date), N'Korston Club Hotel', 0, N'1script.jpg', 3, 3)
INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (6, N'Хоккейный матч Ак Барс -  Торпедо', N'Главные судьи Ансонс Андрис, Соин Александр', CAST(N'2021-01-17' AS Date), N'Татнефть-Арена', 120, N'1bars.jpg', 4, 5)
INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (7, N'Мэхэббэт FM(Спектакль)', N'Они работают на радио. Он — диджей, она — звукорежиссер. Они любят друг-друга, но мучительно долго идут к осознанию своих чувств. ', CAST(N'2021-02-26' AS Date), N'Татарский театр им.Камала', 180, N'1мм.jpg', 3, 2)
INSERT [dbo].[EventTable] ([EventId], [EventName], [Info], [EventDate], [EventPlace], [Duration], [Photo], [StatusId], [CategoryId]) VALUES (8, N'Когда зажигаются елки. Шедевры новогодней анимации в России ХХ века', N'Выставка лучшей новогодней отечественной анимации XX века', CAST(N'2021-01-11' AS Date), N'Манеж', 6000, N'1ff.jpg', 1, 4)
SET IDENTITY_INSERT [dbo].[EventTable] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusId], [StatusName], [Color]) VALUES (1, N'Идет', N'#FFADFF2F')
INSERT [dbo].[Status] ([StatusId], [StatusName], [Color]) VALUES (3, N'Не началось', N'#FFFFFF00')
INSERT [dbo].[Status] ([StatusId], [StatusName], [Color]) VALUES (4, N'Завершилось', N'#FFF08080')
SET IDENTITY_INSERT [dbo].[Status] OFF
ALTER TABLE [dbo].[EventTable]  WITH CHECK ADD  CONSTRAINT [FK_EventTable_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[EventTable] CHECK CONSTRAINT [FK_EventTable_Category]
GO
ALTER TABLE [dbo].[EventTable]  WITH CHECK ADD  CONSTRAINT [FK_EventTable_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[EventTable] CHECK CONSTRAINT [FK_EventTable_Status]
GO
USE [master]
GO
ALTER DATABASE [AfishaBD] SET  READ_WRITE 
GO
