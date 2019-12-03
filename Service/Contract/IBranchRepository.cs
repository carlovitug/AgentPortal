using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IBranchRepository
    {
        Task<ActionResult<IEnumerable<Branch>>> GetBranches();
        Task<Branch> AddBranch(Branch agent);
        Task<Branch> UpdateBranch(Branch agent);
        Task<Branch> DeleteBranch(Branch agent);
        Task<Branch> GetBranch(int id);
    }
}
