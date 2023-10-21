USE [shopping]

/* Elimino la tablas si existen */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[TagComment]')AND type in ('U'))
DROP TABLE [TagComment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]')AND type in ('U'))
DROP TABLE [Tag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[OrderLine]') AND type in ('U'))
DROP TABLE [OrderLine]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Order]') AND type in ('U'))
DROP TABLE [Order]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CreditCard]') AND type in ('U'))
DROP TABLE [CreditCard]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Movie]') AND type in ('U'))
DROP TABLE [Movie]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Book]') AND type in ('U'))
DROP TABLE [Book]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') AND type in ('U'))
DROP TABLE [Product]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[User]') AND type in ('U'))
DROP TABLE [User]
GO

/* Creación de tablas */
CREATE TABLE [User] (
	userId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(150) NOT NULL,
	[password] varchar(150) NOT NULL,
	firstName varchar(150) NOT NULL,
	lastName varchar(150) NOT NULL,
	email varchar(150) NOT NULL,
	postalAddress varchar(150) NOT NULL,
	country varchar(150) NOT NULL,
	[language] varchar(150) NOT NULL,
	[admin] bit NOT NULL,

	CONSTRAINT [PK_User] PRIMARY KEY (userId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)
PRINT N'Table User created.'
GO

CREATE TABLE CreditCard (
	creditCardId bigint IDENTITY(1,1) NOT NULL,
	userId bigint NOT NULL,
	creditType varchar(30) NOT NULL,
	number bigint NOT NULL,
	verificationCode int NOT NULL,
	expirationDate date NOT NULL,
	defaultCard bit NOT NULL,

	CONSTRAINT [PK_CreditCard] PRIMARY KEY (creditCardId),
	CONSTRAINT [FK_CreditCardUserId] FOREIGN KEY(userId) REFERENCES [User](userId) ON DELETE CASCADE 
)
PRINT N'Table CreditCard created.'
GO


CREATE TABLE [Order] (
	orderId bigint IDENTITY(1,1) NOT NULL,
	creditCardId bigint NOT NULL,
	userId bigint NOT NULL, 
	orderDate datetime NOT NULL,
	postalAddressOrder varchar(150) NOT NULL,
	orderName varchar(100) NOT NULL,
	totalPrice decimal(10,2) NOT NULL,

	CONSTRAINT [PK_Order] PRIMARY KEY (orderId),
	CONSTRAINT [FK_OrderCreditCardId] FOREIGN KEY(creditCardId) REFERENCES CreditCard(creditCardId) ON DELETE NO ACTION,
	CONSTRAINT [FK_OrderUserId] FOREIGN KEY(userId) REFERENCES [User](userId) ON DELETE CASCADE
)

PRINT N'Table Order created.'
GO

CREATE TABLE OrderLine (
	orderLineId bigint IDENTITY(1,1) NOT NULL,
	productId bigint NOT NULL,
	orderId bigint NOT NULL,
	productPrice decimal(10,2) NOT NULL,
	quantity int NOT NULL,
	isGift bit NOT NULL,
	
	CONSTRAINT [PK_OrderLine] PRIMARY KEY (orderLineId),
	CONSTRAINT [FK_OrderLineOrderId] FOREIGN KEY(orderId) REFERENCES [Order](orderId) ON DELETE CASCADE,
)
PRINT N'Table OrderLine created.'
GO

CREATE TABLE Category (
	categoryId bigint IDENTITY(1,1) NOT NULL,
	categoryName varchar(50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId),
)
PRINT N'Table Category created.'
GO

CREATE TABLE Product (
	productId bigint IDENTITY(1,1) NOT NULL,
	categoryId bigint NOT NULL,
	[name] varchar(50) NOT NULL,
	price decimal(10,2) NOT NULL,
	createDate date NOT NULL,
	stock int NOT NULL,
	[description] varchar(1500) NOT NULL,

	CONSTRAINT [PK_Product] PRIMARY KEY (productId),
	CONSTRAINT [FK_ProductCateogryId] FOREIGN KEY(categoryId) REFERENCES Category(categoryId) ON DELETE CASCADE,
)
PRINT N'Table Product created.'
GO

CREATE TABLE Book (
	productId bigint NOT NULL,
	editorial varchar(50) NOT NULL,
	ISBN varchar(50) NOT NULL,
	edition int NOT NULL,
	pages int NOT NULL,
	publicationDate date NOT NULL,

	CONSTRAINT [PK_Book] PRIMARY KEY (productId),
	CONSTRAINT [FK_BookProductId] FOREIGN KEY(productId) REFERENCES Product(productId) ON DELETE CASCADE,
)
PRINT N'Table Book created.'
GO

CREATE TABLE Movie (
	productId bigint NOT NULL,
	title varchar(50) NOT NULL,
	runtime time NOT NULL,
	creationDate date NOT NULL,

	CONSTRAINT [PK_Movie] PRIMARY KEY (productId),
	CONSTRAINT [FK_MovieProductId] FOREIGN KEY(productId) REFERENCES Product(productId) ON DELETE CASCADE,
)
PRINT N'Table Movie created.'
GO

CREATE TABLE Tag (
	tagId bigint IDENTITY(1,1) NOT NULL,
	tagUses int NOT NULL,
	[name] varchar(30) NOT NULL

	CONSTRAINT [PK_Tag] PRIMARY KEY (tagId)
)
PRINT N'Table Tag created.'
GO

CREATE TABLE Comment (
	commentId bigint IDENTITY(1,1) NOT NULL,
	userId bigint NOT NULL,
	productId bigint NOT NULL,
	commentText varchar(255) NOT NULL,
	createDate datetime NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_CommentUserId] FOREIGN KEY(userId) REFERENCES [User](userId) ON DELETE CASCADE,
	CONSTRAINT [FK_CommentProductId] FOREIGN KEY(productId) REFERENCES Product(productId) ON DELETE CASCADE,
)
PRINT N'Table Comment created.'
GO

