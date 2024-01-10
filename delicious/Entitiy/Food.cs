using System;
using System.Collections.Generic;
using System.Text;

namespace Entitiy
{
    internal class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FoodGroup Group { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Recipe { get; set; }
    }
}
