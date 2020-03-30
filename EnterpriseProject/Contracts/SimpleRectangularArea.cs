namespace EnterpriseProject.Contracts
{
    public class SimpleRectangularArea : RectangularArea<int>
    {
        public SimpleRectangularArea(SimpleCoordinateParser parser, string upperRightExtremity, string lowerLeftExtremity = "0 0")
        {
            var upperRightLocation = parser.ParseCoordinates(upperRightExtremity);
            var lowerLeftLocation = parser.ParseCoordinates(lowerLeftExtremity);
            CreateBoundaries(upperRightLocation, lowerLeftLocation);
        }
    }
}
