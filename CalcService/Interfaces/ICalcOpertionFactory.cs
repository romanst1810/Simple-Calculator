namespace CalcService.Interfaces
{
    public interface ICalcOpertionFactory
    {
        ICalcOperation Create(string operationId);
    }
}
