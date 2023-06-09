﻿using ChocolateManagementSystem.Domain.Common;

namespace ChocolateManagementSystem.Domain.Entities
{
    public class ChocolateFactory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ChocolateBar> ChocolateBars { get; set; }
    }
}
