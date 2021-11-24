using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(5)]
    public class MerchandiseItems : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merchandise_items(
                    id INT NOT NULL PRIMARY KEY,                    
                    merch_pack_id INT NOT NULL,
                    items INT[]);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merchandise_items;");
        }
    }
}