﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorialEK.Model.Database.Entities
{
    public class GalleryPhoto
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Url { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime CreatedAt  { get; set; }
    }
}