CREATE TABLE TagComment (
	tagId bigint NOT NULL,
	commentId bigint NOT NULL,

	CONSTRAINT [PK_TagComment] PRIMARY KEY (tagId, commentId),
	CONSTRAINT [FK_TagCommentTagId] FOREIGN KEY(tagId) REFERENCES Tag(tagId) ON DELETE CASCADE,
	CONSTRAINT [FK_TagCommentCommentId] FOREIGN KEY(commentId) REFERENCES Comment(commentId) ON DELETE CASCADE,

)
PRINT N'Table TagComment created.'
GO

/* Añado la referencia a la clave foránea ya que sino haría referencia a una tabla que todavía no existe */
ALTER TABLE OrderLine
ADD CONSTRAINT FK_OrderLineProductId
FOREIGN KEY (productId) REFERENCES Product(productId) ON DELETE CASCADE;
PRINT N'Alter table OrderLine --> ADD CONSTRAINT FK_OrderLineProductId'
GO

/* Categorías */
INSERT INTO Category VALUES ('Libros');
INSERT INTO Category VALUES ('Películas');

/* Productos (ID, CategoryId, name, price, createDate, stock, description) */
INSERT INTO Product VALUES (2, 'Gladiator Movie DVD', 3.99, '2018-06-18', 1, 'El general romano Máximo es el soporte más leal del emperador Marco Aurelio, que lo ha conducido de victoria en victoria. Sin embargo, Cómodo, el hijo de Marco Aurelio, está celoso del prestigio de Máximo y aún más del amor que su padre siente por él.');
INSERT INTO Product VALUES (1, 'Tintín en el Tíbet Book', 12.90, '2018-06-18', 1, 'Después de leer la noticia de un accidente aéreo en el Himalaya, Tintín tiene un sueño donde su joven amigo Tchang herido le pide ayuda medio enterrado en la nieve. Al día siguiente se entera por el diario de que Tchang viajaba en el avión siniestrado, y que no han encontrado supervivientes.');
INSERT INTO Product VALUES (2, 'Avatar Movie Blu Ray', 4.99, '2019-12-18', 4, 'Entramos en el mundo Avatar de la mano de Jake Sully, un ex-Marine en silla de ruedas, que ha sido reclutado para viajar a Pandora, donde existe un mineral raro y muy preciado que puede solucionar la crisis energética existente en la Tierra.');
INSERT INTO Product VALUES (1, 'En el Reino de la Fantasía Book', 18.95, '2012-06-18', 5, 'Geronimo Stilton viaja al Reino de la fantasía y descubre cómo huelen las brujas, las sirenas, los dragones, las hadas, los duendes...');
INSERT INTO Product VALUES (2, 'El Resplandor Movie Blu Ray', 1.99, '2005-12-19', 0, 'Jack Torrance es un hombre que se muda con su familia a un hotel aislado que debe cuidar, con la esperanza de salir del bloqueo creativo de su escritura. Mientras Jack no puede escapar del bloqueo, las visiones psíquicas de su hijo van en aumento.');
INSERT INTO Product VALUES (1, 'Robinson Crusoe Book', 18.95, '2001-06-18', 2, 'Cuenta la historia de un hombre que naufragó cerca de una isla desierta y pasó en ella más de veinticinco años. El protagonista tiene que empezar a vivir prácticamente desde cero, enfrentándose a los desafíos de la vida salvaje y a la terrible carga de la soledad.');
INSERT INTO Product VALUES (2, 'Gladiator Movie Blu Ray', 5.99, '2016-06-18', 0, 'El general romano Máximo es el soporte más leal del emperador Marco Aurelio, que lo ha conducido de victoria en victoria. Sin embargo, Cómodo, el hijo de Marco Aurelio, está celoso del prestigio de Máximo y aún más del amor que su padre siente por él.');
INSERT INTO Product VALUES (1, 'Don Quijote de la Mancha V.Original', 199.95, '2000-04-06', 3, 'El ingenioso hidalgo don Quijote de la Mancha narra las aventuras de Alonso Quijano, un hidalgo pobre que de tanto leer novelas de caballería acaba enloqueciendo y creyendo ser un caballero andante, nombrándose a sí mismo como don Quijote de la Mancha.');
INSERT INTO Product VALUES (2, 'Documental de ballenas y peces', 10.99, '2012-01-01', 1, 'Los cineastas Jacques Perrin y Jacques Cluzaud exploran las maravillas y la dura realidad de la vida que existe debajo de las olas.');
INSERT INTO Product VALUES (1, 'Activity Book 3º Primaria', 23.37, '2022-06-05', 1, 'Una colección de libros de texto cuidadosamente elaborada para niños y niñas de EDUCACION PRIMARIA, con temas seleccionados para su interés.');
INSERT INTO Product VALUES (2, 'Pokemon Mewtwo Contrataca', 2.99, '2016-06-18', 0, 'Ash y sus compañeros Pokémon se enfrentan a una criatura clonada que le ha declarado la guerra a sus oponentes.');
/* Movie (productID, title, runtime, creationDate) */
INSERT INTO Movie VALUES (1, 'Gladiator', '2:30', '2000-05-17');
INSERT INTO Movie VALUES (3, 'Avatar', '2:41', '2009-12-18');
INSERT INTO Movie VALUES (5, 'El Resplandor', '2:26', '1980-12-19');
INSERT INTO Movie VALUES (7, 'Gladiator', '2:30', '2000-05-17');
INSERT INTO Movie VALUES (9, 'Océanos', '1:24', '2010-04-23');
INSERT INTO Movie VALUES (11, 'Mew vs Mewtwo', '1:36', '2000-04-14');
/* Book (productID, editorial, ISBN, edition, pages, publicationDate) */
INSERT INTO Book VALUES (2, 'Juventud', '978842610382-6', 30, 64, '1989-10-05');
INSERT INTO Book VALUES (4, 'Destino', '978-84-08-06099-4', 14, 384, '2011-06-18');
INSERT INTO Book VALUES (6, 'Austral', '958-64-88-07049-1', 23, 512, '1719-04-25');
INSERT INTO Book VALUES (8, 'Molino', '978-14-96-17157-3', 1, 462, '1605-05-17');
INSERT INTO Book VALUES (10, 'Oxford', '978-01-94-07237-3', 22, 114, '2022-05-05');

/* Usuarios */
/* loginName, password, firstName, lastName, email, postalAddress, country, language, admin */
INSERT INTO [User] VALUES ('admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=',
'Admin','Admin Pro', 'admin@udc.es', 'Rúa Pepito, A Coruña', 'España' , 'Español', 1);
INSERT INTO [User] VALUES ('usuario', 'klDiIsTHHwxY1MVLUKiAoxLp+f7VXVw6oLDoYN7ZkWU=',
'Usuario','Usuario Pro', 'usuario@udc.es', 'Rúa Pepito, A Coruña', 'España' , 'Español', 0);
