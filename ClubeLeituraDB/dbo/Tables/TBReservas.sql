CREATE TABLE [dbo].[TBReservas] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [AmigoId]     INT      NOT NULL,
    [RevistaId]   INT      NOT NULL,
    [DataReserva] DATETIME NOT NULL,
    [Situacao]    INT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBReservas_Amigo] FOREIGN KEY ([AmigoId]) REFERENCES [dbo].[TBAmigos] ([Id]),
    CONSTRAINT [FK_TBReservas_Revista] FOREIGN KEY ([RevistaId]) REFERENCES [dbo].[TBRevistas] ([Id])
);

