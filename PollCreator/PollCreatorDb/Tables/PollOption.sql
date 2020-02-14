CREATE TABLE [dbo].[PollOption]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [SerialNumber] INT NOT NULL,
	[Value] NVARCHAR(100) NOT NULL,
    [Poll_PK] NVARCHAR(50) NOT NULL, 
	--CONSTRAINT FK_Poll_PollId FOREIGN KEY([Poll_PK])
	--	REFERENCES Poll(PollId),
	CONSTRAINT CHECK_SerialNumber
		CHECK([SerialNumber] >=1 AND [SerialNumber] <=10)
)
