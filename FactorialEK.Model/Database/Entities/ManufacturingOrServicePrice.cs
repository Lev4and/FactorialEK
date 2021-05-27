using System;

namespace FactorialEK.Model.Database.Entities
{
    public class ManufacturingOrServicePrice
    {
        public Guid Id { get; set; }
        
        public Guid ManufacturingOrServiceId { get; set; }
        
        public int? SecondPrice { get; set; }
        
        public int? LowerLimitPrice { get; set; }
        
        public int? UpperLimitPrice { get; set; }
        
        public bool IsUponRequest { get; set; }
        
        public bool IsIndividualCalculation { get; set; }
        
        public ManufacturingOrService ManufacturingOrService { get; set; }
    }
}