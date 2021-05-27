using System;
using System.ComponentModel.DataAnnotations;

namespace FactorialEK.Model.Database.Entities
{
    public class ManufacturingOrServiceInformation
    {
        public Guid Id { get; set; }
        
        public Guid ManufacturingOrServiceId { get; set; }
        
        public string DescriptionOfValueFormation { get; set; }
        
        [Required]
        public string ShortDescription { get; set; }
        
        public string Specifications { get; set; }
        
        public string Description { get; set; }
        
        public string Advantages { get; set; }
        
        public ManufacturingOrService ManufacturingOrService { get; set; }
    }
}