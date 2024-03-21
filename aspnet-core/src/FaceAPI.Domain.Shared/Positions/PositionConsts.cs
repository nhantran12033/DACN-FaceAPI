namespace FaceAPI.Positions
{
    public static class PositionConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Position." : string.Empty);
        }

    }
}