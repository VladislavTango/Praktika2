
using System.ComponentModel.DataAnnotations;

namespace PraktikaDomain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
