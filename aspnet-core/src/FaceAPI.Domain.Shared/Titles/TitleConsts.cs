namespace FaceAPI.Titles
{
    public static class TitleConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Title." : string.Empty);
        }

    }
}