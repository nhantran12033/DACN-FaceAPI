namespace FaceAPI.ScheduleFormats
{
    public static class ScheduleFormatConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ScheduleFormat." : string.Empty);
        }

    }
}