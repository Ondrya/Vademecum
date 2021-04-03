CREATE TABLE [Dev].[Device_Envi] (
    [id_envi] INT NOT NULL,
    [id]      INT NOT NULL,
    CONSTRAINT [PK_Av_Envi] PRIMARY KEY CLUSTERED ([id_envi] ASC, [id] ASC),
    CONSTRAINT [FK_Avia_Envi_Avia_Device] FOREIGN KEY ([id]) REFERENCES [Dev].[Device] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Avia_Envi_Enviroment] FOREIGN KEY ([id_envi]) REFERENCES [Param].[Enviroment] ([id_envi]) ON DELETE CASCADE ON UPDATE CASCADE
);

