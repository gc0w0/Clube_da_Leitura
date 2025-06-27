CREATE TABLE [dbo].[TBAmigos] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Nome]            VARCHAR (100) NOT NULL,
    [NomeResponsavel] VARCHAR (100) NOT NULL,
    [Telefone]        VARCHAR (15)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

