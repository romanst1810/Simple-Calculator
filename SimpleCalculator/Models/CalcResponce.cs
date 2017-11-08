using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCalculator.Models
{
    public class CalcResponce
    {
        public int ArgumentsCount { get; set; }
        public List<string> Operators { get; set; }
        public CalcResponce()
        {
            Operators = new List<string>();
        }
    }
}