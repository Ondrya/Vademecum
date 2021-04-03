CREATE TABLE [Dev].[Device_Measure] (
    [id]         INT NOT NULL,
    [id_measure] INT NOT NULL,
    CONSTRAINT [PK_Avia_Measure] PRIMARY KEY CLUSTERED ([id] ASC, [id_measure] ASC),
    CONSTRAINT [FK_Avia_Measure_Avia_Device] FOREIGN KEY ([id]) REFERENCES [Dev].[Device] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Avia_Measure_Measure] FOREIGN KEY ([id_measure]) REFERENCES [Param].[Measure] ([id_measure]) ON DELETE CASCADE ON UPDATE CASCADE
);

