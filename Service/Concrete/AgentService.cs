using ABMS_Backend.Models;
using ABMS_Backend.Service.Contract;
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
        public async Task<AgentRequest> CreateAgent(AgentRequest agentRequest)
        {
            string generatedRequestID = Guid.NewGuid().ToString("N").Substring(0, 12);
            string generatedApplicationID = Guid.NewGuid().ToString("N").Substring(0, 12);
            int agentID = CreateAgentID();
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
            //var isAgentExist = await _agentRepository.CheckExistingAgentID(agentID);
            //agentRequest.agent.AgentID = agentID;
            //agentRequest.agent.ApplicationID = generatedApplicationID;
            //agentRequest.bank.ApplicationID = generatedApplicationID;
            //agentRequest.contact.ApplicationID = generatedApplicationID;
            //agentRequest.moa.ApplicationID = generatedApplicationID;
            //agentRequest.agent.RequestID = generatedRequestID;
            //agentRequest.bank.RequestID = generatedRequestID;
            //agentRequest.contact.RequestID = generatedRequestID;
            //agentRequest.moa.RequestID = generatedRequestID;
            //agentRequest.agent.IsDeleted = false;
            //agentRequest.bank.IsDeleted = false;
            //agentRequest.contact.IsDeleted = false;
            //agentRequest.moa.IsDeleted = false;

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
                DailyDepositLimit = 30000,
                FirstName = agentRequest.FirstName,
                IsBusiness = false,
                IsCorporate = false,
                IsMerchCategory = false,
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

            AgentBranches agentBranches = new AgentBranches
            { 
                ApplicationID = generatedApplicationID,
                RequestID = generatedRequestID,
                NoAgentOutletsOrBranches = agentRequest.NoofAgent,
                UserCreate = agentRequest.User,
                CreatedDateTime = DateTime.Now,
                LastUserUpdate = agentRequest.User,
                UpdateDeteTime = DateTime.Now,
                IsDeleted = false
            };

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
                    AuthID = CreateAgentID(),
                    AuthLastName = agentRequest.Auth[i].AuthLastName,
                    AuthMiddleName = agentRequest.Auth[i].AuthMiddleName,
                    CreatedDateTime = DateTime.Now,
                    LastUserUpdate = agentRequest.User,
                    RequestID = generatedRequestID,
                    UpdateDeteTime = DateTime.Now,
                    UserCreate = agentRequest.User,
                    ValidIDExpdate = agentRequest.Auth[i].ValidIDExpdate,
                    ValidIDNumber = agentRequest.Auth[i].ValidIDNumber,
                    ValidIDType = agentRequest.Auth[i].ValidIDType
                };
                var moaResponse = await _agentRepository.CreateMoa(moa);
            }            

            var agentResponse = await _agentRepository.CreateAgent(agent, bank, contact, agentBranches);
            return agentRequest;
        }

        public async Task<AgentRequest> UpdateAgent(AgentRequest agentRequest)
        {
            //agentRequest.agent.UpdateDeteTime = DateTime.Now;
            //agentRequest.bank.UpdateDeteTime = DateTime.Now;
            //agentRequest.moa.UpdateDeteTime = DateTime.Now;
            //agentRequest.contact.UpdateDeteTime = DateTime.Now;

            //    Agent agent = new Agent
            //    {
            //        AgentID = agentID,
            //        ApplicationID = generatedApplicationID,
            //        BusinessName = agentRequest.agent.BusinessName,
            //        City = agentRequest.agent.City,
            //        CompanyTIN = agentRequest.agent.CompanyTIN,
            //        CorporateName = agentRequest.agent.CorporateName,
            //        Country = agentRequest.agent.Country,
            //        CreatedDateTime = DateTime.Now,
            //        CTCNo = agentRequest.agent.CTCNo,
            //        DailyDepositLimit = agentRequest.agent.DailyDepositLimit,
            //        FirstName = agentRequest.agent.FirstName,
            //        IsBusiness = agentRequest.agent.IsBusiness,
            //        IsCorporate = agentRequest.agent.IsCorporate,
            //        IsMerchCategory = agentRequest.agent.IsMerchCategory,
            //        LastName = agentRequest.agent.LastName,
            //        LastUserUpdate = agentRequest.agent.LastUserUpdate,
            //        MasterAgentCodeID = 1234,
            //        MerchantCategory = agentRequest.agent.MerchantCategory,
            //        MiddleName = agentRequest.agent.MiddleName,
            //        PhoneNo = agentRequest.agent.PhoneNo,
            //        PostalCode = agentRequest.agent.PostalCode,
            //        RequestID = generatedRequestID,
            //        StreetNo = agentRequest.agent.StreetNo,
            //        SubAgentCodeID = 1234,
            //        Town = agentRequest.agent.Town,
            //        UpdateDeteTime = DateTime.Now,
            //        UserCrete = agentRequest.agent.UserCrete
            //};

            //    Bank bank = new Bank
            //    {
            //        ApplicationID = generatedApplicationID,
            //        BankAccountName = agentRequest.bank.BankAccountName,
            //        City = agentRequest.bank.City,
            //        Country = agentRequest.bank.Country,
            //        CreatedDateTime = DateTime.Now,
            //        DepositoryBank = agentRequest.bank.DepositoryBank,
            //        LastUserUpdate = agentRequest.bank.LastUserUpdate,
            //        PostalCode = agentRequest.bank.PostalCode,
            //        RBOContactNo = agentRequest.bank.RBOContactNo,
            //        RBOEmailAdd = agentRequest.bank.RBOEmailAdd,
            //        RBOFName = agentRequest.bank.RBOFName,
            //        RBOLName = agentRequest.bank.RBOLName,
            //        RBOMName = agentRequest.bank.RBOMName,
            //        RBOType = agentRequest.bank.RBOType,
            //        RequestID = generatedRequestID,
            //        StreetNo = agentRequest.bank.StreetNo,
            //        Town = agentRequest.bank.Town,
            //        UpdateDeteTime = DateTime.Now,
            //        UserCreate = agentRequest.bank.UserCreate
            //    };

            //    Contact contact = new Contact
            //    {
            //        ApplicationID = generatedApplicationID,
            //        BillingContactNo = agentRequest.contact.BillingContactNo,
            //        BillingFirstName = agentRequest.contact.BillingFirstName,
            //        BillingLastName = agentRequest.contact.BillingLastName,
            //        BillingMiddleName = agentRequest.contact.BillingMiddleName,
            //        ContactNo = agentRequest.contact.ContactNo,
            //        CreatedDateTime = DateTime.Now,
            //        Department = agentRequest.contact.Department,
            //        Designation = agentRequest.contact.Designation,
            //        EmailAddress = agentRequest.contact.EmailAddress,
            //        FaxNo = agentRequest.contact.FaxNo,
            //        FirstName = agentRequest.agent.FirstName,
            //        LastName = agentRequest.agent.LastName,
            //        LastUserUpdate = agentRequest.agent.LastUserUpdate,
            //        MiddleName = agentRequest.agent.MiddleName,
            //        RequestID = generatedRequestID,
            //        UpdateDeteTime = DateTime.Now,
            //        UserCreate = agentRequest.contact.UserCreate
            //    };

            //    Moa moa = new Moa
            //    {
            //        ApplicationID = generatedApplicationID,
            //        AuthDesignation = agentRequest.moa.AuthDesignation,
            //        AuthFirstName = agentRequest.moa.AuthFirstName,
            //        AuthID = CreateAgentID(),
            //        AuthLastName = agentRequest.moa.AuthLastName,
            //        AuthMiddleName = agentRequest.moa.AuthMiddleName,
            //        CreateDateTime = DateTime.Now,
            //        LastUserUpdate = agentRequest.moa.LastUserUpdate,
            //        RequestID = generatedRequestID,
            //        UpdateDeteTime = DateTime.Now,
            //        UserCreate = agentRequest.moa.UserCreate,
            //        ValidIDExpdate = agentRequest.moa.ValidIDExpdate,
            //        ValidIDNumber = agentRequest.moa.ValidIDNumber,
            //        ValidIDType = agentRequest.moa.ValidIDType
            //    };

            var agentResponse = await _agentRepository.UpdateAgent(agentRequest);
            return agentResponse;
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
