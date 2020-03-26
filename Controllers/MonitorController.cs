using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : Controller
    {

        public IMonitorService monitorService;
        public MonitorController(IMonitorService monitor)
        {
            monitorService = monitor;
        }

        //Get all the transaction for today.
        [HttpPost("GetFinancialMessages")]
        public async Task<IEnumerable<FinancialMessage>> GetFinancialMessages([FromBody] FinancialMessageRequest request)
        {
            var temp = await monitorService.GetFinancialMessages(request);
            return temp;
        }

        //Get all transaction for the filtered day
        [HttpGet("GetCurrentFinancialMessages")]
        public async Task<IEnumerable<FinancialMessage>> GetCurrentFinancialMessages()
        {
            string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateFrom = "2019-08-20";
            FinancialMessageRequest messageRequest = new FinancialMessageRequest();
            var temp = new List<FinancialMessage>();

            try
            {
                messageRequest.DateFrom = dateFrom;
                temp = await monitorService.GetCurrentFinancialMessages(messageRequest);
            }
            catch (Exception ex)
            {
                var err = ex;
            }

            return temp;
        }

    }
}