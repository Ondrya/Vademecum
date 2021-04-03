CREATE TABLE [Param].[Producer] (
    [id_prod]      INT            IDENTITY (1, 1) NOT NULL,
    [name_prod]    NVARCHAR (200) NULL,
    [address_prod] NVARCHAR (300) NULL,
    [phone_prod]   VARCHAR (100)  NULL,
    [web_prod]     NVARCHAR (100) NULL,
    [email_prod]   NVARCHAR (100) NULL,
    [spec_prod]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_id_prod] PRIMARY KEY CLUSTERED ([id_prod] ASC)
);

