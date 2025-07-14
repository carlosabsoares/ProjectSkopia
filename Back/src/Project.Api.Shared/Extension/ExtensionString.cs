namespace Project.Api.Shared.Extension
{
    public static class ExtensionString
    {
        public static bool IsGuid(this string text)
        {
            bool isGuid = Guid.TryParse(text, out _);

            return isGuid;
        }
    }
}