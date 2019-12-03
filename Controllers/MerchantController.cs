using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ABMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : Controller
    {
        public IMerchantRepository _merchantRepository;
        public MerchantController(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        //Returns list of merchants
        [HttpGet("GetMerchants")]
        public async Task<ActionResult<IEnumerable<Merchant>>> GetMerchants()
        {
            return await _merchantRepository.GetMerchants(); 
            
        }

        //Returns merchant
        [HttpGet("GetMerchant")]
        public async Task<IActionResult> GetMerchant(int id)
        {
            if(id > 0)
            {
                var temp = await _merchantRepository.GetMerchant(id);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
            
        }

        //Add new merchant record
        [HttpPost("AddMerchant")]
        public async Task<ActionResult<Merchant>> AddMerchant(Merchant agent)
        {
            if (agent.Merchant_ID > 0)
            {
                var temp = await _merchantRepository.AddMerchant(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }
        //Update merchant record
        [HttpPost("UpdateMerchant")]
        public async Task<ActionResult<Merchant>> UpdateMerchant(Merchant agent)
        {
            //return await _merchantRepository.UpdateMerchant(agent);
            if (agent.Merchant_ID > 0)
            {
                var temp = await _merchantRepository.UpdateMerchant(agent);
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }
        //Delete merchant record
        [HttpGet("DeleteMerchant")]
        public async Task<ActionResult<Merchant>> DeleteMerchant(int id)
        {
            //return await _merchantRepository.DeleteMerchant(new Merchant { ID = id });
            if (id > 0)
            {
                var temp = await _merchantRepository.DeleteMerchant(new Merchant { ID = id });
                return Ok(temp);
            }
            else
            {
                return BadRequest(new { Message = "Error: Incomplete data." });
            }
        }
    }
}