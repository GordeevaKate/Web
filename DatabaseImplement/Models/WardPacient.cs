using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseImplement.Models
{
        public class WardPacient
    {
        public int? Id { get; set; }
        public int PacientId { get; set; }
        public int WardId { get; set; }
            public virtual Pacient Pacients { get; set; }
            public virtual Service Wards { get; set; }
        }
}
