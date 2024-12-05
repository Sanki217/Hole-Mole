using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text multiplierTxt;
    [SerializeField] private TMP_Text scoreTxt;

    public void DisplayScore(int score, int multiplier)
    {
        multiplierTxt.text = "X" + multiplier.ToString();
        scoreTxt.text = "Player Score: " + score.ToString();
    }
}
