CREATE TABLE [dbo].[TBEmprestimos] (
    [Id]                    INT      IDENTITY (1, 1) NOT NULL,
    [AmigoId]               INT      NOT NULL,
    [RevistaId]             INT      NOT NULL,
    [DataEmprestimo]        DATE     NOT NULL,
    [DataDevolucao]         DATE     NULL,
    [DataPrevistaDevolucao] DATETIME NULL,
    [Situacao]              INT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBEmprestimos_Amigo] FOREIGN KEY ([AmigoId]) REFERENCES [dbo].[TBAmigos] ([Id]),
    CONSTRAINT [FK_TBEmprestimos_Revista] FOREIGN KEY ([RevistaId]) REFERENCES [dbo].[TBRevistas] ([Id])
);

