CREATE TABLE [Dev].[Device_Lit] (
    [id]     INT NOT NULL,
    [id_lit] INT NOT NULL,
    CONSTRAINT [PK_Avia_Lit] PRIMARY KEY CLUSTERED ([id] ASC, [id_lit] ASC),
    CONSTRAINT [FK_Avia_Lit_Avia_Device] FOREIGN KEY ([id]) REFERENCES [Dev].[Device] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Avia_Lit_Literature] FOREIGN KEY ([id_lit]) REFERENCES [Param].[Literature] ([id_lit]) ON DELETE CASCADE ON UPDATE CASCADE
);

