using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcService.Models;

namespace CalcService.Operations
{
    public class DivOperation : CalcOperationBase
    {
        public DivOperation() : base("/")
        {
        }

        public override decimal Execute(OperationRequest context)
        {
            if (context == null)
                throw new ArgumentException();
            if (context.Args.Skip(1).Any(x => x == decimal.Zero))
            {
                throw new ArgumentException("Can't divide to zero");
            }

            decimal result = context.Args.First();
            context.Args
                .Skip(1)
                .ToList()
                .ForEach(x => result /= x);

            return result;
        }
    }
}
