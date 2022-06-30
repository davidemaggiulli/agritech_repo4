using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdoNetExamples.EF.Es1.Data.Models
{
    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        public int Code { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        public decimal Cost { get; set; }

        public decimal Stock { get; set; }

        public IList<Composition> Compositions { get; set; }
    }
}
