CREATE TABLE [dbo].[TBRevistas] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]        VARCHAR (100) NOT NULL,
    [NumeroEdicao]  INT           NULL,
    [AnoPublicacao] INT           NULL,
    [Status]        INT           NULL,
    [CaixaId]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBRevistas_Caixa] FOREIGN KEY ([CaixaId]) REFERENCES [dbo].[TBCaixas] ([Id])
);

