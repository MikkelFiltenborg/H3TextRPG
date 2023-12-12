using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Test.MockData
{
    internal class MockDataRepos
    {
        public static Race GetRaceData(int id)
        {
            Race race = new Race()
            {
                Id = id,
                RaceType = $"Race-{id}"
            };
            return race;
        }

        public static Career GetCareerData(int id)
        {
            Career career = new Career()
            {
                Id = id,
                CareerType = $"Career-{id}"
            };
            return career;
        }

        public static Armour GetArmourData(int id)
        {
            Armour armour = new Armour()
            {
                Id = id,
                ArmourTypeName = $"ArmourType-{id}",
                ArmourModifier = id,
                AvailableToHero = true,
                Value = id,
                Note = $"Note-{id}"
            };
            return armour;
        }

        public static SkillRollType GetSkillRollTypeData(int id)
        {
            SkillRollType skillRollType = new SkillRollType()
            {
                Id = id,
                SkillType = $"SkillType-{id}"
            };
            return skillRollType;
        }

        public static WeaponType GetWeaponTypeData(int id)
        {
            WeaponType weaponType = new WeaponType()
            {
                Id = id,
                WeaponTypeName = $"WeaponType-{id}",
                SkillRollTypeId = id,
                EnergyCost = id,
                DamageDice = id,
                Range = id,
                SkillRollType = GetSkillRollTypeData(id)
            };
            return weaponType;
        }

        public static Weapon GetWeaponData(int id)
        {
            Weapon weapon = new Weapon()
            {
                Id = id,
                WeaponTypeId = id,
                WeaponName = $"Weapon-{id}",
                WeaponDamageModifier = id,
                SkillRoll = id,
                AvailableToHero = true,
                StarterWeapon = true,
                Value = id,
                Note = $"Note-{id}",
                WeaponType = GetWeaponTypeData(id)
                
            };
            return weapon;
        }

        public static PotionType GetPotionTypeData(int id)
        {
            PotionType potionType = new PotionType()
            {
                Id = id,
                PotionTypeName = $"PotionTypeName-{id}",
                PotionDice = id,
                AvailableToHero = true,
                Value = id,
                Note = $"Note-{id}"
            };
            return potionType;
        }

        public static Inventory GetInventoryData(int id)
        {
            Inventory inventory = new Inventory()
            {
                Id = id,
                Gold = id
            };
            return inventory;
        }


        public static Potion GetpotionData(int id)
        {
            Potion potion = new Potion()
            {
                Id = id,
                InventoryId = id,
                PotionTypeId = id,
                PotionType = GetPotionTypeData(id)
            };
            return potion;
        }

        public static EntityBaseSystem GetEntityBaseSystemData(int id)
        {
            EntityBaseSystem entityBaseSystem = new EntityBaseSystem()
            {
                Id = id,
                Strength = id,
                Agility = id,
                Vigor = id,
                Spirit = id,
                Health = id,
                Energy = id,
                HealthModifier = id,
                EnergyModifier = id,
                DamageModifier = id,
                ArmourModifier = id
            };
            return entityBaseSystem;
        }

        public static Monster GetMonsterData(int id)
        {
            Monster monster = new Monster()
            {
                Id = id,
                EntityBaseSystem = GetEntityBaseSystemData(id),
                Inventory = GetInventoryData(id),
                MonsterName = $"MonsterName-{id}",
                MonsterXp = id,
                LevelDifficulty = id,
                Note = $"Note-{id}"
            };
            return monster;
        }

        public static Hero GetHeroData(int id)
        {
            Hero hero = new Hero()
            {
                Id = id,
                EntityBaseSystem = GetEntityBaseSystemData(id),
                Inventory = GetInventoryData(id),
                HeroName = $"HeroName-{id}",
                HeroXp = id,
                Level = id,
                RaceId = id,
                Race = GetRaceData(id),
                CareerId = id,
                Career = GetCareerData(id),
                Note = $"Note-{id}"
            };
            return hero;
        }
    }
}
