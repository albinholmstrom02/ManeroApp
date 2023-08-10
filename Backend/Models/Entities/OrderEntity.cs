
using Backend.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities
{
    public class OrderEntity
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int TotalPrice { get; set; }
        public string Products { get; set; }
    }
}
