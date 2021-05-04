CREATE TABLE [Param].[Control] (
    [id_control]   INT            IDENTITY (1, 1) NOT NULL,
    [name_control] NVARCHAR (100) NULL,
    [spec_control] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_id_control] PRIMARY KEY CLUSTERED ([id_control] ASC)
);

