using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdoNetExamples.EF.Es1.Data.Models
{
    [Table("Composition")]
    public class Composition
    {
        
        public int CodePizza { get; set; }

   
        [ForeignKey(nameof(Ingredient))]
        public int CodeIngredient { get; set; }
        public decimal Quantity { get; set; }

        [ForeignKey(nameof(CodePizza))]
        public Pizza Pizza { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}
