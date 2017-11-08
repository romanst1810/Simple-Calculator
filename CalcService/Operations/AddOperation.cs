using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcService.Models;

namespace CalcService.Operations
{
    public class AddOperation : CalcOperationBase
    {
        public AddOperation() : base("+")
        {
        }

        public override decimal Execute(OperationRequest context)
        {
            if (context == null)
                throw new ArgumentException();

            decimal result = context.Args.First();
            context.Args
                .Skip(1)
                .ToList()
                .ForEach(x => result += x);

            return result;
        }
    }

}
