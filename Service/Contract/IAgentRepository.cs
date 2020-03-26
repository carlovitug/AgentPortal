using ABMS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Service.Contract
{
    public interface IAgentRepository
    {
        Task<ActionResult<IEnumerable<Agent>>> GetAgents();
        Task<ActionResult<IEnumerable<Agent>>> GetPendingAgents();
        Task<ChangeStatus> ChangeStatus(ChangeStatus status);
        Task<Tuple<List<Agent>, List<Bank>, List<Contact>, List<AgentBranches>, List<Terminal>, List<BankFees>, List<Moa>>> GetAgentwithID(string requestID);
        Task<ActionResult<IEnumerable<MasterAgentID>>> GetMasterAgents();
        Task<ActionResult<IEnumerable<Agent>>> GetSubAgents([FromBody] int agentRequestID);
        Task<ActionResult<IEnumerable<Agent>>> GetMasterAgentID([FromBody] int agentRequestID);
        Task<Tuple<Agent, Bank, Contact>> CreateAgent(Agent agent, Bank bank, Contact contact);
        Task<Moa> CreateMoa(Moa moa);
        Task<Terminal> CreateTerminal(Terminal terminal);
        Task<BankFees> CreateBankFees(BankFees bankFees);
        Task<AgentBranches> CreateAgentBranches(AgentBranches agentBranches);
        Task<Tuple<Agent, Bank, Contact>> UpdateAgent(Agent agent, Bank bank, Contact contact);
        Task<Moa> UpdateMoa(Moa moa);
        Task<Terminal> UpdateTerminal(Terminal terminal);
        Task<BankFees> UpdateBankFees(BankFees bankFees);
        Task<AgentBranches> UpdateAgentBranches(AgentBranches agentBranches);
        Task<int> DeleteAgent(int agentRequestID);
        Task<bool> DeleteAgentBranches(string agentRequestID);
        Task<bool> DeleteBankFees(string agentRequestID);
        Task<bool> DeleteMoa(string agentRequestID);
        Task<bool> DeleteTerminal(string agentRequestID);
        Task<bool> CheckExistingAgentID(int agentID);
        Task<string> GetRequestID(int id);
    }
}
