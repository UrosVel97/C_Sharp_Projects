using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollGameToBeTested.UserCommunication
{
    public class ConsoleUserCommunication : IUserCommunication
    {
        public int ReadInteger(string prompt)
        {
            int result;
            do
            {
                Console.WriteLine(prompt);

            }  
            while(!int.TryParse(Console.ReadLine(), out result));
            return result;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
