using CalcService.Interfaces;
using CalcService.Models;

namespace CalcService.Operations
{
    public abstract class CalcOperationBase : ICalcOperation
    {
        protected CalcOperationBase(string opertion)
        {
            Code = opertion;
        }

        public bool CanExecute(OperationRequest context)
        {
            return context != null && context.OperationId == Code;
        }

        public abstract decimal Execute(OperationRequest context);

        public string Code { get; private set; }
    }
}
