CREATE TABLE [dbo].[Poll]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [IsSingleOption] BIT NOT NULL , 
    [IsPublished] BIT NOT NULL, 
    [PollId] NVARCHAR(50) NOT NULL, 
    [EditorToken] NVARCHAR(50) NOT NULL
)
