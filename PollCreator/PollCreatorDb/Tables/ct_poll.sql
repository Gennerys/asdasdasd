CREATE TABLE [dbo].[ct_poll]
(
	[PK_poll_id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [title] NVARCHAR(100) NULL, 
    [is_single_option] BIT NULL , 
    [is_published] BIT NOT NULL, 
    [poll_id] NVARCHAR(50) NOT NULL, 
    [editor_token] NVARCHAR(50) NOT NULL
)
