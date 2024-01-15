namespace Desafio.Domain;

internal static class DocumentHelper
{
    internal static string GetOnlyDocumentNumber(this string document)
    {
        var onlyNumber = "";
        foreach (var value in document)
        {
            if (char.IsDigit(value))
            {
                onlyNumber += value;
            }
        }
        return onlyNumber.Trim();
    }
}
