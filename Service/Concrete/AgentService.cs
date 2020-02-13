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
            int generatedRequestID = CreateAgentID();
            int generatedApplicationID = CreateAgentID();
            int agentID = CreateAgentID();

            agentRequest.agent.AgentID = agentID;
            agentRequest.agent.ApplicationID = generatedApplicationID;
            agentRequest.bank.ApplicationID = generatedApplicationID;
            agentRequest.contact.ApplicationID = generatedApplicationID;
            agentRequest.moa.ApplicationID = generatedApplicationID;
            agentRequest.agent.RequestID = generatedRequestID;
            agentRequest.bank.RequestID = generatedRequestID;
            agentRequest.contact.RequestID = generatedRequestID;
            agentRequest.moa.RequestID = generatedRequestID;

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

            var agentResponse = await _agentRepository.CreateAgent(agentRequest);
            return agentResponse;
        }

        public static int CreateAgentID()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);

            return myRandomNo;
        }
    }
}
