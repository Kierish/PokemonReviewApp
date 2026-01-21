using System;
using System.Collections.Generic;

namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }

        public int? OwnerId { get; set; } 
        public Owner? Owner { get; set; }   
        public ICollection<PokemonCategory>? PokemonCategories { get; set; }
    }
}