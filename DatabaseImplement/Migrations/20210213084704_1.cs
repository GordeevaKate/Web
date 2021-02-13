using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseImplement.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnosiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosiss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Healings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    DiagnosisId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Temperatura = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Healings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(nullable: false),
                    Polis = table.Column<int>(nullable: false),
                    Adress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Cena = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Mesto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisServices_Diagnosiss_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosiss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealingServises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealingId = table.Column<int>(nullable: false),
                    ServiseId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ServicesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealingServises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealingServises_Healings_HealingId",
                        column: x => x.HealingId,
                        principalTable: "Healings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealingServises_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacientWards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacientId = table.Column<int>(nullable: false),
                    WardId = table.Column<int>(nullable: false),
                    DiagnosissId = table.Column<int>(nullable: true),
                    ServicesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientWards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacientWards_Diagnosiss_DiagnosissId",
                        column: x => x.DiagnosissId,
                        principalTable: "Diagnosiss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientWards_Pacients_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientWards_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientWards_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisServices_DiagnosisId",
                table: "DiagnosisServices",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisServices_ServiceId",
                table: "DiagnosisServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_HealingServises_HealingId",
                table: "HealingServises",
                column: "HealingId");

            migrationBuilder.CreateIndex(
                name: "IX_HealingServises_ServicesId",
                table: "HealingServises",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientWards_DiagnosissId",
                table: "PacientWards",
                column: "DiagnosissId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientWards_PacientId",
                table: "PacientWards",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientWards_ServicesId",
                table: "PacientWards",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientWards_WardId",
                table: "PacientWards",
                column: "WardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosisServices");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "HealingServises");

            migrationBuilder.DropTable(
                name: "PacientWards");

            migrationBuilder.DropTable(
                name: "Healings");

            migrationBuilder.DropTable(
                name: "Diagnosiss");

            migrationBuilder.DropTable(
                name: "Pacients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Wards");
        }
    }
}
