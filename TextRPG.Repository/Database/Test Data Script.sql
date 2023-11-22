use  TextRPG
go

insert into Armour (ArmourTypeName,ArmourModifier,AvailableToHero,[Value],Note)
values
('Leather Armour',1,1,20,NULL),
('Chainmail Armour',2,1,45,NULL),
('Plate Armour',5,1,75,NULL),
('Goblin Hide',2,0,8,NULL)

insert into Race (RaceType)
values 
('Human'),
('Dwarf'),
('Elf'),
('Halfling')
go

insert into Career(CareerType)
values 
('Guard'),
('Thief'),
('Hunter'),
('Smith'),
('Farmer'),
('Lumberjack'),
('Sailor')
go

insert into Inventory(Gold, ArmourId)
values 
(0, NULL), --Alex
(10, NULL), --McTest
(37, NULL),
(23, NULL),
(9999, NULL), --Björk
(56, NULL), --Skeleton
(25, NULL),
(34, NULL) --Goblin
go

insert into EntityBaseSystem(Stength,Agility,Vigor,Spirit,Health,Energy,HealthModifier,EnergyModifier,DamagerModifier,ArmourModifier)
values 
(10,10,10,10,10,10,0,0,0,0), --Alex
(15,12,8,7,12,8,1,0,1,0), --Björk
(20,20,20,20,20,20,5,5,5,5),
(0,0,0,0,0,0,0,0,0,0), --McTest
(18,7,15,10,26,11,2,-1,2,1), --Skeleton
(7,10,12,19,17,26,-2,5,0,0) --Goblin
go

insert into Hero(HeroName, HeroXp, [Level],EntityBaseSystemId,InventoryId,RaceId,CareerId,Note)
values 
('Alex',0,1,1,1,1,1,Null),
('Björk',100,12,2,5,2,6,'Bjork hits hard'),
('McTest',0,100,4,2,4,1,Null)
go

insert into Monster(MonsterName, MonsterXp, LevelDifficulty, EntityBaseSystemId,InventoryId,Note)
values 
('Skeleton',7,3,5,6, NULL),
('Goblin',2,1,6,8, NULL)
go