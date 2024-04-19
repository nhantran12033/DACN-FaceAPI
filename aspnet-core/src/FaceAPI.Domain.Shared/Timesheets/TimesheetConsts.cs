namespace FaceAPI.Timesheets
{
    public static class TimesheetConsts
    {
        private const string DefaultSorting = "{0}Image asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Timesheet." : string.Empty);
        }

    }
}