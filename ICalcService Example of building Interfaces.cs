using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService
{
    public class OperationInfo
    {
        public string Id { get; set; }

        public string Text { get; set; }
    }

    public interface ICalcService
    {
        IEnumerable<OperationInfo> GetOperations();

        OperationResult Calculate(OperationRequest context);
    }

    public class OperationRequest
    {
        public string OperationId { get; set; }
        public decimal[] Args { get; set; }
    }

    public class OperationResult
    {
        public string OperationId { get; set; }

        public decimal[] Args { get; set; }

        public decimal Result { get; set; }

        public string ColorCode { get; set; }
    }

    public interface IOperation<TIn, TOut>
    {
        bool CanExecute(TIn context);
        TOut Execute(TIn context);
    }

    public interface ICalcOperation : IOperation<OperationRequest, decimal>
    {
        string Code { get; }
    }

    public interface IColorOperation : IOperation<decimal, string>
    {

    }

    public interface ICalcOpertionFactory
    {
        ICalcOperation Create(string operationId);
    }



    public class CalcService : ICalcService
    {
        private IEnumerable<ICalcOperation> calcOperations;
        private IColorOperation colorOperation;
        //private ICalcOpertionFactory factory;

        public CalcService()
        {
            calcOperations = new ICalcOperation[] { new AddOperation(),new SubOperation(), new MulOperation(),  new DivOperation() };
            colorOperation = new ColorOperation();
        }

        public CalcService(IEnumerable<ICalcOperation> calcOperation, IColorOperation colorOperation)
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

            calcOperation = factory.Create(context.OperationId);

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

    public class AddOperation : CalcOpertionBase
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

    public class SubOperation : CalcOperationBase
    {
        public SubOperation() : base("-")
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
                .ForEach(x => result -= x);

            return result;
        }
    }

    public class MulOperation : CalcOperationBase
    {
        public MulOperation() : base("*")
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
                .ForEach(x => result *= x);

            return result;
        }
    }

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

    public abstract class ColorOperationBase : IColorOperation
    {
        public bool CanExecute(decimal context)
        {
            return context != decimal.Zero;
        }
        public abstract string Execute(decimal context);   
    }
    public class ColorOperation : ColorOperationBase
    {
        public override string Execute(decimal context)
        {
            return context % 2 == 0 ? "green" : "red";
        }
    }

}
