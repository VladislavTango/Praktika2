
namespace PraktikaDomain.Entities
{
    public class Client : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
        public ICollection<Order> OrderList { get; set; } = new List<Order>();
    }
}
