namespace OddsFetchingEntities
{
    public static class SessionToken
    {
        private static string instance;

        public static string Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance=value;
            }
        }
    }
}
