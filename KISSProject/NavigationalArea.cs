namespace KISSProject
{
    public class NavigationalArea
    {
        private int UpperExtremity { get; }
        private int RightExtremity { get; }
        private int BottomExtremity { get; }
        private int LeftExtremity { get; }

        public NavigationalArea(string upperRightExtremity, string lowerLeftExtremity = "0 0")
        {
            (UpperExtremity, RightExtremity) = upperRightExtremity.ParseCoordinates();
            (BottomExtremity, LeftExtremity) = lowerLeftExtremity.ParseCoordinates();
        }

        public bool PositionIsInArea(int x, int y)
        {
            return x >= LeftExtremity && x <= RightExtremity
                && y >= BottomExtremity && y <= UpperExtremity;
        }
    }
}
