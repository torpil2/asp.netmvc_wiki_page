USE [bidbwiki]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4.03.2022 15:47:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Description]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Description](
	[desc_id] [int] IDENTITY(1,1) NOT NULL,
	[post_ID] [int] NOT NULL,
	[user_ID] [int] NULL,
	[Description_Text] [varchar](max) NULL,
	[View_order] [int] NULL,
 CONSTRAINT [PK_Description] PRIMARY KEY CLUSTERED 
(
	[desc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[img_id] [int] IDENTITY(1,1) NOT NULL,
	[post_ID] [int] NULL,
	[img_path] [varchar](max) NOT NULL,
	[View_order] [int] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[img_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[post_id] [int] IDENTITY(1,1) NOT NULL,
	[user_ID] [int] NOT NULL,
	[Category_ID] [int] NULL,
	[Post_Name] [varchar](255) NOT NULL,
	[Create_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile_Pictures]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile_Pictures](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_ID] [int] NOT NULL,
	[Path] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Profile_Pictures] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminders](
	[reminder_id] [int] IDENTITY(1,1) NOT NULL,
	[user_ID] [int] NOT NULL,
	[text] [varchar](max) NULL,
	[create_time] [datetime] NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[reminder_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4.03.2022 15:48:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[email] [varchar](255) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[created_time] [datetime] NULL,
	[last_login] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([id], [name]) VALUES (1, N'Bilgilendirme')
INSERT [dbo].[Category] ([id], [name]) VALUES (2, N'DenemeKategori')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [name], [email], [username], [password], [created_time], [last_login]) VALUES (1, N'Bugra Kaan Şener', N'bugraksener@gmail.com', N'torpil2', N'123', CAST(N'2022-03-03T10:47:27.460' AS DateTime), CAST(N'2022-03-04T15:46:24.570' AS DateTime))
INSERT [dbo].[Users] ([user_id], [name], [email], [username], [password], [created_time], [last_login]) VALUES (2, N'admin', N'admin@admin.com', N'admin', N'adminpasswordadmin', CAST(N'2022-03-03T00:00:00.000' AS DateTime), CAST(N'2022-03-04T15:21:51.583' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Description]  WITH CHECK ADD  CONSTRAINT [FK_Description_Posts] FOREIGN KEY([post_ID])
REFERENCES [dbo].[Posts] ([post_id])
GO
ALTER TABLE [dbo].[Description] CHECK CONSTRAINT [FK_Description_Posts]
GO
ALTER TABLE [dbo].[Description]  WITH CHECK ADD  CONSTRAINT [FK_Description_Users] FOREIGN KEY([user_ID])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Description] CHECK CONSTRAINT [FK_Description_Users]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Posts] FOREIGN KEY([post_ID])
REFERENCES [dbo].[Posts] ([post_id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Posts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Category]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([user_ID])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
ALTER TABLE [dbo].[Profile_Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Pictures_Users] FOREIGN KEY([user_ID])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Profile_Pictures] CHECK CONSTRAINT [FK_Profile_Pictures_Users]
GO
ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_Users] FOREIGN KEY([user_ID])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [FK_Reminders_Users]
GO
