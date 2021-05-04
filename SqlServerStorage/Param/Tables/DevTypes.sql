CREATE TABLE [Param].[DevTypes] (
    [id_device] INT NOT NULL,
    [id_type]   INT NOT NULL,
    CONSTRAINT [PK_DevTypes_1] PRIMARY KEY CLUSTERED ([id_device] ASC, [id_type] ASC),
    CONSTRAINT [FK_DevTypes_Device_Type] FOREIGN KEY ([id_device]) REFERENCES [Param].[Device_Type] ([id_device]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DevTypes_Type] FOREIGN KEY ([id_type]) REFERENCES [Param].[Type] ([id_type]) ON DELETE CASCADE ON UPDATE CASCADE
);

