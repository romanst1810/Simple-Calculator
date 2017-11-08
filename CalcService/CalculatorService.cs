using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcService.Interfaces;
using CalcService.Models;
using CalcService.Operations;

namespace CalcService
{
    public class CalculatorService : ICalcService
    {
        private IEnumerable<ICalcOperation> calcOperations;
        private IColorOperation colorOperation;
        private ICalcOpertionFactory factory;

        public CalculatorService()
        {
            calcOperations = new ICalcOperation[] { new AddOperation(), new SubOperation(), new MulOperation(), new DivOperation() };
            colorOperation = new ColorOperation();
        }

        public CalculatorService(IEnumerable<ICalcOperation> calcOperation, IColorOperation colorOperation)
        {
            this.calcOperations = calcOperation;
            this.colorOperation = colorOperation;
        }

        public IEnumerable<OperationInfo> GetOperations()
        {
            return calcOperations.Select(x => new OperationInfo()
            {
                Id = x.Code
            }).ToArray();
        }

        public OperationResult Calculate(OperationRequest context)
        {
            var calcOperation = calcOperations.FirstOrDefault(x => x.CanExecute(context));
            if (calcOperation == null)
            {
                throw new NotSupportedException();
            }

            var result = new OperationResult()
            {
                Args = context.Args,
                OperationId = calcOperation.Code,
                Result = calcOperation.Execute(context),
            };
            result.ColorCode = colorOperation.Execute(result.Result);
            return result;
        }
    }
}
