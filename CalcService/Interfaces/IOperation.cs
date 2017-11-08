namespace CalcService.Interfaces
{
    public interface IOperation<TIn, TOut>
    {
        bool CanExecute(TIn context);
        TOut Execute(TIn context);
    }

}
