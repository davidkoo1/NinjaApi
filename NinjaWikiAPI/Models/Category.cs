﻿using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<Skill> Skills { get; set; }
    }
}
