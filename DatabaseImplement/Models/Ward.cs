using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{

    public class Ward
    {
        public int? Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int Mesto { get; set; }
        public virtual List<WardPacient> WardPacients { get; set; }
    }
}
