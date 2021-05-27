using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactorialEK.Model.Database.Entities
{
    public class CategoryManufacturingOrService
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public CategoryPhotoManufacturingOrService Photo { get; set; }
        
        public virtual ICollection<ManufacturingOrService> ManufacturingOrServices { get; set; }
    }
}