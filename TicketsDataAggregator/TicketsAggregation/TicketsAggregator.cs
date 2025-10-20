using System.Globalization;
using System.Text;
using TicketsDataAggregator.Extensions;
using TicketsDataAggregator.FileAccess;
using static TicketsDataAggregator.FileAccess.DocumentsFromPdfReader;

namespace TicketsDataAggregator.TicketsAggregation;

public class TicketsAggregator
{
    private readonly string _ticketsFolder;
    private readonly Dictionary<string, CultureInfo> _domainCultureMapping = new()
    {
        [".com"]= new CultureInfo("en-US"),
        [".fr"] = new CultureInfo("fr-FR"),
        [".jp"] = new CultureInfo("ja-JP")
    };

    private readonly IFileWriter _fileWriter;
    private readonly IDocumentsReader _documentsReader;

    public TicketsAggregator(string tiicketsFolder, IFileWriter fileWriter, IDocumentsReader documentsReader)
    {
        this._ticketsFolder = tiicketsFolder;
        _fileWriter = fileWriter;
        _documentsReader = documentsReader;
    }

    public void Run()
    {
        var stringBuilder = new StringBuilder();
        var ticketDocuments = _documentsReader.Read(_ticketsFolder);

        foreach (var document in ticketDocuments)
        {

            var lines = ProcessDocument(document);
            stringBuilder.AppendLine(string.Join(Environment.NewLine, lines));

        }

      _fileWriter.Write(stringBuilder.ToString(), _ticketsFolder, "aggregatedTickets.txt" );
    }



    private IEnumerable<string> ProcessDocument(string document)
    {

        var split = document.Split(new[] { "Title:", "Date:", "Time:", "Visit us:" }, StringSplitOptions.None);

        var domain = split.Last().ExtractDomain();
        var ticketCulture = _domainCultureMapping[domain];

        for (int i = 1; i < split.Length - 1; i += 3)
        {

            yield return BuildTicketData(split, i, ticketCulture);
        }
    }

    private static string BuildTicketData(string[] split, int i, CultureInfo ticketCulture)
    {
        string title = split[i];
        string dateAsString = split[i + 1];
        string timeAsString = split[i + 2];

        var date = DateOnly.Parse(dateAsString, ticketCulture);
        var time = TimeOnly.Parse(timeAsString, ticketCulture);

        var dateAsStringInvariant = date.ToString(CultureInfo.InvariantCulture);
        var timeAsStringInvariant = time.ToString(CultureInfo.InvariantCulture);

        var ticketData = $"{title,-40}| {dateAsStringInvariant}| {timeAsStringInvariant}";

        return ticketData;
    }


}
