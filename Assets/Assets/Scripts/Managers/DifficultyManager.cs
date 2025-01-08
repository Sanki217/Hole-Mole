using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour, IManager
{
    [SerializeField] private float easyDifficultyTime;
    [SerializeField] private float mediumDifficultyTime;
    [SerializeField] private float hardDifficultyTime;

    [SerializeField] private TMP_Text dificultyName;

    public Difficulty Difficulty { get; private set; }

    public void ChangDifficulty()
    {
        int difficultyIndex = ((int)Difficulty + 1) % 3;

        Difficulty = (Difficulty)difficultyIndex;

        dificultyName.text = Difficulty.ToString();
    }

    public float GetDifficultyInFloat()
    {
        switch(Difficulty)
        {
            case Difficulty.Hard:   return hardDifficultyTime;
            case Difficulty.Medium: return mediumDifficultyTime;
            default:                return easyDifficultyTime;
        }
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
