CREATE TABLE [dbo].[TBMultas] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [EmprestimoId] INT             NOT NULL,
    [Valor]        DECIMAL (10, 2) NULL,
    [Situacao]     INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBMultas_Emprestimo] FOREIGN KEY ([EmprestimoId]) REFERENCES [dbo].[TBEmprestimos] ([Id])
);

