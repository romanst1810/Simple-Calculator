using CalcService.Interfaces;

namespace CalcService.Operations
{
    public abstract class ColorOperationBase : IColorOperation
    {
        public bool CanExecute(decimal context)
        {
            return true;
        }
        public abstract string Execute(decimal context);
    }
}
