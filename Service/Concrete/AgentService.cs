using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Concrete
{
    public class AgentService : IAgentService
    {
        public IAgentRepository _agentRepository;
        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<AgentList> GetAgentwithID([FromBody] int id)
        {
            string request = await _agentRepository.GetRequestID(id);

            var response = await _agentRepository.GetAgentwithID(request);

            AgentList agentList = new AgentList();
            agentList.Agent = response.Item1;
            agentList.Bank = response.Item2;
            agentList.Contact = response.Item3;
            agentList.AgentBranches = response.Item4;
            agentList.Terminal = response.Item5;
            agentList.BankFees = response.Item6;
            agentList.Moa = response.Item7;

            return agentList;
        }
        public async Task<AgentRequest> CreateAgent(AgentRequest agentRequest)
        {
            string generatedRequestID = Guid.NewGuid().ToString("N").Substring(0, 12);
            string generatedApplicationID = Guid.NewGuid().ToString("N").Substring(0, 12);
            int agentID = CreateAgentID();
            int authID = CreateAgentID();
            int masterAgentCode = 0;
            int submasteragentCode = 0;

            if (agentRequest.Masteridlist != null)
            {
                submasteragentCode = CreateMasterSubID();
                masterAgentCode = Convert.ToInt32(agentRequest.Masteridlist);
            }
            else
            {
                masterAgentCode = CreateMasterSubID();
                submasteragentCode = 0;
            }
           
            Agent agent = new Agent
            {
                AgentID = agentID,
                ApplicationID = generatedApplicationID,
                BusinessName = agentRequest.BusinessName,
                City = agentRequest.City,
                CompanyTIN = agentRequest.Comptin,
                CorporateName = agentRequest.CorpName,
                Country = agentRequest.Country,
                CreatedDateTime = DateTime.Now,
                CTCNo = Convert.ToInt32(agentRequest.CTCNo),
                DailyDepositLimit = agentRequest.DailyDepLimit,
                FirstName = agentRequest.FirstName,
                IsBusiness = agentRequest.IsBusinessName != null ? bool.Parse(agentRequest.IsBusinessName) : false,
                IsCorporate = agentRequest.IsCorpName != null ? bool.Parse(agentRequest.IsCorpName) : false,
                IsMerchCategory = agentRequest.IsMerchcategory != null ? bool.Parse(agentRequest.IsMerchcategory) : false,
                LastName = agentRequest.LastName,
                LastUserUpdate = agentRequest.User,
                MasterAgentCodeID = masterAgentCode,
                MerchantCategory = agentRequest.Merchcategory,
                MiddleName = agentRequest.MiddleName,
                PhoneNo = agentRequest.PhoneNo,
                PostalCode = Convert.ToInt32(agentRequest.PostalCode),
                RequestID = generatedRequestID,
                StreetNo = agentRequest.StreetNo,
                SubAgentCodeID = submasteragentCode,
                Town = agentRequest.Town,
                UpdateDeteTime = DateTime.Now,
                UserCreate = agentRequest.User,
                Status = "Pending",
                IsDeleted = false
            };

            Bank bank = new Bank
            {
                ApplicationID = generatedApplicationID,
                BankAccountName = agentRequest.Bankaccname,
                City = agentRequest.BCity,
                Country = agentRequest.BCountry,
                CreatedDateTime = DateTime.Now,
                DepositoryBank = agentRequest.DepBank,
                LastUserUpdate = agentRequest.User,
                PostalCode = Convert.ToInt32(agentRequest.BPostalCode),
                RBOContactNo = agentRequest.RBOContactNo,
                RBOEmailAdd = agentRequest.RBOEmailAdd,
                RBOFName = agentRequest.RBOFirstName,
                RBOLName = agentRequest.RBOLastName,
                RBOMName = agentRequest.RBOMiddleName,
                RBOType = agentRequest.RBOType,
                RequestID = generatedRequestID,
                StreetNo = agentRequest.BStreetNo,
                Town = agentRequest.BTown,
                UpdateDeteTime = DateTime.Now,
                UserCreate = agentRequest.User,
                IsDeleted = false
            };

            Contact contact = new Contact
            {
                ApplicationID = generatedApplicationID,
                BillingContactNo = Convert.ToInt32(agentRequest.CBillContactNo),
                BillingFirstName = agentRequest.CBillFName,
                BillingLastName = agentRequest.CBillLName,
                BillingMiddleName = agentRequest.CBillMName,
                ContactNo = agentRequest.CContactno,
                CreatedDateTime = DateTime.Now,
                Department = agentRequest.CDepartment,
                Designation = agentRequest.CDesignation,
                EmailAddress = agentRequest.CEmailAdd,
                FaxNo = agentRequest.CFaxno,
                FirstName = agentRequest.CFirstName,
                LastName = agentRequest.CLastName,
                LastUserUpdate = agentRequest.User,
                MiddleName = agentRequest.CMiddleName,
                RequestID = generatedRequestID,
                UpdateDeteTime = DateTime.Now,
                UserCreate = agentRequest.User,
                IsDeleted = false
            };

            for (int i = 0; i < agentRequest.AgentBranches?.Count(); i++)
            {
                AgentBranches agentBranches = new AgentBranches
                {
                    ApplicationID = generatedApplicationID,
                    RequestID = generatedRequestID,
                    AgentBranchName = agentRequest.AgentBranches[i].ABranchName,
                    StreetNo = agentRequest.AgentBranches[i].AStreetNo,
                    Town = agentRequest.AgentBranches[i].ATown,
                    City = agentRequest.AgentBranches[i].ACity,
                    Country = agentRequest.AgentBranches[i].Acountry,
                    PostalCode = Convert.ToInt32(agentRequest.AgentBranches[i].APostalCode),
                    PhoneNo = agentRequest.AgentBranches[i].APhoneNo,
                    UserCreate = agentRequest.User,
                    CreatedDateTime = DateTime.Now,
                    LastUserUpdate = agentRequest.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var agentBranchesResponse = await _agentRepository.CreateAgentBranches(agentBranches);
            }
            

            for (int i = 0; i < agentRequest.Terminal?.Count() ; i++)
            {
                Terminal terminal = new Terminal
                {
                    ApplicationID = generatedApplicationID,
                    RequestID = generatedRequestID,
                    POSTerminalName = agentRequest.Terminal[i].TerminalName,
                    TypeOfPOSTerminal = agentRequest.Terminal[i].TerminalType,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequest.User,
                    LastUserUpdate = agentRequest.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var terminalResponse = await _agentRepository.CreateTerminal(terminal);
            }

            for (int i = 0; i < agentRequest.Auth?.Count(); i++)
            {
                Moa moa = new Moa
                {
                    ApplicationID = generatedApplicationID,
                    AuthDesignation = agentRequest.Auth[i].AuthDesignation,
                    AuthFirstName = agentRequest.Auth[i].AuthFirstName,
                    AuthID = authID,
                    AuthLastName = agentRequest.Auth[i].AuthLastName,
                    AuthMiddleName = agentRequest.Auth[i].AuthMiddleName,
                    CreatedDateTime = DateTime.Now,
                    LastUserUpdate = agentRequest.User,
                    RequestID = generatedRequestID,
                    UpdateDeteTime = DateTime.Now,
                    UserCreate = agentRequest.User,
                    ValidIDExpdate = agentRequest.Auth[i].ValidIDExpdate,
                    ValidIDNumber = agentRequest.Auth[i].ValidIDNo,
                    ValidIDType = agentRequest.Auth[i].ValidIDType
                };
                var moaResponse = await _agentRepository.CreateMoa(moa);
            }

            for (int i = 0; i < agentRequest.BankFeesList?.Count(); i++)
            {
                BankFees bankFees = new BankFees
                {
                    ApplicationID = generatedApplicationID,
                    RequestID = generatedRequestID,
                    AgentID = agentID,
                    MerchantID = agentID.ToString(),
                    TransactionType = agentRequest.BankFeesList[i].TransName,
                    ConvenienceFee = agentRequest.BankFeesList[i].ConvFee,
                    Quota = null,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequest.User,
                    LastUserUpdate = agentRequest.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var bankResponse = await _agentRepository.CreateBankFees(bankFees);
            }

            var agentResponse = await _agentRepository.CreateAgent(agent, bank, contact);
            return agentRequest;
        }

        public async Task<AgentRequestEdit> UpdateAgent(AgentRequestEdit agentRequestEdit)
        {
            Agent agent = new Agent
            {
                AgentID = Convert.ToInt32(agentRequestEdit.AgentID),
                ApplicationID = agentRequestEdit.ApplicationID,
                BusinessName = agentRequestEdit.BusinessName,
                City = agentRequestEdit.City,
                CompanyTIN = agentRequestEdit.Comptin,
                CorporateName = agentRequestEdit.CorpName,
                Country = agentRequestEdit.Country,
                CTCNo = Convert.ToInt32(agentRequestEdit.CTCNo),
                DailyDepositLimit = agentRequestEdit.DailyDepLimit,
                FirstName = agentRequestEdit.FirstName,
                IsBusiness = agentRequestEdit.IsBusinessName != null ? bool.Parse(agentRequestEdit.IsBusinessName) : false,
                IsCorporate = agentRequestEdit.IsCorpName != null ? bool.Parse(agentRequestEdit.IsCorpName) : false,
                IsMerchCategory = agentRequestEdit.IsMerchcategory != null ? bool.Parse(agentRequestEdit.IsMerchcategory) : false,
                LastName = agentRequestEdit.LastName,
                LastUserUpdate = agentRequestEdit.User,
                MasterAgentCodeID = Convert.ToInt32(agentRequestEdit.Masteridlist),
                MerchantCategory = agentRequestEdit.Merchcategory,
                MiddleName = agentRequestEdit.MiddleName,
                PhoneNo = agentRequestEdit.PhoneNo,
                PostalCode = Convert.ToInt32(agentRequestEdit.PostalCode),
                RequestID = agentRequestEdit.RequestID,
                StreetNo = agentRequestEdit.StreetNo,
                SubAgentCodeID = Convert.ToInt32(agentRequestEdit.SubmasterAgentCode),
                Town = agentRequestEdit.Town,
                UpdateDeteTime = DateTime.Now,
                Status = "Pending",
                IsDeleted = false
            };

            Bank bank = new Bank
            {
                ApplicationID = agentRequestEdit.ApplicationID,
                BankAccountName = agentRequestEdit.Bankaccname,
                City = agentRequestEdit.BCity,
                Country = agentRequestEdit.BCountry,
                DepositoryBank = agentRequestEdit.DepBank,
                LastUserUpdate = agentRequestEdit.User,
                PostalCode = Convert.ToInt32(agentRequestEdit.BPostalCode),
                RBOContactNo = agentRequestEdit.RBOContactNo,
                RBOEmailAdd = agentRequestEdit.RBOEmailAdd,
                RBOFName = agentRequestEdit.RBOFirstName,
                RBOLName = agentRequestEdit.RBOLastName,
                RBOMName = agentRequestEdit.RBOMiddleName,
                RBOType = agentRequestEdit.RBOType,
                RequestID = agentRequestEdit.RequestID,
                StreetNo = agentRequestEdit.BStreetNo,
                Town = agentRequestEdit.BTown,
                UpdateDeteTime = DateTime.Now,
                IsDeleted = false
            };

            Contact contact = new Contact
            {
                ApplicationID = agentRequestEdit.ApplicationID,
                BillingContactNo = Convert.ToInt32(agentRequestEdit.CBillContactNo),
                BillingFirstName = agentRequestEdit.CBillFName,
                BillingLastName = agentRequestEdit.CBillLName,
                BillingMiddleName = agentRequestEdit.CBillMName,
                ContactNo = agentRequestEdit.CContactno,
                Department = agentRequestEdit.CDepartment,
                Designation = agentRequestEdit.CDesignation,
                EmailAddress = agentRequestEdit.CEmailAdd,
                FaxNo = agentRequestEdit.CFaxno,
                FirstName = agentRequestEdit.CFirstName,
                LastName = agentRequestEdit.CLastName,
                LastUserUpdate = agentRequestEdit.User,
                MiddleName = agentRequestEdit.CMiddleName,
                RequestID = agentRequestEdit.RequestID,
                UpdateDeteTime = DateTime.Now,
                IsDeleted = false
            };

            for (int i = 0; i < agentRequestEdit.AgentBranches?.Count(); i++)
            {
                if(i == 0)
                {
                    await _agentRepository.DeleteAgentBranches(agentRequestEdit.RequestID);
                }
                AgentBranches agentBranches = new AgentBranches
                {
                    ApplicationID = agentRequestEdit.ApplicationID,
                    RequestID = agentRequestEdit.RequestID,
                    AgentBranchName = agentRequestEdit.AgentBranches[i].ABranchName,
                    StreetNo = agentRequestEdit.AgentBranches[i].AStreetNo,
                    Town = agentRequestEdit.AgentBranches[i].ATown,
                    City = agentRequestEdit.AgentBranches[i].ACity,
                    Country = agentRequestEdit.AgentBranches[i].Acountry,
                    PostalCode = Convert.ToInt32(agentRequestEdit.AgentBranches[i].APostalCode),
                    PhoneNo = agentRequestEdit.AgentBranches[i].APhoneNo,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequestEdit.User,
                    LastUserUpdate = agentRequestEdit.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var agentBranchesResponse = await _agentRepository.UpdateAgentBranches(agentBranches);
            }


            for (int i = 0; i < agentRequestEdit.Terminal?.Count(); i++)
            {
                if (i == 0)
                {
                    await _agentRepository.DeleteTerminal(agentRequestEdit.RequestID);
                }
                Terminal terminal = new Terminal
                {
                    ApplicationID = agentRequestEdit.ApplicationID,
                    RequestID = agentRequestEdit.RequestID,
                    POSTerminalName = agentRequestEdit.Terminal[i].TerminalName,
                    TypeOfPOSTerminal = agentRequestEdit.Terminal[i].TerminalType,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequestEdit.User,
                    LastUserUpdate = agentRequestEdit.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var terminalResponse = await _agentRepository.UpdateTerminal(terminal);
            }

            for (int i = 0; i < agentRequestEdit.Auth?.Count(); i++)
            {
                if (i == 0)
                {
                    await _agentRepository.DeleteMoa(agentRequestEdit.RequestID);
                }
                Moa moa = new Moa
                {
                    ApplicationID = agentRequestEdit.ApplicationID,
                    AuthDesignation = agentRequestEdit.Auth[i].AuthDesignation,
                    AuthFirstName = agentRequestEdit.Auth[i].AuthFirstName,
                    AuthID = CreateAgentID(),
                    AuthLastName = agentRequestEdit.Auth[i].AuthLastName,
                    AuthMiddleName = agentRequestEdit.Auth[i].AuthMiddleName,
                    LastUserUpdate = agentRequestEdit.User,
                    RequestID = agentRequestEdit.RequestID,
                    UpdateDeteTime = DateTime.Now,
                    ValidIDExpdate = agentRequestEdit.Auth[i].ValidIDExpdate,
                    ValidIDNumber = agentRequestEdit.Auth[i].ValidIDNo,
                    ValidIDType = agentRequestEdit.Auth[i].ValidIDType,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequestEdit.User
                };
                var moaResponse = await _agentRepository.UpdateMoa(moa);
            }

            for (int i = 0; i < agentRequestEdit.BankFeesList?.Count(); i++)
            {
                if (i == 0)
                {
                    await _agentRepository.DeleteBankFees(agentRequestEdit.RequestID);
                }
                BankFees bankFees = new BankFees
                {
                    ApplicationID = agentRequestEdit.ApplicationID,
                    RequestID = agentRequestEdit.RequestID,
                    AgentID = Convert.ToInt32(agentRequestEdit.AgentID),
                    MerchantID = agentRequestEdit.MerchantID,
                    TransactionType = agentRequestEdit.BankFeesList[i].TransName,
                    ConvenienceFee = agentRequestEdit.BankFeesList[i].ConvFee,
                    Quota = null,
                    CreatedDateTime = DateTime.Now,
                    UserCreate = agentRequestEdit.User,
                    LastUserUpdate = agentRequestEdit.User,
                    UpdateDeteTime = DateTime.Now,
                    IsDeleted = false
                };
                var bankResponse = await _agentRepository.UpdateBankFees(bankFees);
            }

            var agentResponse = await _agentRepository.UpdateAgent(agent, bank, contact);
            return agentRequestEdit;

        }

        public static int CreateAgentID()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);

            return myRandomNo;
        }

        public static int CreateMasterSubID()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);

            return myRandomNo;
        }
    }
}
