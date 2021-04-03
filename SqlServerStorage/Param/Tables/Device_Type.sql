CREATE TABLE [Param].[Device_Type] (
    [id_device]   INT           IDENTITY (1, 1) NOT NULL,
    [device_type] VARCHAR (100) NULL,
    CONSTRAINT [PK_Device_Type] PRIMARY KEY CLUSTERED ([id_device] ASC)
);

