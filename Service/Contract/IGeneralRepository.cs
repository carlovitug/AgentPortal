using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IGeneralRepository
    {
        Task<ActionResult<IEnumerable<General>>> GetGenerals();
        Task<General> AddGeneral(General agent);
        Task<General> UpdateGeneral(General agent);
        Task<General> DeleteGeneral(General agent);
        Task<General> GetGeneral(int id);
    }
}
