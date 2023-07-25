using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCM.TesteAcesso.Infra.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fund-transfer");

            migrationBuilder.CreateSequence(
                name: "seq_operation_id",
                schema: "fund-transfer",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "seq_transfer_id",
                schema: "fund-transfer",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "transfer",
                schema: "fund-transfer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Table unique identity"),
                    account_origin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Account origin"),
                    account_destination = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Account destination"),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    message = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    transaction_uid = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Record inserted timestamp"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Recorde updated timestamp ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "operation",
                schema: "fund-transfer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Table unique identity"),
                    account_destination = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    operation_type = table.Column<byte>(type: "smallint", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    executed = table.Column<bool>(type: "boolean", nullable: false),
                    date_executed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    transfer_id = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Record inserted timestamp"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Recorde updated timestamp ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operation", x => x.id);
                    table.ForeignKey(
                        name: "fk_operation_transfer_transfer_id",
                        column: x => x.transfer_id,
                        principalSchema: "fund-transfer",
                        principalTable: "transfer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_operation_transfer_id",
                schema: "fund-transfer",
                table: "operation",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "idx_transaction_transaction_uid",
                schema: "fund-transfer",
                table: "transfer",
                column: "transaction_uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operation",
                schema: "fund-transfer");

            migrationBuilder.DropTable(
                name: "transfer",
                schema: "fund-transfer");

            migrationBuilder.DropSequence(
                name: "seq_operation_id",
                schema: "fund-transfer");

            migrationBuilder.DropSequence(
                name: "seq_transfer_id",
                schema: "fund-transfer");
        }
    }
}
