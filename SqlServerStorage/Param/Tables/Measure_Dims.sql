CREATE TABLE [Param].[Measure_Dims] (
    [id_dim_measure] INT            IDENTITY (1, 1) NOT NULL,
    [dim_measure]    NVARCHAR (50)  NULL,
    [dim_symbol]     NVARCHAR (50)  NULL,
    [dim_spec]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Measure_Dims] PRIMARY KEY CLUSTERED ([id_dim_measure] ASC)
);



