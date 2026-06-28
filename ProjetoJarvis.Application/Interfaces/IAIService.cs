using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> GenerateResponseAsync(string prompt);
        Task<string> GenerateWithMemoryAsync(string userId, string prompt);
    }
}
