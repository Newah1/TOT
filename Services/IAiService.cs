using System.Threading.Tasks;
using TheOregonTrailAI.Models;

namespace TheOregonTrailAI.Services;

public interface IAiService
{
    Task<AIResponse> GetResponse(AIRequest aiRequest);
    Task<AIResponse> GetResponseSimple(string message);
}

