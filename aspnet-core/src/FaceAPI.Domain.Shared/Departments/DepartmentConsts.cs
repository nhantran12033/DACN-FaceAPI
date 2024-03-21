namespace FaceAPI.Departments
{
    public static class DepartmentConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Department." : string.Empty);
        }

    }
}