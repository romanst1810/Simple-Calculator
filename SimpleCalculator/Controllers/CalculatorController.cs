using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalcService;
using CalcService.Interfaces;
using CalcService.Models;

namespace SimpleCalculator.Controllers
{
    public class CalculatorController : ApiController
    {

        ICalcService _service;
        
        public CalculatorController() : this(new CalculatorService())
        {
        }
        
        public CalculatorController(ICalcService service)
        {
            _service = service;
        }
        
       [HttpGet]
        public IEnumerable<OperationInfo> GetOperations()
        {
           try
           {
               return _service.GetOperations();
           }
           catch (Exception ex)
           {
               throw new HttpResponseException(HttpStatusCode.BadRequest);
           }
        }
       
        [HttpPost]
        public OperationResult Calculate(OperationRequest request)
        {
            try
            {
                var result = _service.Calculate(request);
                return result;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
