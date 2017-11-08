using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleCalculator.Models
{
    public class CalcResultRequest
    {

        [Required]
        public int ArgumentA { get; set; }

        [Required]
        public int ArgumentB { get; set; }

        [Required]
        public string Operator { get; set; }
    }
}