using Microsoft.AspNetCore.Http;
using System;

namespace FactorialEK.AspNetCore.Models
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }
    }
}
