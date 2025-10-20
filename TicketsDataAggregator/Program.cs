using TicketsDataAggregator.FileAccess;
using TicketsDataAggregator.TicketsAggregation;

namespace TicketsDataAggregator
{
    internal class Program
    {

        const string TicketsFolder = @".\Tickets";

        static void Main(string[] args)
        {
            try
            {
                var ticketsAggregator = new TicketsAggregator(
                    TicketsFolder, 
                    new FileWriter(),
                    new DocumentsFromPdfReader());

                ticketsAggregator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred. " +
                    "Exception message: " + ex.Message);
            }

            Console.WriteLine("Press any key to close.");
        }
    }
}
