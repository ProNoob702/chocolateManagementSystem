using ChocolateManagementSystem.Domain.Common;

namespace ChocolateManagementSystem.Domain.Entities
{
    public class WholesalerChocolateStock : BaseEntity
    {
        public int WholesalerId { get; set; }
        public int ChocolateBarId { get; set; }
        public int Stock { get; set; }

    }
}
