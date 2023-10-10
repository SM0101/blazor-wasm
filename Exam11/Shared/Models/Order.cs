using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam11.Shared.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerImage { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
