namespace EnterpriseProject.Contracts
{
    public static class BearingExtensions
    {
        public static char TranslateBearing(this decimal bearing)
        {
            return bearing == 0 ? 'W'
                    : bearing == 90 ? 'N'
                    : bearing == 180 ? 'E'
                    : 'S'; // must be 270
        }

        public static decimal TranslateBearing(this char bearing)
        {
            return bearing == 'W' ? 0
                    : bearing == 'N' ? 90
                    : bearing == 'E' ? 180
                    : 270; // must be 'S'
        }
    }
}
