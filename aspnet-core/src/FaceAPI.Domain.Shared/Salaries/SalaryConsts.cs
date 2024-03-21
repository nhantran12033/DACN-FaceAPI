namespace FaceAPI.Salaries
{
    public static class SalaryConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Salary." : string.Empty);
        }

    }
}