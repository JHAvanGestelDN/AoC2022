namespace Day02
{
    public class RockPaperScissors
    {
        private Shape opponentShape { get; set; }
        private Shape playerShape { get; set; }
        private int score { get; set; }
        public int points { get; set; }
        public RockPaperScissors(string opponent, string player)
        {
            opponentShape = ConvertStringToShape(opponent);
            playerShape = ConvertStringToShape(player);
            score = CalculateScore();
            points = score + (int)playerShape;
        }
        public RockPaperScissors(string opponent, string outcomeNeeded, bool secondAssignment)
        {
            opponentShape = ConvertStringToShape(opponent);
            playerShape = DetermineNecessaryShape(outcomeNeeded);
            score = CalculateScore();
            points = score + (int)playerShape;
        }
        private Shape DetermineNecessaryShape(string outcomeNeeded)
        {
            return outcomeNeeded switch
            {
            "Y" => opponentShape,
            "X" => opponentShape == Shape.Rock ? Shape.Scissors : opponentShape == Shape.Paper ? Shape.Rock : Shape.Paper,
            "Z" => opponentShape == Shape.Rock ? Shape.Paper : opponentShape == Shape.Paper ? Shape.Scissors : Shape.Rock,
            _ => throw new Exception()
            };
        }


        private int CalculateScore()
        {
            if (opponentShape == playerShape)
                return 3;
            return opponentShape switch
            {
            Shape.Rock => playerShape == Shape.Paper ? 6 : 0,
            Shape.Paper => playerShape == Shape.Scissors ? 6 : 0,
            Shape.Scissors => playerShape == Shape.Rock ? 6 : 0,
            _ => throw new Exception()
            };
        }

        private static Shape ConvertStringToShape(string shape)
        {
            switch (shape)
            {
                case "A":
                case "X":
                    return Shape.Rock;
                case "B":
                case "Y":
                    return Shape.Paper;
                case "C":
                case "Z":
                    return Shape.Scissors;
                default:
                    return Shape.Unknown;
            }
        }
    }
    public enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
        Unknown
    }
}
