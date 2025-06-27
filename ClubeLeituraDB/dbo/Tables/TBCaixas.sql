CREATE TABLE [dbo].[TBCaixas] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Etiqueta] VARCHAR (50) NOT NULL,
    [Cor]      INT          NOT NULL,
    [Dias]     INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

