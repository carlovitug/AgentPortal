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
        

        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents(int applicationID)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationReadAll " +
                    "@applicationID = {0} ", applicationID).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw;
            }
            return agent;
        }

        public async Task<ActionResult<IEnumerable<Agent>>> GetPendingAgents(int applicationID)
        {
            List<Agent> agent = new List<Agent>();
            try
            {
                agent = await _context.Agent.FromSql("dbo.SP_AgentInformationPendingReadAll " +
                    "@applicationID = {0} ", applicationID).ToListAsync();
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
        

        public async Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents(int applicationID)
        {
            List<MasterAgentID> masteragentID = new List<MasterAgentID>();
            try
            {
                masteragentID = await _context.MasterAgentID.FromSql("dbo.SP_MasterAgentReadAll " +
                       "@applicationID = {0} ", applicationID).ToListAsync();
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
                     "@requestid = {0}, @applicationid = {1}, @masteragentcodeid = {2}, @subagentcodeid  = {3}, @agentid = {4}, @iscorp = {5}, @corpname = {6}, " +
                     "@ismerch = {7}, @merchcategory = {8}, @isbusiness = {9}, @businessname  = {10}, @phonenum = {11}, @firstname = {12}, @middlename = {13}, " +
                     "@lastname = {14}, @streetno = {15}, @town = {16}, @city  = {17}, @country = {18}, @postalcode = {19}, @comptin = {20}, " +
                     "@ctcno = {21}, @dailydeplimit = {22}, @createddatetime = {23}, @updatedatetime  = {24}, @usercreate = {25}, @lastuserupdate = {26}, @status = {27}, @isdeleted = {28} ",
                      agent.RequestID, agent.ApplicationID, agent.MasterAgentCodeID, agent.SubAgentCodeID, agent.AgentID, agent.IsCorporate, agent.CorporateName,
                      agent.IsMerchCategory, agent.MerchantCategory, agent.IsBusiness, agent.BusinessName, agent.PhoneNo, agent.FirstName, agent.MiddleName,
                      agent.LastName, agent.StreetNo, agent.Town, agent.City, agent.Country, agent.PostalCode, agent.CompanyTIN, agent.CTCNo, agent.DailyDepositLimit,
                      agent.CreatedDateTime, agent.UpdateDeteTime, agent.UserCreate, agent.LastUserUpdate, agent.Status, agent.IsDeleted);

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
                     "@requestid = {0}, @applicationid = {1}, @authid = {2}, @authfirstname = {3}, @authmiddlename = {4}, @authlastname = {5}, @authdesignation = {6}, @valididtype = {7}, " +
                     "@valididno = {8}, @valididexpdate = {9}, @createddatetime = {10}, @updatedatetime  = {11}, @usercreate = {12}, @lastuserupdate = {13}, @isdeleted = {14} ",
                      moa.RequestID, moa.ApplicationID, moa.AuthID, moa.AuthFirstName, moa.AuthMiddleName, moa.AuthLastName, moa.AuthDesignation,
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
                     "@requestid = {0}, @applicationid = {1}, @posterminalname = {2}, @typeofposterminal = {3}, " +
                     "@createddatetime = {4}, @updatedatetime  = {5}, @usercreate = {6}, @lastuserupdate = {7}, @isdeleted = {8} ",
                      terminal.RequestID, terminal.ApplicationID, terminal.POSTerminalName, terminal.TypeOfPOSTerminal, terminal.CreatedDateTime, terminal.UpdateDeteTime, terminal.UserCreate, terminal.LastUserUpdate, terminal.IsDeleted);
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
                     "@requestid = {0}, @applicationid = {1}, @agentbranchname = {2}, @streetno = {3}, " +
                     "@town = {4}, @city = {5}, @country = {6}, @postalcode = {7}, @phoneno = {8}, " +
                     "@createddatetime = {9}, @updatedatetime = {10}, @usercreate = {11}, @lastuserupdate  = {12}, @isdeleted = {13} ",
                      agentBranches.RequestID, agentBranches.ApplicationID, agentBranches.AgentBranchName, agentBranches.StreetNo, agentBranches.Town, agentBranches.City, agentBranches.Country, agentBranches.PostalCode,
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
                     "@requestid = {0}, @applicationid = {1}, @agentid = {2}, @merchantid = {3}, " +
                     "@transactiontype = {4}, @conveniencefee  = {5}, @quota = {6}, @createddatetime = {7}, " +
                     "@updatedatetime = {8}, @usercreate  = {9}, @lastuserupdate = {10}, @isdeleted = {11}", 
                      bankFees.RequestID, bankFees.ApplicationID, bankFees.AgentID, bankFees.MerchantID, bankFees.TransactionType, bankFees.ConvenienceFee, bankFees.Quota,
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
                     "@requestid = {0}, @applicationid = {1}, @masteragentcodeid = {2}, @subagentcodeid  = {3}, @agentid = {4}, @iscorp = {5}, @corpname = {6}, " +
                     "@ismerch = {7}, @merchcategory = {8}, @isbusiness = {9}, @businessname  = {10}, @phonenum = {11}, @firstname = {12}, @middlename = {13}, " +
                     "@lastname = {14}, @streetno = {15}, @town = {16}, @city  = {17}, @country = {18}, @postalcode = {19}, @comptin = {20}, " +
                     "@ctcno = {21}, @dailydeplimit = {22}, @updatedatetime = {23}, @lastuserupdate  = {24}, @status = {25}, @isdeleted = {26} ",
                      agent.RequestID, agent.ApplicationID, agent.MasterAgentCodeID, agent.SubAgentCodeID, agent.AgentID, agent.IsCorporate, agent.CorporateName,
                      agent.IsMerchCategory, agent.MerchantCategory, agent.IsBusiness, agent.BusinessName, agent.PhoneNo, agent.FirstName, agent.MiddleName,
                      agent.LastName, agent.StreetNo, agent.Town, agent.City, agent.Country, agent.PostalCode, agent.CompanyTIN, agent.CTCNo, agent.DailyDepositLimit,
                      agent.UpdateDeteTime, agent.LastUserUpdate, agent.Status, agent.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_BankInformationUpdate " +
                     "@requestid = {0}, @applicationid = {1}, @depbank = {2}, @streetno  = {3}, @town = {4}, @city = {5}, @country = {6}, " +
                     "@postalcode = {7}, @bankaccname = {8}, @rbotype = {9}, @rbofname  = {10}, @rbomname = {11}, @rbolname = {12}, @rboemail = {13}, " +
                     "@rbocontactno = {14}, @updatedatetime = {15}, @lastuserupdate = {16}, @isdeleted  = {17} ",
                      bank.RequestID, bank.ApplicationID, bank.DepositoryBank, bank.StreetNo, bank.Town, bank.City, bank.Country,
                      bank.PostalCode, bank.BankAccountName, bank.RBOType, bank.RBOFName, bank.RBOMName, bank.RBOLName, bank.RBOEmailAdd,
                      bank.RBOContactNo, bank.UpdateDeteTime, bank.LastUserUpdate, bank.IsDeleted);

                await _context.Database.ExecuteSqlCommandAsync("dbo.SP_ContactInformationUpdate " +
                     "@requestid = {0}, @applicationid = {1}, @firstname = {2}, @middlename  = {3}, @lastname = {4}, @designation = {5}, @department = {6}, " +
                     "@contactno = {7}, @faxno = {8}, @emailadd = {9}, @billfname  = {10}, @billmname = {11}, @billlname = {12}, @billcontactno = {13}, " +
                     "@updatedatetime = {14}, @lastuserupdate = {15}, @isdeleted = {16} ",
                      contact.RequestID, contact.ApplicationID, contact.FirstName, contact.MiddleName, contact.LastName, contact.Designation, contact.Department,
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
                     "@requestid = {0}, @applicationid = {1}, @authid = {2}, @authfirstname = {3}, @authmiddlename = {4}, @authlastname = {5}, @authdesignation = {6}, @valididtype = {7}, " +
                     "@valididno = {8}, @valididexpdate = {9}, @createddatetime = {10}, @updatedatetime  = {11}, @usercreate = {12}, @lastuserupdate = {13}, @isdeleted = {14} ",
                      moa.RequestID, moa.ApplicationID, moa.AuthID, moa.AuthFirstName, moa.AuthMiddleName, moa.AuthLastName, moa.AuthDesignation,
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
                     "@requestid = {0}, @applicationid = {1}, @posterminalname = {2}, @typeofposterminal = {3}, " +
                     "@createddatetime = {4}, @updatedatetime  = {5}, @usercreate = {6}, @lastuserupdate = {7}, @isdeleted = {8} ",
                      terminal.RequestID, terminal.ApplicationID, terminal.POSTerminalName, terminal.TypeOfPOSTerminal, terminal.CreatedDateTime, terminal.UpdateDeteTime, terminal.UserCreate, terminal.LastUserUpdate, terminal.IsDeleted);
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
                     "@requestid = {0}, @applicationid = {1}, @agentbranchname = {2}, @streetno = {3}, " +
                     "@town = {4}, @city = {5}, @country = {6}, @postalcode = {7}, @phoneno = {8}, " +
                     "@createddatetime = {9}, @updatedatetime = {10}, @usercreate = {11}, @lastuserupdate  = {12}, @isdeleted = {13} ",
                      agentBranches.RequestID, agentBranches.ApplicationID, agentBranches.AgentBranchName, agentBranches.StreetNo, agentBranches.Town, agentBranches.City, agentBranches.Country, agentBranches.PostalCode,
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
                     "@requestid = {0}, @applicationid = {1}, @agentid = {2}, @merchantid = {3}, " +
                     "@transactiontype = {4}, @conveniencefee  = {5}, @quota = {6}, @createddatetime = {7}, " +
                     "@updatedatetime = {8}, @usercreate  = {9}, @lastuserupdate = {10}, @isdeleted = {11}",
                      bankFees.RequestID, bankFees.ApplicationID, bankFees.AgentID, bankFees.MerchantID, bankFees.TransactionType, bankFees.ConvenienceFee, bankFees.Quota,
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
