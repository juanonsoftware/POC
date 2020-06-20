create table Item (
 Id int identity(1,1),
 Name varchar(256) not null,
 Versioning timestamp not null,
 CreatedAt datetime not null,
 UpdatedAt datetime null
)


insert into Item (Name, CreatedAt) values ('Item ONE', GETDATE())
--0x00000000000007D1

update Item set Name='Item One' where Id=1
select * from Item




insert into Item (Name, CreatedAt) values ('Item TWO', GETDATE())
select * from Item
