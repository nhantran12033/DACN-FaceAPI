namespace FaceAPI.Schedules
{
    public static class ScheduleConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Schedule." : string.Empty);
        }

    }
}