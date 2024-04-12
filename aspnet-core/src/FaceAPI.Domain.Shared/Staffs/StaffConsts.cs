namespace FaceAPI.Staffs
{
    public static class StaffConsts
    {
        private const string DefaultSorting = "{0}Image asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Staff." : string.Empty);
        }

    }
}