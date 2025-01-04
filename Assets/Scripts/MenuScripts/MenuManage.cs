using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject difficultyCanvas;

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        difficultyCanvas.SetActive(false);
    }

    public void SelectDifficulty()
    {
        mainMenuCanvas.SetActive(false);
        difficultyCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        difficultyCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    // Called when you click on "Easy" button
    public void SetEasyDifficulty()
    {
        DifficultyManager.CurrentDifficulty = DifficultyManager.Difficulty.Easy;
        Debug.Log("Difficulty set to Easy");
    }

    // Called when you click on "Medium" button
    public void SetMediumDifficulty()
    {
        DifficultyManager.CurrentDifficulty = DifficultyManager.Difficulty.Medium;
        Debug.Log("Difficulty set to Medium");
    }

    // Called when you click on "Hard" button
    public void SetHardDifficulty()
    {
        DifficultyManager.CurrentDifficulty = DifficultyManager.Difficulty.Hard;
        Debug.Log("Difficulty set to Hard");
    }

    public void SetNightmareDifficulty()
    {
        DifficultyManager.CurrentDifficulty = DifficultyManager.Difficulty.Nightmare;
        Debug.Log("Difficulty set to Nightmare");
    }

}
