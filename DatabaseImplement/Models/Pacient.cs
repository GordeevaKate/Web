using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class Pacient
    {
        public int? Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public int Polis { get; set; }
        [Required]
        public string Adress { get; set; }
        public virtual List<PacientWard> PacientWards { get; set; }
    }
}
