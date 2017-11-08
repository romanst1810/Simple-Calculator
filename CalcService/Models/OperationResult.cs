namespace CalcService.Models
{
    public class OperationResult
    {
        public string OperationId { get; set; }

        public decimal[] Args { get; set; }

        public decimal Result { get; set; }

        public string ColorCode { get; set; }
    }
}
