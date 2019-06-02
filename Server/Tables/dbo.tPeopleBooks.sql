CREATE TABLE dbo.tPeopleBooks(
	[ID] [int] IDENTITY(1,1) not null,
	[BookID] [int] NOT NULL,
	[PeopleID] [int] NOT NULL)

ALTER TABLE dbo.tPeopleBooks
	WITH CHECK ADD CONSTRAINT [tPeopleBooks_BookID_tBook_ID] FOREIGN KEY ([BookID]) REFERENCES [dbo].[tBook]([ID]) ON

UPDATE CASCADE;

ALTER TABLE dbo.tPeopleBooks
	WITH CHECK ADD CONSTRAINT [tPeopleBooks_PeopleID_tPeople_ID] FOREIGN KEY ([PeopleID]) REFERENCES [dbo].[tPeople]([ID]) ON

UPDATE CASCADE;
