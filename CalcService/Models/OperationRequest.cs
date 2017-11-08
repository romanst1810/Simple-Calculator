namespace CalcService.Models
{
    public class OperationRequest
    {
        public string OperationId { get; set; }
        public decimal[] Args { get; set; }
    }
}
