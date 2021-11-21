using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class Employees : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists employees(
                    id BIGSERIAL PRIMARY KEY,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    phone_number INT NOT NULL,
                    clothing_size INT NOT NULL);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists employees;");
        }
    }
}