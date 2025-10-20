namespace TicketsDataAggregator.Extensions;

public static class WebAddressExtensioins
{
    public static string ExtractDomain(this string webAddress)
    {
        var lastDotIndex = webAddress.LastIndexOf('.');

        return webAddress.Substring(lastDotIndex);
    }
}
