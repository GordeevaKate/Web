using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class Doctor
    {
        public int? Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public DoctorStatus Status { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
