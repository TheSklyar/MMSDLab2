insert into dbo.tRevisionLog values ('CHANGE_VERSION', 'APPLICATION', '1.0.0.0')
GO
CREATE LOGIN Administrator WITH PASSWORD = '123456789'
,DEFAULT_DATABASE = [TestDB]
GO
USE TestDB;
CREATE USER Administrator FOR LOGIN Administrator;
GO
EXEC sp_addrolemember 'db_owner', 'Administrator'
go
CREATE ROLE LibUser
go
GRANT execute on Init TO LibUser;

grant select on tPeople to LibUser;
grant update on tPeople to LibUser;
grant delete on tPeople to LibUser;

grant select on [tBook] to LibUser;
grant update on [tBook] to LibUser;
grant delete on [tBook] to LibUser;

grant select on [tPeopleBooks] to LibUser;
grant update on [tPeopleBooks] to LibUser;
grant delete on [tPeopleBooks] to LibUser;

