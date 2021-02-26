using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class AddServiceModel
    {
        public int Id { get; set; }
        public int DiagnosId { get; set; }
        public string Name { get; set; }
        public double Count { get; set; }
    }
}
