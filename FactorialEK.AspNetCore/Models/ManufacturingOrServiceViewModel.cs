using FactorialEK.Model.Database.Entities;
using System.Collections.Generic;

namespace FactorialEK.AspNetCore.Models
{
    public class ManufacturingOrServiceViewModel
    {
        public ManufacturingOrService ManufacturingOrService { get; set; }

        public List<CategoryManufacturingOrService> Categories { get; set; }
    }
}
