using System;
using System.ComponentModel.DataAnnotations;

namespace FactorialEK.Model.Database.Entities
{
    public class CategoryPhotoManufacturingOrService
    {
        public Guid Id { get; set; }
        
        public Guid CategoryManufacturingOrServiceId { get; set; }
        
        [Required]
        public string Url { get; set; }
        
        public CategoryManufacturingOrService CategoryManufacturingOrService { get; set; }
    }
}