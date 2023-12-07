using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextRPG.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmourTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArmourModifier = table.Column<int>(type: "int", nullable: false),
                    AvailableToHero = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Career",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityBaseSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Vigor = table.Column<int>(type: "int", nullable: false),
                    Spirit = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    HealthModifier = table.Column<int>(type: "int", nullable: false),
                    EnergyModifier = table.Column<int>(type: "int", nullable: false),
                    DamagerModifier = table.Column<int>(type: "int", nullable: false),
                    ArmourModifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityBaseSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PotionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PotionDice = table.Column<int>(type: "int", nullable: false),
                    AvailableToHero = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillRollType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillRollType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    ArmourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Armour_ArmourId",
                        column: x => x.ArmourId,
                        principalTable: "Armour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeaponType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillRollTypeId = table.Column<int>(type: "int", nullable: false),
                    EnergyCost = table.Column<int>(type: "int", nullable: false),
                    DamageDice = table.Column<int>(type: "int", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponType_SkillRollType_SkillRollTypeId",
                        column: x => x.SkillRollTypeId,
                        principalTable: "SkillRollType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroXp = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityBaseSystemId = table.Column<int>(type: "int", nullable: true),
                    InventoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hero_Career_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Career",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hero_EntityBaseSystem_EntityBaseSystemId",
                        column: x => x.EntityBaseSystemId,
                        principalTable: "EntityBaseSystem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hero_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hero_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonsterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonsterXp = table.Column<int>(type: "int", nullable: false),
                    LevelDifficulty = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityBaseSystemId = table.Column<int>(type: "int", nullable: true),
                    InventoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monster_EntityBaseSystem_EntityBaseSystemId",
                        column: x => x.EntityBaseSystemId,
                        principalTable: "EntityBaseSystem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Monster_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Potion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    PotionTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Potion_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Potion_PotionType_PotionTypeId",
                        column: x => x.PotionTypeId,
                        principalTable: "PotionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponTypeId = table.Column<int>(type: "int", nullable: false),
                    WeaponName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeaponDamageModifier = table.Column<int>(type: "int", nullable: false),
                    SkillRoll = table.Column<int>(type: "int", nullable: false),
                    AvailableToHero = table.Column<bool>(type: "bit", nullable: false),
                    StarterWeapon = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapon_WeaponType_WeaponTypeId",
                        column: x => x.WeaponTypeId,
                        principalTable: "WeaponType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryWeapon",
                columns: table => new
                {
                    InventoriesId = table.Column<int>(type: "int", nullable: false),
                    WeaponsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryWeapon", x => new { x.InventoriesId, x.WeaponsId });
                    table.ForeignKey(
                        name: "FK_InventoryWeapon_Inventory_InventoriesId",
                        column: x => x.InventoriesId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryWeapon_Weapon_WeaponsId",
                        column: x => x.WeaponsId,
                        principalTable: "Weapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hero_CareerId",
                table: "Hero",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_EntityBaseSystemId",
                table: "Hero",
                column: "EntityBaseSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_InventoryId",
                table: "Hero",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_RaceId",
                table: "Hero",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ArmourId",
                table: "Inventory",
                column: "ArmourId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryWeapon_WeaponsId",
                table: "InventoryWeapon",
                column: "WeaponsId");

            migrationBuilder.CreateIndex(
                name: "IX_Monster_EntityBaseSystemId",
                table: "Monster",
                column: "EntityBaseSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Monster_InventoryId",
                table: "Monster",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Potion_InventoryId",
                table: "Potion",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Potion_PotionTypeId",
                table: "Potion",
                column: "PotionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapon_WeaponTypeId",
                table: "Weapon",
                column: "WeaponTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponType_SkillRollTypeId",
                table: "WeaponType",
                column: "SkillRollTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hero");

            migrationBuilder.DropTable(
                name: "InventoryWeapon");

            migrationBuilder.DropTable(
                name: "Monster");

            migrationBuilder.DropTable(
                name: "Potion");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "EntityBaseSystem");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "PotionType");

            migrationBuilder.DropTable(
                name: "WeaponType");

            migrationBuilder.DropTable(
                name: "Armour");

            migrationBuilder.DropTable(
                name: "SkillRollType");
        }
    }
}
