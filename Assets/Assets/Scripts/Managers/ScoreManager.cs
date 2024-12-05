using UnityEngine;

public class ScoreManager : MonoBehaviour, IManager
{
    [SerializeField] private ScoreDisplay PlayerOneScoreDisplay;
    [SerializeField] private ScoreDisplay PlayerTwoScoreDisplay;

    public void DisplayScore(int playerID, int score, int multiplier)
    {
        ScoreDisplay display = playerID == 1 ? PlayerOneScoreDisplay : PlayerTwoScoreDisplay;

        display.DisplayScore(score, multiplier);
    }
}
