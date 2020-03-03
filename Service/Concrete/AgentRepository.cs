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
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            List<Agent> agent = new List<Agent>();
            List<Bank> bank = new List<Bank>();
            List<Contact> contact = new List<Contact>();
            List<Moa> moa = new List<Moa>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationReadAll").ToListAsync();
                bank = await _context.Bank.FromSql("dbo.SP_BankInformationReadAll").ToListAsync();
                contact = await _context.Contact.FromSql("dbo.SP_ContactInformationReadAll").ToListAsync();
                moa = await _context.Moa.FromSql("dbo.SP_MoaInformationReadAll").ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }
        
        public async Task<ActionResult<IEnumerable<Agent>>> GetSubAgents(int agentRequestID)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_SubAgentReadAll " +
                    "@masteragentcodeid = {0} ", agentRequestID).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }

        public async Task<ActionResult<IEnumerable<Agent>>> GetMasterAgentID(int agentRequestID)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_MasterAgentIDReadAll " +
                    "@masteragentcodeid = {0} ", agentRequestID).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }

        public async Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents()
        {
            List<MasterAgentID> masteragentID = new List<MasterAgentID>();
            try
            {
                masteragentID = await _context.MasterAgentID.FromSql("dbo.SP_MasterAgentReadAll").ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return masteragentID;
        }

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

        public async Task<AgentRequest> UpdateAgent(AgentRequest agentRequest)
        {
            try
            {
                //await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationUpdate " +
                //     "@id = {0}, @requestid = {1}, @applicationid = {2}, @masteragentcodeid = {3}, @subagentcodeid  = {4}, @agentid = {5}, @iscorp = {6}, @corpname = {7}, " +
                //     "@ismerch = {8}, @merchcategory = {9}, @isbusiness = {10}, @businessname  = {11}, @phonenum = {12}, @firstname = {13}, @middlename = {14}, " +
                //     "@lastname = {15}, @streetno = {16}, @town = {17}, @city  = {18}, @country = {19}, @postalcode = {20}, @comptin = {21}, " +
                //     "@ctcno = {22}, @dailydeplimit = {23}, @createddatetime = {24}, @updatedatetime  = {25}, @usercreate = {26}, @lastuserupdate = {27}, @isdeleted = {28} ",
                //      agentRequest.agent.ID, agentRequest.agent.RequestID, agentRequest.agent.ApplicationID, agentRequest.agent.MasterAgentCodeID, agentRequest.agent.SubAgentCodeID, agentRequest.agent.AgentID, agentRequest.agent.IsCorporate, agentRequest.agent.CorporateName,
                //      agentRequest.agent.IsMerchCategory, agentRequest.agent.MerchantCategory, agentRequest.agent.IsBusiness, agentRequest.agent.BusinessName, agentRequest.agent.PhoneNo, agentRequest.agent.FirstName, agentRequest.agent.MiddleName,
                //      agentRequest.agent.LastName, agentRequest.agent.StreetNo, agentRequest.agent.Town, agentRequest.agent.City, agentRequest.agent.Country, agentRequest.agent.PostalCode, agentRequest.agent.CompanyTIN, agentRequest.agent.CTCNo, agentRequest.agent.DailyDepositLimit,
                //      agentRequest.agent.CreatedDateTime, agentRequest.agent.UpdateDeteTime, agentRequest.agent.UserCreate, agentRequest.agent.LastUserUpdate, agentRequest.agent.IsDeleted);

                //await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationUpdate " +
                //     "@requestid = {0}, @applicationid = {1}, @depbank = {2}, @streetno  = {3}, @town = {4}, @city = {5}, @country = {6}, " +
                //     "@postalcode = {7}, @bankaccname = {8}, @rbotype = {9}, @rbofname  = {10}, @rbomname = {11}, @rbolname = {12}, @rboemail = {13}, " +
                //     "@rbocontactno = {14}, @createddatetime = {15}, @updatedatetime = {16}, @usercreate  = {17}, @lastuserupdate = {18}, @isdeleted = {19} ",
                //      agentRequest.bank.RequestID, agentRequest.bank.ApplicationID, agentRequest.bank.DepositoryBank, agentRequest.bank.StreetNo, agentRequest.bank.Town, agentRequest.bank.City, agentRequest.bank.Country,
                //      agentRequest.bank.PostalCode, agentRequest.bank.BankAccountName, agentRequest.bank.RBOType, agentRequest.bank.RBOFName, agentRequest.bank.RBOMName, agentRequest.bank.RBOLName, agentRequest.bank.RBOEmailAdd,
                //      agentRequest.bank.RBOContactNo, agentRequest.bank.CreatedDateTime, agentRequest.bank.UpdateDeteTime, agentRequest.bank.UserCreate, agentRequest.bank.LastUserUpdate, agentRequest.bank.IsDeleted);

                //await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationUpdate " +
                //     "@requestid = {0}, @applicationid = {1}, @firstname = {2}, @middlename  = {3}, @lastname = {4}, @designation = {5}, @department = {6}, " +
                //     "@contactno = {7}, @faxno = {8}, @emailadd = {9}, @billfname  = {10}, @billmname = {11}, @billlname = {12}, @billcontactno = {13}, " +
                //     "@createddatetime = {14}, @updatedatetime = {15}, @usercreate = {16}, @lastuserupdate  = {17}, @isdeleted = {18} ",
                //      agentRequest.contact.RequestID, agentRequest.contact.ApplicationID, agentRequest.contact.FirstName, agentRequest.contact.MiddleName, agentRequest.contact.LastName, agentRequest.contact.Designation, agentRequest.contact.Department,
                //      agentRequest.contact.ContactNo, agentRequest.contact.FaxNo, agentRequest.contact.EmailAddress, agentRequest.contact.BillingFirstName, agentRequest.contact.BillingMiddleName, agentRequest.contact.BillingLastName, agentRequest.contact.BillingContactNo,
                //      agentRequest.contact.CreatedDateTime, agentRequest.contact.UpdateDeteTime, agentRequest.contact.UserCreate, agentRequest.contact.LastUserUpdate, agentRequest.contact.IsDeleted);

                //await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationUpdate " +
                //     "@requestid = {0}, @applicationid = {1}, @authid = {2}, @authfirstname = {3}, @authmiddlename = {4}, @authlastname = {5}, @authdesignation = {6}, @valididtype = {7}, " +
                //     "@valididno = {8}, @valididexpdate = {9}, @createddatetime = {10}, @updatedatetime  = {11}, @usercreate = {12}, @lastuserupdate = {13}, @isdeleted = {14} ",
                //      agentRequest.moa.RequestID, agentRequest.moa.ApplicationID, agentRequest.moa.AuthID, agentRequest.moa.AuthFirstName, agentRequest.moa.AuthMiddleName, agentRequest.moa.AuthLastName, agentRequest.moa.AuthDesignation,
                //      agentRequest.moa.ValidIDType, agentRequest.moa.ValidIDNumber, agentRequest.moa.ValidIDExpdate, agentRequest.moa.CreatedDateTime, agentRequest.moa.UpdateDeteTime, agentRequest.moa.UserCreate, agentRequest.moa.LastUserUpdate, agentRequest.moa.IsDeleted);
            }
            catch (Exception ex)
            {
                throw;
            }

            return agentRequest;
        }

        public async Task<Tuple<Agent, Bank, Contact, AgentBranches>> CreateAgent(Agent agent, Bank bank, Contact contact, AgentBranches agentBranches)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @masteragentcodeid = {2}, @subagentcodeid  = {3}, @agentid = {4}, @iscorp = {5}, @corpname = {6}, " +
                     "@ismerch = {7}, @merchcategory = {8}, @isbusiness = {9}, @businessname  = {10}, @phonenum = {11}, @firstname = {12}, @middlename = {13}, " +
                     "@lastname = {14}, @streetno = {15}, @town = {16}, @city  = {17}, @country = {18}, @postalcode = {19}, @comptin = {20}, " +
                     "@ctcno = {21}, @dailydeplimit = {22}, @createddatetime = {23}, @updatedatetime  = {24}, @usercreate = {25}, @lastuserupdate = {26}, @isdeleted = {27} ",
                      agent.RequestID, agent.ApplicationID, agent.MasterAgentCodeID, agent.SubAgentCodeID, agent.AgentID, agent.IsCorporate, agent.CorporateName,
                      agent.IsMerchCategory, agent.MerchantCategory, agent.IsBusiness, agent.BusinessName, agent.PhoneNo, agent.FirstName, agent.MiddleName,
                      agent.LastName, agent.StreetNo, agent.Town, agent.City, agent.Country, agent.PostalCode, agent.CompanyTIN, agent.CTCNo, agent.DailyDepositLimit,
                      agent.CreatedDateTime, agent.UpdateDeteTime, agent.UserCreate, agent.LastUserUpdate, agent.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @depbank = {2}, @streetno  = {3}, @town = {4}, @city = {5}, @country = {6}, " +
                     "@postalcode = {7}, @bankaccname = {8}, @rbotype = {9}, @rbofname  = {10}, @rbomname = {11}, @rbolname = {12}, @rboemail = {13}, " +
                     "@rbocontactno = {14}, @createddatetime = {15}, @updatedatetime = {16}, @usercreate  = {17}, @lastuserupdate = {18}, @isdeleted = {19} ",
                      bank.RequestID, bank.ApplicationID, bank.DepositoryBank, bank.StreetNo, bank.Town, bank.City, bank.Country,
                      bank.PostalCode, bank.BankAccountName, bank.RBOType, bank.RBOFName, bank.RBOMName, bank.RBOLName, bank.RBOEmailAdd,
                      bank.RBOContactNo, bank.CreatedDateTime, bank.UpdateDeteTime, bank.UserCreate, bank.LastUserUpdate, bank.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @firstname = {2}, @middlename  = {3}, @lastname = {4}, @designation = {5}, @department = {6}, " +
                     "@contactno = {7}, @faxno = {8}, @emailadd = {9}, @billfname  = {10}, @billmname = {11}, @billlname = {12}, @billcontactno = {13}, " +
                     "@createddatetime = {14}, @updatedatetime = {15}, @usercreate = {16}, @lastuserupdate  = {17}, @isdeleted = {18} ",
                      contact.RequestID, contact.ApplicationID, contact.FirstName, contact.MiddleName, contact.LastName, contact.Designation, contact.Department,
                      contact.ContactNo, contact.FaxNo, contact.EmailAddress, contact.BillingFirstName, contact.BillingMiddleName, contact.BillingLastName, contact.BillingContactNo,
                      contact.CreatedDateTime, contact.UpdateDeteTime, contact.UserCreate, contact.LastUserUpdate, contact.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentBranchesInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @noagentoutletsorbranches = {2}, " +
                     "@createddatetime = {3}, @updatedatetime = {4}, @usercreate = {5}, @lastuserupdate  = {6}, @isdeleted = {7} ",
                      agentBranches.RequestID, agentBranches.ApplicationID, agentBranches.NoAgentOutletsOrBranches,
                      agentBranches.CreatedDateTime, agentBranches.UpdateDeteTime, agentBranches.UserCreate, agentBranches.LastUserUpdate, agentBranches.IsDeleted);
                
            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            return Tuple.Create(agent, bank, contact, agentBranches);
        }

        public async Task<Moa> CreateMoa(Moa moa)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @authid = {2}, @authfirstname = {3}, @authmiddlename = {4}, @authlastname = {5}, @authdesignation = {6}, @valididtype = {7}, " +
                     "@valididno = {8}, @valididexpdate = {9}, @createddatetime = {10}, @updatedatetime  = {11}, @usercreate = {12}, @lastuserupdate = {13}, @isdeleted = {14} ",
                      moa.RequestID, moa.ApplicationID, moa.AuthID, moa.AuthFirstName, moa.AuthMiddleName, moa.AuthLastName, moa.AuthDesignation,
                      moa.ValidIDType, moa.ValidIDNumber, moa.ValidIDExpdate, moa.CreatedDateTime, moa.UpdateDeteTime, moa.UserCreate, moa.LastUserUpdate, moa.IsDeleted);

            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            return moa;
        }

        public async Task<Terminal> CreateTerminal(Terminal terminal)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_TerminalInformationCreate " +
                     "@requestid = {0}, @applicationid = {1}, @posterminalname = {2}, @typeofposterminal = {3}, " +
                     "@createddatetime = {4}, @updatedatetime  = {5}, @usercreate = {6}, @lastuserupdate = {7}, @isdeleted = {8} ",
                      terminal.RequestID, terminal.ApplicationID, terminal.POSTerminalName, terminal.POSTerminalName, terminal.CreatedDateTime, terminal.UpdateDeteTime, terminal.UserCreate, terminal.LastUserUpdate, terminal.IsDeleted);
            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }
            return terminal;
        }

        public async Task<int> DeleteAgent(int agentRequestID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationDelete @id = {0}", agentRequestID);
            }
            catch (Exception)
            {
                throw;
            }
            return agentRequestID;
        }

        public async Task<bool> CheckExistingAgentID(int agentID)
        {
            bool result = await _context.Agent.AnyAsync(x => x.AgentID == agentID);
            return result;
        }
    }
}
