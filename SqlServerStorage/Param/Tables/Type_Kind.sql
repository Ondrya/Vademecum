CREATE TABLE [Param].[Type_Kind] (
    [id_type] INT NOT NULL,
    [id_kind] INT NOT NULL,
    CONSTRAINT [PK_Type_Kind] PRIMARY KEY CLUSTERED ([id_type] ASC, [id_kind] ASC),
    CONSTRAINT [FK_Type_Kind_Kind] FOREIGN KEY ([id_kind]) REFERENCES [Param].[Kind] ([id_kind]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Type_Kind_Type] FOREIGN KEY ([id_type]) REFERENCES [Param].[Type] ([id_type]) ON DELETE CASCADE ON UPDATE CASCADE
);

