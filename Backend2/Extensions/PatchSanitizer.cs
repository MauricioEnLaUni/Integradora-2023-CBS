namespace Fictichos.Constructora.Utilities
{
    public static class ExtendJSONPatch
    {
        public static void Sanitize<T>(
            this Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<T> document,
            Dictionary<string, Action<T, dynamic>> dict)
        where T : class
        {
            for (int i = document.Operations.Count - 1; i >= 0; i--)
            {
                string pathPropertyName =
                    document.Operations[i].path
                    .Split("/", StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault("");
                if (!dict.ContainsKey(pathPropertyName))
                {
                    document.Operations.RemoveAt(i);
                }
            }
        }
    }
}