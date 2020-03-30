using System;

namespace EnterpriseProject.Contracts
{
    public class SimpleRover : Rover<int>
    {
        public SimpleRover(ICoordinateParser<int> parser, string startingLocation) : base(parser, startingLocation)
        {
            MovementAbilities.AddRange
            (
                new Action<char>[]
                {
                    (movement) =>
                    {
                        if (movement == 'L')
                        {
                            var nextBearing = Bearing - 90;
                            if (nextBearing < 0)
                            {
                                nextBearing = 360 + nextBearing;
                            }
                            NextBearing = nextBearing;
                        }
                    },
                    (movement) =>
                    {
                        if (movement == 'R')
                        {
                            NextBearing = (Bearing + 90) % 360;
                        }
                    },
                    (movement) =>
                    {
                        if (movement == 'M')
                        {
                            switch (Bearing)
                            {
                                case 90:
                                    NextY = Y + 1;
                                    NextX = X;
                                    break;
                                case 180:
                                    NextX = X + 1;
                                    NextY = Y;
                                    break;
                                case 270:
                                    NextY = Y - 1;
                                    NextX = X;
                                    break;
                                case 0:
                                    NextX = X - 1;
                                    NextY = Y;
                                    break;
                            }
                        }
                    }
                }
            );
        }
    }
}
