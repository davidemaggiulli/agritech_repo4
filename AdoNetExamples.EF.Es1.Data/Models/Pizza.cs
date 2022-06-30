using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdoNetExamples.EF.Es1.Data.Models
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key]
        public int Code { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IList<Composition> Compositions { get; set; }
    }
}
