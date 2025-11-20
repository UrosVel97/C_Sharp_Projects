using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorisJokes.UserInteraction
{
    public interface IUserInteractor : IDisposable
    {
        Task<string> ReadSingleWord(string prompt);
        int ReadInteger(string prompt);

        void ShowMessage(string message);
    }
}
