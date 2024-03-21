namespace FaceAPI.ScheduleDetails
{
    public static class ScheduleDetailConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ScheduleDetail." : string.Empty);
        }

    }
}