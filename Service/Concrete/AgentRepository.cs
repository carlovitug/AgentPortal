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
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationReadAll").ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }

        public async Task<ActionResult<IEnumerable<Agent>>> GetPendingAgents()
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationPendingReadAll").ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }

        public async Task<Tuple<List<Agent>, List<Bank>, List<Contact>, List<AgentBranches>, List<Terminal>, List<BankFees>, List<Moa>>> GetAgentwithID(string requestID)
        {
            List<Agent> agent = new List<Agent>();
            List<Bank> bank = new List<Bank>();
            List<Contact> contact = new List<Contact>();
            List<AgentBranches> agentBranches = new List<AgentBranches>();
            List<Terminal> terminal = new List<Terminal>();
            List<BankFees> bankFees = new List<BankFees>();
            List<Moa> moa = new List<Moa>();
            List<AgentList> agentList = new List<AgentList>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
                bank = await _context.Bank.FromSql("dbo.SP_BankInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
                contact = await _context.Contact.FromSql("dbo.SP_ContactInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
                agentBranches = await _context.AgentBranches.FromSql("dbo.SP_AgentBranchesInformationRead " +
                     "@requestid = {0} ", requestID).ToListAsync(); 
                terminal = await _context.Terminal.FromSql("dbo.SP_TerminalInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
                bankFees = await _context.BankFees.FromSql("dbo.SP_BankFeesInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
                moa = await _context.Moa.FromSql("dbo.SP_MoaInformationRead " +
                    "@requestid = {0} ", requestID).ToListAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                throw;
            }
            return Tuple.Create(agent, bank, contact, agentBranches, terminal, bankFees, moa);
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

        public async Task<ActionResult<IEnumerable<BankFees>>> GetMGR(MGRRequest mgrRequest)
        {
            List<BankFees> bankfees = new List<BankFees>();
            try
            {
                bankfees = await _context.BankFees.FromSql("dbo.SP_GetMGR " +
                    "@requestid = {0} ", mgrRequest.RequestID).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return bankfees;
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

        public async Task<bool> UpgradeDailyDepositLimit(DailyDepositLimitRequest dailyDepositLimitRequest)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UpgradeDailyDepositLimit " +
                    "@requestid = {0}, @dailydeplimit = {1} ", dailyDepositLimitRequest.RequestID, dailyDepositLimitRequest.DailyDepLimit);
                return true;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateMGR(MGRRequest mgrRequest)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UpdateMGR " +
                    "@requestid = {0}, @mgr = {1} ", mgrRequest.RequestID, mgrRequest.MGR);
                return true;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePhone(PhoneRequest phoneRequest)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_UpdatePhone " +
                    "@requestid = {0}, @phoneno = {1} ", phoneRequest.RequestID, phoneRequest.PhoneNo);
                return true;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }


        public async Task<ChangeStatus> ChangeStatus(ChangeStatus status)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationChangeStatus " +
                     "@requestid = {0}, @status = {1} " , status.RequestID, status.Status);
            }
            catch (Exception Ex)
            {
                throw;
            }
            return status;
        }
        

        public async Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents()
        {
            List<MasterAgentID> masteragentID = new List<MasterAgentID>();
            try
            {
                masteragentID = await _context.MasterAgentID.FromSql("dbo.SP_MasterAgentReadAll ").ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return masteragentID;
        }


        public async Task<Tuple<Agent, Bank, Contact>> CreateAgent(Agent agent, Bank bank, Contact contact)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationCreate " +
                     "@requestid = {0}, @masteragentcodeid = {1}, @subagentcodeid  = {2}, @agentid = {3}, @iscorp = {4}, @corpname = {5}, " +
                     "@ismerch = {6}, @merchcategory = {7}, @isbusiness = {8}, @businessname  = {9}, @phonenum = {10}, @firstname = {11}, @middlename = {12}, " +
                     "@lastname = {13}, @streetno = {14}, @town = {15}, @city  = {16}, @country = {17}, @postalcode = {18}, @comptin = {19}, " +
                     "@ctcno = {20}, @dailydeplimit = {21}, @createddatetime = {22}, @updatedatetime  = {23}, @usercreate = {24}, @lastuserupdate = {25}, @status = {26}, @isdeleted = {27} ",
                      agent.RequestID, agent.MasterAgentCodeID, agent.SubAgentCodeID, agent.AgentID, agent.IsCorporate, agent.CorporateName,
                      agent.IsMerchCategory, agent.MerchantCategory, agent.IsBusiness, agent.BusinessName, agent.PhoneNo, agent.FirstName, agent.MiddleName,
                      agent.LastName, agent.StreetNo, agent.Town, agent.City, agent.Country, agent.PostalCode, agent.CompanyTIN, agent.CTCNo, agent.DailyDepositLimit,
                      agent.CreatedDateTime, agent.UpdateDeteTime, agent.UserCreate, agent.LastUserUpdate, agent.Status, agent.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationCreate " +
                     "@requestid = {0}, @depbank = {1}, @streetno  = {2}, @town = {3}, @city = {4}, @country = {5}, " +
                     "@postalcode = {6}, @bankaccname = {7}, @rbotype = {8}, @rbofname  = {9}, @rbomname = {10}, @rbolname = {11}, @rboemail = {12}, " +
                     "@rbocontactno = {13}, @createddatetime = {14}, @updatedatetime = {15}, @usercreate  = {16}, @lastuserupdate = {17}, @isdeleted = {18} ",
                      bank.RequestID, bank.DepositoryBank, bank.StreetNo, bank.Town, bank.City, bank.Country,
                      bank.PostalCode, bank.BankAccountName, bank.RBOType, bank.RBOFName, bank.RBOMName, bank.RBOLName, bank.RBOEmailAdd,
                      bank.RBOContactNo, bank.CreatedDateTime, bank.UpdateDeteTime, bank.UserCreate, bank.LastUserUpdate, bank.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationCreate " +
                     "@requestid = {0}, @firstname = {1}, @middlename  = {2}, @lastname = {3}, @designation = {4}, @department = {5}, " +
                     "@contactno = {6}, @faxno = {7}, @emailadd = {8}, @billfname  = {9}, @billmname = {10}, @billlname = {11}, @billcontactno = {12}, " +
                     "@createddatetime = {13}, @updatedatetime = {14}, @usercreate = {15}, @lastuserupdate  = {16}, @isdeleted = {17} ",
                      contact.RequestID, contact.FirstName, contact.MiddleName, contact.LastName, contact.Designation, contact.Department,
                      contact.ContactNo, contact.FaxNo, contact.EmailAddress, contact.BillingFirstName, contact.BillingMiddleName, contact.BillingLastName, contact.BillingContactNo,
                      contact.CreatedDateTime, contact.UpdateDeteTime, contact.UserCreate, contact.LastUserUpdate, contact.IsDeleted);
                
            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully created - Agent, Bank, Contact", DateTime.Now);
            return Tuple.Create(agent, bank, contact);
        }

        public async Task<Moa> CreateMoa(Moa moa)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationCreate " +
                     "@requestid = {0}, @authid = {1}, @authfirstname = {2}, @authmiddlename = {3}, @authlastname = {4}, @authdesignation = {5}, @valididtype = {6}, " +
                     "@valididno = {7}, @valididexpdate = {8}, @createddatetime = {9}, @updatedatetime  = {10}, @usercreate = {11}, @lastuserupdate = {12}, @isdeleted = {13} ",
                      moa.RequestID, moa.AuthID, moa.AuthFirstName, moa.AuthMiddleName, moa.AuthLastName, moa.AuthDesignation,
                      moa.ValidIDType, moa.ValidIDNumber, moa.ValidIDExpdate, moa.CreatedDateTime, moa.UpdateDeteTime, moa.UserCreate, moa.LastUserUpdate, moa.IsDeleted);

            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully created - Moa", DateTime.Now);
            return moa;
        }

        public async Task<Terminal> CreateTerminal(Terminal terminal)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_TerminalInformationCreate " +
                     "@requestid = {0}, @posterminalname = {1}, @typeofposterminal = {2}, " +
                     "@createddatetime = {3}, @updatedatetime  = {4}, @usercreate = {5}, @lastuserupdate = {6}, @isdeleted = {7}, @terminalid = {8}, @merchantid = {9} ",
                      terminal.RequestID, terminal.POSTerminalName, terminal.TypeOfPOSTerminal, terminal.CreatedDateTime, terminal.UpdateDeteTime, terminal.UserCreate, terminal.LastUserUpdate, terminal.IsDeleted, terminal.TerminalID, terminal.MerchantID);
            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully created - Terminal", DateTime.Now);
            return terminal;
        }

        public async Task<AgentBranches> CreateAgentBranches(AgentBranches agentBranches)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentBranchesInformationCreate " +
                     "@requestid = {0}, @agentbranchname = {1}, @streetno = {2}, " +
                     "@town = {3}, @city = {4}, @country = {5}, @postalcode = {6}, @phoneno = {7}, " +
                     "@createddatetime = {8}, @updatedatetime = {9}, @usercreate = {10}, @lastuserupdate  = {11}, @isdeleted = {12} ",
                      agentBranches.RequestID, agentBranches.AgentBranchName, agentBranches.StreetNo, agentBranches.Town, agentBranches.City, agentBranches.Country, agentBranches.PostalCode,
                      agentBranches.PhoneNo, agentBranches.CreatedDateTime, agentBranches.UpdateDeteTime, agentBranches.UserCreate, agentBranches.LastUserUpdate, agentBranches.IsDeleted);
            }
            
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully created - Agent Branches", DateTime.Now);
            return agentBranches;
        }

        public async Task<BankFees> CreateBankFees(BankFees bankFees)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankFeesInformationCreate " +
                     "@requestid = {0}, @agentid = {1}, @merchantid = {2}, " +
                     "@transactiontype = {3}, @conveniencefee  = {4}, @quota = {5}, @createddatetime = {6}, " +
                     "@updatedatetime = {7}, @usercreate  = {8}, @lastuserupdate = {9}, @isdeleted = {10}", 
                      bankFees.RequestID, bankFees.AgentID, bankFees.MerchantID, bankFees.TransactionType, bankFees.ConvenienceFee, bankFees.Quota,
                      bankFees.CreatedDateTime, bankFees.UpdateDeteTime, bankFees.UserCreate, bankFees.LastUserUpdate, bankFees.IsDeleted);
            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully created - Bank Fees", DateTime.Now);
            return bankFees;
        }

        public async Task<Tuple<Agent, Bank, Contact>> UpdateAgent(Agent agent, Bank bank, Contact contact)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentInformationUpdate " +
                     "@requestid = {0}, @masteragentcodeid = {1}, @subagentcodeid  = {2}, @agentid = {3}, @iscorp = {4}, @corpname = {5}, " +
                     "@ismerch = {6}, @merchcategory = {7}, @isbusiness = {8}, @businessname  = {9}, @phonenum = {10}, @firstname = {11}, @middlename = {12}, " +
                     "@lastname = {13}, @streetno = {14}, @town = {15}, @city  = {16}, @country = {17}, @postalcode = {18}, @comptin = {19}, " +
                     "@ctcno = {20}, @dailydeplimit = {21}, @updatedatetime = {22}, @lastuserupdate  = {23}, @status = {24}, @isdeleted = {25} ",
                      agent.RequestID, agent.MasterAgentCodeID, agent.SubAgentCodeID, agent.AgentID, agent.IsCorporate, agent.CorporateName,
                      agent.IsMerchCategory, agent.MerchantCategory, agent.IsBusiness, agent.BusinessName, agent.PhoneNo, agent.FirstName, agent.MiddleName,
                      agent.LastName, agent.StreetNo, agent.Town, agent.City, agent.Country, agent.PostalCode, agent.CompanyTIN, agent.CTCNo, agent.DailyDepositLimit,
                      agent.UpdateDeteTime, agent.LastUserUpdate, agent.Status, agent.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationUpdate " +
                     "@requestid = {0}, @depbank = {1}, @streetno  = {2}, @town = {3}, @city = {4}, @country = {5}, " +
                     "@postalcode = {6}, @bankaccname = {7}, @rbotype = {8}, @rbofname  = {9}, @rbomname = {10}, @rbolname = {11}, @rboemail = {12}, " +
                     "@rbocontactno = {13}, @updatedatetime = {14}, @lastuserupdate = {15}, @isdeleted  = {16} ",
                      bank.RequestID, bank.DepositoryBank, bank.StreetNo, bank.Town, bank.City, bank.Country,
                      bank.PostalCode, bank.BankAccountName, bank.RBOType, bank.RBOFName, bank.RBOMName, bank.RBOLName, bank.RBOEmailAdd,
                      bank.RBOContactNo, bank.UpdateDeteTime, bank.LastUserUpdate, bank.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationUpdate " +
                     "@requestid = {0}, @firstname = {1}, @middlename  = {2}, @lastname = {3}, @designation = {4}, @department = {5}, " +
                     "@contactno = {6}, @faxno = {7}, @emailadd = {8}, @billfname  = {9}, @billmname = {10}, @billlname = {11}, @billcontactno = {12}, " +
                     "@updatedatetime = {13}, @lastuserupdate = {14}, @isdeleted = {15} ",
                      contact.RequestID, contact.FirstName, contact.MiddleName, contact.LastName, contact.Designation, contact.Department,
                      contact.ContactNo, contact.FaxNo, contact.EmailAddress, contact.BillingFirstName, contact.BillingMiddleName, contact.BillingLastName, contact.BillingContactNo,
                      contact.UpdateDeteTime, contact.LastUserUpdate, contact.IsDeleted);

            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully updated - Agent, Bank, Contact", DateTime.Now);
            return Tuple.Create(agent, bank, contact);
        }

        public async Task<Moa> UpdateMoa(Moa moa)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationUpdate " +
                     "@requestid = {0}, @authid = {1}, @authfirstname = {2}, @authmiddlename = {3}, @authlastname = {4}, @authdesignation = {5}, @valididtype = {6}, " +
                     "@valididno = {7}, @valididexpdate = {8}, @createddatetime = {9}, @updatedatetime  = {10}, @usercreate = {11}, @lastuserupdate = {12}, @isdeleted = {13} ",
                      moa.RequestID, moa.AuthID, moa.AuthFirstName, moa.AuthMiddleName, moa.AuthLastName, moa.AuthDesignation,
                      moa.ValidIDType, moa.ValidIDNumber, moa.ValidIDExpdate, moa.CreatedDateTime, moa.UpdateDeteTime, moa.UserCreate, moa.LastUserUpdate, moa.IsDeleted);

            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully updated - Moa", DateTime.Now);
            return moa;
        }

        public async Task<Terminal> UpdateTerminal(Terminal terminal)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_TerminalInformationUpdate " +
                     "@requestid = {0}, @posterminalname = {1}, @typeofposterminal = {2}, " +
                     "@createddatetime = {3}, @updatedatetime  = {4}, @usercreate = {5}, @lastuserupdate = {6}, @isdeleted = {7}, @terminalid = {8}, @merchantid = {9} ",
                      terminal.RequestID, terminal.POSTerminalName, terminal.TypeOfPOSTerminal, terminal.CreatedDateTime, terminal.UpdateDeteTime, terminal.UserCreate, terminal.LastUserUpdate, terminal.IsDeleted, terminal.TerminalID, terminal.MerchantID);
            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully updated - Terminal", DateTime.Now);
            return terminal;
        }

        public async Task<AgentBranches> UpdateAgentBranches(AgentBranches agentBranches)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentBranchesInformationUpdate " +
                     "@requestid = {0}, @agentbranchname = {1}, @streetno = {2}, " +
                     "@town = {3}, @city = {4}, @country = {5}, @postalcode = {6}, @phoneno = {7}, " +
                     "@createddatetime = {8}, @updatedatetime = {9}, @usercreate = {10}, @lastuserupdate  = {11}, @isdeleted = {12} ",
                      agentBranches.RequestID, agentBranches.AgentBranchName, agentBranches.StreetNo, agentBranches.Town, agentBranches.City, agentBranches.Country, agentBranches.PostalCode,
                      agentBranches.PhoneNo, agentBranches.CreatedDateTime, agentBranches.UpdateDeteTime, agentBranches.UserCreate, agentBranches.LastUserUpdate, agentBranches.IsDeleted);
            }

            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully updated - Agent Branches", DateTime.Now);
            return agentBranches;
        }

        public async Task<BankFees> UpdateBankFees(BankFees bankFees)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankFeesInformationUpdate " +
                     "@requestid = {0}, @agentid = {1}, @merchantid = {2}, " +
                     "@transactiontype = {3}, @conveniencefee  = {4}, @quota = {5}, @createddatetime = {6}, " +
                     "@updatedatetime = {7}, @usercreate  = {8}, @lastuserupdate = {9}, @isdeleted = {10}",
                      bankFees.RequestID, bankFees.AgentID, bankFees.MerchantID, bankFees.TransactionType, bankFees.ConvenienceFee, bankFees.Quota,
                      bankFees.CreatedDateTime, bankFees.UpdateDeteTime, bankFees.UserCreate, bankFees.LastUserUpdate, bankFees.IsDeleted);
            }
            catch (Exception ex)
            {
                var temp = "Error - " + ex;
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", temp, DateTime.Now);
                throw;
            }
            await _context.Database.ExecuteSqlCommandAsync("dbo.SP_LogsInformationCreate " +
                     "@logmessage = {0}, @logdatetime = {1} ", "Successfully updated - Bank Fees", DateTime.Now);
            return bankFees;
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

        public async Task<bool> DeleteAgentBranches(string agentRequestID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_AgentBranchesInformationDelete @requestid = {0}", agentRequestID);
            }
            catch (Exception Ex)
            {
                throw;

            }
            return true;
        }
        public async Task<bool> DeleteBankFees(string agentRequestID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankFeesInformationDelete @requestid = {0}", agentRequestID);
            }
            catch (Exception Ex)
            {
                throw;

            }
            return true;
        }
        public async Task<bool> DeleteMoa(string agentRequestID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_MoaInformationDelete @requestid = {0}", agentRequestID);
            }
            catch (Exception Ex)
            {
                throw;

            }
            return true;
        }
        public async Task<bool> DeleteTerminal(string agentRequestID)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_TerminalInformationDelete @requestid = {0}", agentRequestID);
            }
            catch (Exception Ex)
            {
                throw;

            }
            return true;
        }

        public async Task<bool> CheckExistingAgentID(int agentID)
        {
            bool agentIDresult = await _context.AgentInformation.AnyAsync(x => x.AgentID == agentID);
            return agentIDresult;
        }

        public async Task<bool> CheckExistingRequestID(string requestID)
        {
            bool requestIDresult = await _context.AgentInformation.AnyAsync(x => x.RequestID == requestID);
            return requestIDresult;
        }
        public async Task<bool> CheckExistingMasterAgentCodeID(int masterAgentCodeID)
        {
            bool masterAgentCodeIDresult = await _context.AgentInformation.AnyAsync(x => x.MasterAgentCodeID == masterAgentCodeID);
            return masterAgentCodeIDresult;
        }

        public async Task<string> GetRequestID(int id)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_GetRequestID " +
                    "@id = {0} ", id).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent.FirstOrDefault()?.RequestID;
        }
    }
}
