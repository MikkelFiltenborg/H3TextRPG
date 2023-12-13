use  TextRPG
go

insert into Armour (ArmourTypeName,ArmourModifier,AvailableToHero,[Value],Note)
values
('Leather Armour',1,1,20,NULL),
('Chainmail Armour',2,1,45,NULL),
('Plate Armour',5,1,75,NULL),
('Goblin Hide',2,0,8,NULL)
go

insert into PotionType (PotionTypeName,PotionDice,AvailableToHero,[Value],Note)
values
('Health Potion',4,1,25,NULL),
('Energy Potion',4,1,25,NULL),
('Goblin Brew',5,0,6,NULL)
go

insert into SkillRollType (SkillType)
values
('Strength'),
('Agility'),
('Vigor'),
('Spirit')
go

insert into WeaponType (WeaponTypeName, SkillRollTypeId, EnergyCost,DamageDice,[Range])
values
('Short Sword',1,2,6,0),
('Long Sword',1,4,8,0),
('Spear',2,3,6,1),
('Axe',1,5,10,0),
('Bow',2,4,6,3),
('Sling',2,3,4,2),
('Stick',4,1,18,0)
go

insert into Weapon (WeaponName,WeaponTypeId,WeaponDamageModifier,SkillRoll,AvailableToHero,StarterWeapon,[Value], Note)
values
('Sting',1,1,7,1,0,42,''),
('Great Axe',4,4,13,1,0,35,''),
('Rusty Axe',4,1,11,1,1,27,''),
('Old Short Sword',1,-1,8,1,1,20,''),
('Long Stick',7,0,6,1,1,18,'Big Stick to walk & hit'),
('Long Bow',5,1,10,1,0,30,''),
('Spear',3,1,9,1,0,36,''),
('The Stick',7,10,5,0,0,1000,'Ohh God, what has happen')
go

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
(10, 1), --McTest
(300, 3), --Björk
(56, 2), --Skeleton
(99999, NULL), --The Unholy Admin
(34, 4), --Goblin
(8, Null) --Wolf
go

insert into EntityBaseSystem(Strength,Agility,Vigor,Spirit,Health,Energy,HealthModifier,EnergyModifier,DamageModifier,ArmourModifier)
values 
(10,10,10,10,10,10,0,0,0,0), --Alex
(15,12,8,7,12,8,1,0,1,0), --Björk
(20,20,20,20,20,20,5,5,5,5), --The Unholy Admin
(0,0,0,0,0,0,0,0,0,0), --McTest
(18,7,15,10,26,11,2,-1,2,1), --Skeleton
(7,10,12,19,17,26,-2,5,0,0), --Goblin
(8,9,12,10,14,18,0,2,1,0) --Wolf
go

insert into Hero(HeroName, HeroXp, [Level],EntityBaseSystemId,InventoryId,RaceId,CareerId,Note)
values 
('Alex',0,1,1,1,1,1,Null),
('Björk',100,12,2,3,2,6,'Bjork hits hard'),
('McTest',0,100,4,2,4,1,Null)
go

insert into Monster(MonsterName, MonsterXp, LevelDifficulty, EntityBaseSystemId,InventoryId,Note)
values 
('Skeleton',7,3,4,4, NULL),
('Goblin',4,1,6,6, NULL),
('Wolf',2,1,7,7, NULL),
('The Unholy Admin',9999,10,3,5,'You dont want to meet the Admin')
go

insert into InventoryWeapon (InventoriesId,WeaponsId)
values
(1,3), --Alex-OldShortSword
(3,2), --Björk-GreatAxe
(3,6), --Björk-LongBow
(4,5), --Skeleton-Spear
(6,3), --Goblin-OldShortSword
(5,6)  --The Unholy Admin-TheStick
go

insert into Potion (InventoryId,PotionTypeId,Amount)
values
(3,1,7),
(3,2,3),
(6,3,1),
(1,1,1)
go