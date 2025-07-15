namespace Project.Api.Shared.Extension
{
    public static class ExtensionString
    {
        public static bool IsGuid(this string text)
        {
            bool isGuid = false;

            if (text == "00000000-0000-0000-0000-000000000000")
                return false;

            isGuid = Guid.TryParse(text, out _);

            return isGuid;
        }
    }
}