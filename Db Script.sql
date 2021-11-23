USE [OnlineShoppingSystem]
GO
/****** Object:  Table [dbo].[AdminLogin]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminLogin](
	[AdminID] [varchar](10) NOT NULL,
	[AdminPassword] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankDetails]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankDetails](
	[billId] [int] IDENTITY(5001,1) NOT NULL,
	[CardHoldername] [varchar](20) NULL,
	[cardtye] [varchar](20) NULL,
	[cvv] [varchar](10) NULL,
	[balance] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[billId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [varchar](10) NOT NULL,
	[CategoryName] [varchar](25) NOT NULL,
	[Descriptions] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(2000,1) NOT NULL,
	[CustomerName] [varchar](20) NULL,
	[Address] [varchar](20) NULL,
	[Phonenumber] [varchar](20) NULL,
	[EmailID] [varchar](20) NULL,
	[Password] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1001,1) NOT NULL,
	[ProductName] [varchar](20) NOT NULL,
	[CategoryId] [varchar](10) NULL,
	[RetailerId] [varchar](10) NULL,
	[Description] [varchar](25) NULL,
	[ProductImage] [varchar](50) NULL,
	[Features] [varchar](25) NULL,
	[AvailableProduct] [int] NULL,
	[Price] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOrderDetails]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrderDetails](
	[OrderId] [int] NULL,
	[CustomerId] [int] NULL,
	[BillId] [int] NULL,
	[Customername] [varchar](20) NULL,
	[OrderedDate] [date] NULL,
	[Address] [varchar](20) NULL,
	[Productname] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOrders]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrders](
	[OrderId] [int] IDENTITY(4001,1) NOT NULL,
	[ProductId] [int] NULL,
	[Productname] [varchar](20) NULL,
	[quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[OrderedDate] [date] NULL,
	[CustomerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Retailer]    Script Date: 23-11-2021 08:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Retailer](
	[RetailerId] [varchar](10) NOT NULL,
	[RetailerName] [varchar](20) NULL,
	[Gender] [varchar](10) NULL,
	[Address] [varchar](20) NULL,
	[Phonenumber] [varchar](20) NULL,
	[EmailID] [varchar](20) NULL,
	[Password] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RetailerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AdminLogin] ([AdminID], [AdminPassword]) VALUES (N'Admin', N'Admin')
GO
SET IDENTITY_INSERT [dbo].[BankDetails] ON 

INSERT [dbo].[BankDetails] ([billId], [CardHoldername], [cardtye], [cvv], [balance]) VALUES (5024, N'Balamurali', N'Debitcard', N'5321', CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[BankDetails] ([billId], [CardHoldername], [cardtye], [cvv], [balance]) VALUES (5025, N'Balamurali', N'Debitcard', N'5321', CAST(5000 AS Decimal(18, 0)))
INSERT [dbo].[BankDetails] ([billId], [CardHoldername], [cardtye], [cvv], [balance]) VALUES (5026, N'Balamurali', N'Debitcard', N'5321', CAST(6000 AS Decimal(18, 0)))

SET IDENTITY_INSERT [dbo].[BankDetails] OFF
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'EEE1001', N'PowerBank', N'HP')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'IP1001', N'Iphones', N'New Arrival..')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'LAP1001', N'Laptops', N'Brands:HP,DELL')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'Lap2012', N'Laptops', N'Brands:HP,DELL')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'MOB2019', N'MobilePhones', N'Android Mobiles,4GB RAM,STORAGE 32GB')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [Descriptions]) VALUES (N'WAT2019', N'Watch', N'COD')
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [Address], [Phonenumber], [EmailID], [Password]) VALUES (2056, N'BalaMurali T', N'Chennai', N'9003915522', N'bala@gmail.com', N'1212')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [Address], [Phonenumber], [EmailID], [Password]) VALUES (2059, N'Althaf', N'Chennai', N'9003915522', N'Althaf@gmail.com', N'1818')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [Address], [Phonenumber], [EmailID], [Password]) VALUES (2061, N'Althaf', N'Chennai', N'9003915588', N'Althaf1@gmail.com', N'1212')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [RetailerId], [Description], [ProductImage], [Features], [AvailableProduct], [Price]) VALUES (1006, N'Dell Gaming Laptop', N'LAP1001', N'1001', N'Holiday Offers', N'Hp21590731.jfif', N'Gaming Experience:High', 7, CAST(100000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [RetailerId], [Description], [ProductImage], [Features], [AvailableProduct], [Price]) VALUES (1018, N'Iphone', N'EEE1001', N'1001', N'Holiday Offers', N'iphone21582523.jfif', N'Gaming Experience:High', 10, CAST(500000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ProductId], [ProductName], [CategoryId], [RetailerId], [Description], [ProductImage], [Features], [AvailableProduct], [Price]) VALUES (1023, N'HP GAMING', N'Lap2012', N'1001', N'Holiday Offers', N'iphone21262278.jfif', N'Good', 5, CAST(11111111 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[ProductOrderDetails] ([OrderId], [CustomerId], [BillId], [Customername], [OrderedDate], [Address], [Productname]) VALUES (4067, 2056, 5024, N'Balamurali', CAST(N'2021-11-21' AS Date), N'chennai', N'DellGamingLaptop')
INSERT [dbo].[ProductOrderDetails] ([OrderId], [CustomerId], [BillId], [Customername], [OrderedDate], [Address], [Productname]) VALUES (4072, 2056, 5028, N'Balamurali', CAST(N'2021-11-21' AS Date), N'chennai', N'Iphone')

GO
SET IDENTITY_INSERT [dbo].[ProductOrders] ON 

INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [Productname], [quantity], [Price], [OrderedDate], [CustomerId]) VALUES (4067, 1006, N'Dell Gaming Laptop', 4, CAST(400000 AS Decimal(18, 0)), CAST(N'2021-11-21' AS Date), 2056)
INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [Productname], [quantity], [Price], [OrderedDate], [CustomerId]) VALUES (4072, 1018, N'Iphone', 3, CAST(1500000 AS Decimal(18, 0)), CAST(N'2021-11-21' AS Date), 2059)
INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [Productname], [quantity], [Price], [OrderedDate], [CustomerId]) VALUES (4073, 1006, N'Dell Gaming Laptop', 2, CAST(200000 AS Decimal(18, 0)), CAST(N'2021-11-22' AS Date), 2056)
INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [Productname], [quantity], [Price], [OrderedDate], [CustomerId]) VALUES (4077, 1006, N'Dell Gaming Laptop', 2, CAST(200000 AS Decimal(18, 0)), CAST(N'2021-11-22' AS Date), 2056)
SET IDENTITY_INSERT [dbo].[ProductOrders] OFF
GO
INSERT [dbo].[Retailer] ([RetailerId], [RetailerName], [Gender], [Address], [Phonenumber], [EmailID], [Password]) VALUES (N'1001', N'Balamurali', N'Male', N'Vellore', N'9003915522', N'balam@g.com', N'Bala@1')
INSERT [dbo].[Retailer] ([RetailerId], [RetailerName], [Gender], [Address], [Phonenumber], [EmailID], [Password]) VALUES (N'VLR1001', N'Balamurali', N'Male', N'Vellore', N'9003915523', N'bala@gmail1.com', N'1212')
INSERT [dbo].[Retailer] ([RetailerId], [RetailerName], [Gender], [Address], [Phonenumber], [EmailID], [Password]) VALUES (N'VLR1002', N'Althaf', N'Male', N'Chennai', N'9876543210', N'Althaf1@gmail.com', N'Althaf!1')
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([RetailerId])
REFERENCES [dbo].[Retailer] ([RetailerId])
GO
ALTER TABLE [dbo].[ProductOrderDetails]  WITH CHECK ADD FOREIGN KEY([BillId])
REFERENCES [dbo].[BankDetails] ([billId])
GO
ALTER TABLE [dbo].[ProductOrderDetails]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ProductOrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[ProductOrders] ([OrderId])
GO
ALTER TABLE [dbo].[ProductOrders]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ProductOrders]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
