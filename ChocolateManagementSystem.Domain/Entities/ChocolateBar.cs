using ChocolateManagementSystem.Domain.Common;
using System.Diagnostics;

namespace ChocolateManagementSystem.Domain.Entities
{
    public class ChocolateBar : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cacao { get; set; }
        public decimal Price { get; set;}

        public int FactoryId { get; set; }
        public ChocolateFactory Factory { get; set; }
        public ICollection<Wholesaler> Wholesalers { get; set; }
    }
}
