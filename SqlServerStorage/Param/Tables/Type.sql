CREATE TABLE [Param].[Type] (
    [id_type]   INT            IDENTITY (1, 1) NOT NULL,
    [name_type] VARCHAR (100)  NOT NULL,
    [spec_type] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED ([id_type] ASC)
);

