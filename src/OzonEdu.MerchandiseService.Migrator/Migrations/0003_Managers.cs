using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class Managers : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists managers(
                    id BIGSERIAL PRIMARY KEY,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    assigned_tasks INT NOT NULL);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists managers;");
        }
    }
}