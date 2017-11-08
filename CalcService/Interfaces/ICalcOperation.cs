using CalcService.Models;

namespace CalcService.Interfaces
{
    public interface ICalcOperation : IOperation<OperationRequest, decimal>
    {
        string Code { get; }
    }
}
