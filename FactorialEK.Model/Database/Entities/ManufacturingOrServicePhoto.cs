using System;
using System.ComponentModel.DataAnnotations;

namespace FactorialEK.Model.Database.Entities
{
    public class ManufacturingOrServicePhoto
    {
        public Guid Id { get; set; }
        
        public Guid ManufacturingOrServiceId { get; set; }
        
        [Required]
        public string Url { get; set; }
        
        public ManufacturingOrService ManufacturingOrService { get; set; }
    }
}