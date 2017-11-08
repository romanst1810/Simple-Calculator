using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcService.Operations
{
    public class ColorOperation : ColorOperationBase
    {
        public override string Execute(decimal context)
        {
            return context % 2 == 0 ? "green" : "red";
        }
    }
}
