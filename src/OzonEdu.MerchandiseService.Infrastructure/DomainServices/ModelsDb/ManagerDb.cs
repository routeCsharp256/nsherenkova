namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.ModelsDb
{
    public class ManagerDb
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AssignedTasks { get; set; }
    }
}