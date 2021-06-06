using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactorialEK.Model.Database.Entities
{
    public class ManufacturingOrService
    {
        public Guid Id { get; set; }
        
        public Guid CategoryManufacturingOrServiceId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public DateTime AddedAt { get; set; }

        public ManufacturingOrServiceMainPhoto MainPhoto { get; set; }
        
        public CategoryManufacturingOrService Category { get; set; }
        
        public ManufacturingOrServicePrice Price { get; set; }
        
        public ManufacturingOrServiceInformation Information { get; set; }
        
        public virtual ICollection<ManufacturingOrServicePhoto> Photos { get; set; }
    }
}