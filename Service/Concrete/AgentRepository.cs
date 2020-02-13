using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ABMSContext _context;
        public AgentRepository(ABMSContext context)
        {
            _context = context;
        }
        //public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        //{
        //    List<Agent> agent = new List<Agent>();
        //    try
        //    {
        //        agent = await _context.Agent.FromSql("dbo.SP_AgentReadAll").ToListAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return agent;
        //}

        //public async Task<Agent> GetAgent(int id)
        //{
        //    Agent agent = new Agent();
        //    try
        //    {
        //        agent = await _context.Agent.FromSql("dbo.SP_AgentRead @id = {0}").FirstOrDefaultAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return agent;
        //}

        //public async Task<Agent> UpdateAgent(Agent agent)
        //{
        //    try
        //    {
        //        await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentUpdate " +
        //             "@id = {0}, @aid = {1}, @mid = {2}, @otp = {3}, @deactivate  = {4}, @fname = {5}, @mname = {6}, @lname = {7}, " +
        //             "@gender = {8}, @dob = {9}, @pnum = {10}, @address = {11}, @city = {12}, @state = {13}, @pcode = {14}," +
        //             "@nationality = {15}, @user = {16}",
        //              agent.ID, agent.MerchantID, agent.OTP, agent.Deactivated, agent.FirstName, agent.MiddleName, agent.LastName,
        //              agent.Gender, agent.DateOfBirth, agent.PhoneNum, agent.Address, agent.City, agent.State, agent.PostalCode,
        //              agent.Nationality, agent.User);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return agent;
        //}

        public async Task<AgentRequest> CreateAgent(AgentRequest agentRequest)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @masteragentcodeid = {2}, @subagentcodeid  = {3}, @agentid = {4}, @iscorp = {5}, @corpname = {6}, " +
                     "@ismerch = {7}, @merchcategory = {8}, @isbusiness = {9}, @businessname  = {10}, @phonenum = {11}, @firstname = {12}, @middlename = {13}, " +
                     "@lastname = {14}, @streetno = {15}, @town = {16}, @city  = {17}, @country = {18}, @postalcode = {19}, @comptin = {20}, " +
                     "@ctcno = {21}, @dailydeplimit = {22}, @createddatetime = {23}, @updatedatetime  = {24}, @usercreate = {25}, @lastuserupdate = {26} ",
                      agentRequest.agent.RequestID, agentRequest.agent.ApplicationID, agentRequest.agent.MasterAgentCodeID, agentRequest.agent.SubAgentCodeID, agentRequest.agent.AgentID, agentRequest.agent.IsCorporate, agentRequest.agent.CorporateName,
                      agentRequest.agent.IsMerchCategory, agentRequest.agent.MerchantCategory, agentRequest.agent.IsBusiness, agentRequest.agent.BusinessName, agentRequest.agent.PhoneNo, agentRequest.agent.FirstName, agentRequest.agent.MiddleName,
                      agentRequest.agent.LastName, agentRequest.agent.StreetNo, agentRequest.agent.Town, agentRequest.agent.City, agentRequest.agent.Country, agentRequest.agent.PostalCode, agentRequest.agent.CompanyTIN, agentRequest.agent.CTCNo, agentRequest.agent.DailyDepositLimit,
                      agentRequest.agent.CreatedDateTime, agentRequest.agent.UpdateDeteTime, agentRequest.agent.UserCrete, agentRequest.agent.LastUserUpdate);                

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @depbank = {2}, @streetno  = {3}, @town = {4}, @city = {5}, @country = {6}, " +
                     "@postalcode = {7}, @bankaccname = {8}, @rbotype = {9}, @rbofname  = {10}, @rbomname = {11}, @rbolname = {12}, @rboemail = {13}, " +
                     "@rbocontactno = {14}, @createddatetime = {15}, @updatedatetime = {16}, @usercreate  = {17}, @lastuserupdate = {18} ",
                      agentRequest.bank.RequestID, agentRequest.bank.ApplicationID, agentRequest.bank.DepositoryBank, agentRequest.bank.StreetNo, agentRequest.bank.Town, agentRequest.bank.City, agentRequest.bank.Country,
                      agentRequest.bank.PostalCode, agentRequest.bank.BankAccountName, agentRequest.bank.RBOType, agentRequest.bank.RBOFName, agentRequest.bank.RBOMName, agentRequest.bank.RBOLName, agentRequest.bank.RBOEmailAdd,
                      agentRequest.bank.RBOContactNo, agentRequest.bank.CreatedDateTime, agentRequest.bank.UpdateDeteTime, agentRequest.bank.UserCreate, agentRequest.bank.LastUserUpdate);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @firstname = {2}, @middlename  = {3}, @lastname = {4}, @designation = {5}, @department = {6}, " +
                     "@contactno = {7}, @faxno = {8}, @emailadd = {9}, @billfname  = {10}, @billmname = {11}, @billlname = {12}, @billcontactno = {13}, " +
                     "@createddatetime = {14}, @updatedatetime = {15}, @usercreate = {16}, @lastuserupdate  = {17} ",
                      agentRequest.contact.RequestID, agentRequest.contact.ApplicationID, agentRequest.contact.FirstName, agentRequest.contact.MiddleName, agentRequest.contact.LastName, agentRequest.contact.Designation, agentRequest.contact.Department,
                      agentRequest.contact.ContactNo, agentRequest.contact.FaxNo, agentRequest.contact.EmailAddress, agentRequest.contact.BillingFirstName, agentRequest.contact.BillingMiddleName, agentRequest.contact.BillingLastName, agentRequest.contact.BillingContactNo,
                      agentRequest.contact.CreatedDateTime, agentRequest.contact.UpdateDeteTime, agentRequest.contact.UserCreate, agentRequest.contact.LastUserUpdate);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @authid = {2}, @authfirstname = {3}, @authmiddlename = {4}, @authlastname = {5}, @authdesignation = {6}, @valididtype = {7}, " +
                     "@valididno = {8}, @valididexpdate = {9}, @createddatetime = {10}, @updatedatetime  = {11}, @usercreate = {12}, @lastuserupdate = {13} ",
                      agentRequest.moa.RequestID, agentRequest.moa.ApplicationID, agentRequest.moa.AuthID, agentRequest.moa.AuthFirstName, agentRequest.moa.AuthMiddleName, agentRequest.moa.AuthLastName, agentRequest.moa.AuthDesignation,
                      agentRequest.moa.ValidIDType, agentRequest.moa.ValidIDNumber, agentRequest.moa.ValidIDExpdate, agentRequest.moa.CreateDateTime, agentRequest.moa.UpdateDeteTime, agentRequest.moa.UserCreate, agentRequest.moa.LastUserUpdate);

            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            return agentRequest;
        }

        //public async Task<Agent> DeleteAgent(Agent agent)
        //{
        //    try
        //    {
        //        await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentCRUD " +
        //           "@id = {0}", agent.ID);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return agent;
        //}
    }
}
