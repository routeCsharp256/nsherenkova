using FluentMigrator;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class MerchandiseRequests : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TYPE statuses AS ENUM ('Draft', 'Created', 'Assigned', 'InProgress', 'Done');
                CREATE TABLE if not exists merchandise_requests(
                    id BIGSERIAL PRIMARY KEY,
                    status statuses NOT NULL,
                    employee_id INT NOT NULL,
                    menager_id INT,                    
                    merchandise_item_id INT);"
            );
        }
        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merchandise_requests;");
        }
    }
}