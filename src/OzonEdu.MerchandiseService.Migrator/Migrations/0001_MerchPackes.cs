using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class MerchPackes : Migration
    {
        public override void Up()
        {
            Create
                .Table("merch_packes")
                .WithColumn("id").AsInt32().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("merch_packes");
        }
    }
}