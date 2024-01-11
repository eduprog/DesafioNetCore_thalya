using System.Text;

namespace Desafio.Domain;

internal static class GenerateShortId
{
    internal static string GetShortId()
    {
        DateTime date = DateTime.Now;
        Guid guid = Guid.NewGuid();

        string newId = $"{date:yyyyMMddHHmmss}-{guid}";

        string idBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(newId));

        string shortId = new string(idBase64
            .Where(x => char.IsLetterOrDigit(x))
            .ToArray());
        shortId = shortId.Substring(shortId.Length - 10);

        return shortId;
    }
}
