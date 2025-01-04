public static class DifficultyManager
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Nightmare 
    }

    public static Difficulty CurrentDifficulty = Difficulty.Easy;

    public static int GetKeysNeeded()
    {
        switch (CurrentDifficulty)
        {
            case Difficulty.Easy:
                return 3;
            case Difficulty.Medium:
                return 4;
            case Difficulty.Hard:
                return 5;
            case Difficulty.Nightmare: 
                return 6;  
        }
        
        return 3;
    }
}
