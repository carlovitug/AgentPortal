using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IMerchantRepository
    {
        Task<ActionResult<IEnumerable<Merchant>>> GetMerchants();
        Task<Merchant> AddMerchant(Merchant agent);
        Task<Merchant> UpdateMerchant(Merchant agent);
        Task<Merchant> DeleteMerchant(Merchant agent);
        Task<Merchant> GetMerchant(int id);
    }
}
