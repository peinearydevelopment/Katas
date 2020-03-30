namespace KISSProject
{
    public class Rover
    {
        private int CurrentXCoordinate { get; set; }
        private int CurrentYCoordinate { get; set; }
        private char CurrentForwardDirection { get; set; }
        private NavigationalArea NavigationalArea { get; }

        public Rover(NavigationalArea navigationalArea, string startingPosition)
        {
            NavigationalArea = navigationalArea;
            (CurrentXCoordinate, CurrentYCoordinate, CurrentForwardDirection) = startingPosition.ParseCoordinatesAndForwardDirection();
        }

        public string Navigate(string instructions)
        {
            foreach (var instruction in instructions)
            {
                TryMove(instruction);
            }

            return $"{CurrentXCoordinate} {CurrentYCoordinate} {CurrentForwardDirection}";
        }

        private bool TryMove(char move)
        {
            if (!CanMoveTo(move))
            {
                return false;
            }

            if (move == 'L')
            {
                switch (CurrentForwardDirection)
                {
                    case 'N':
                        CurrentForwardDirection = 'W';
                        break;
                    case 'W':
                        CurrentForwardDirection = 'S';
                        break;
                    case 'S':
                        CurrentForwardDirection = 'E';
                        break;
                    case 'E':
                        CurrentForwardDirection = 'N';
                        break;
                }
            }

            if (move == 'R')
            {
                switch (CurrentForwardDirection)
                {
                    case 'N':
                        CurrentForwardDirection = 'E';
                        break;
                    case 'E':
                        CurrentForwardDirection = 'S';
                        break;
                    case 'S':
                        CurrentForwardDirection = 'W';
                        break;
                    case 'W':
                        CurrentForwardDirection = 'N';
                        break;
                }
            }

            if (move == 'M')
            {
                switch (CurrentForwardDirection)
                {
                    case 'N':
                        CurrentYCoordinate++;
                        break;
                    case 'E':
                        CurrentXCoordinate++;
                        break;
                    case 'S':
                        CurrentYCoordinate--;
                        break;
                    case 'W':
                        CurrentXCoordinate--;
                        break;
                }
            }

            return true;
        }

        private bool CanMoveTo(char move)
        {
            if (move == 'L' || move == 'R')
            {
                return true;
            }

            var newXCoordinate = CurrentXCoordinate;
            var newYCoordinate = CurrentYCoordinate;
            if (move == 'M')
            {
                switch (CurrentForwardDirection)
                {
                    case 'N':
                        newYCoordinate++;
                        break;
                    case 'E':
                        newXCoordinate++;
                        break;
                    case 'S':
                        newYCoordinate--;
                        break;
                    case 'W':
                        newXCoordinate--;
                        break;
                }
            }

            return NavigationalArea.PositionIsInArea(newXCoordinate, newYCoordinate);
        }
    }
}
