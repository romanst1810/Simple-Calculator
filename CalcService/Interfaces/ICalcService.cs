using System.Collections.Generic;
using CalcService.Models;

namespace CalcService.Interfaces
{
    public interface ICalcService
    {
        IEnumerable<OperationInfo> GetOperations();

        OperationResult Calculate(OperationRequest context);
    }
}
