CREATE TABLE [dbo].[Professor]
(
    [ProfessorId] INT NOT NULL IDENTITY,
    [Nome] NVARCHAR(120),
    CONSTRAINT [PK_Professor] PRIMARY KEY CLUSTERED ([ProfessorId])
);

GO
CREATE TABLE [dbo].[Aluno]
(
    [AlunoId] INT NOT NULL IDENTITY,
    [Nome] NVARCHAR(160) NOT NULL,
	[DataNascimento] DATETIME NOT NULL,
    [ProfessorId] INT NOT NULL,
    CONSTRAINT [PK_Aluno] PRIMARY KEY CLUSTERED ([AlunoId])
);

GO
ALTER TABLE [dbo].[Aluno] ADD CONSTRAINT [FK_ProfessorAlunoId]
    FOREIGN KEY ([ProfessorId]) REFERENCES [dbo].[Professor] ([ProfessorId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO
CREATE INDEX [IFK_ProfessorAlunoId] ON [dbo].[Aluno] ([ProfessorId]);
