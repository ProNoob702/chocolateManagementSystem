using ChocolateManagementSystem.Domain.Common;

namespace ChocolateManagementSystem.Domain.Entities
{
    public class ChocolateBar : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cacao { get; set; }
        public decimal Price { get; set;}
    }
}
