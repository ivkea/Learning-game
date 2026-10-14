using TMPro;
using UnityEngine;

public class ScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreUI;
    [SerializeField] private TMP_Text targetScoreUI;

    [Header("Цвета кондишенов")]
    [SerializeField] private Color winColor = Color.green;
    [SerializeField] private Color drawColor = Color.gray;
    [SerializeField] private Color loseColor = Color.red;
    [SerializeField] private Color noneColor = Color.white;

    public void UpdateValue(int score, int targetScore, GameResult condition = GameResult.None)
    {
        if (scoreUI != null) scoreUI.text = score.ToString();
        if (targetScoreUI != null) targetScoreUI.text = targetScore.ToString();

        ChangeColor(condition);

    }

    private void ChangeColor(GameResult condition)
    {
        Color textColor = condition switch
        {
            GameResult.Win => winColor,
            GameResult.Lose => loseColor,
            GameResult.Draw => drawColor,
            _ => noneColor
        };

        if (scoreUI != null) scoreUI.color = textColor;
        if (targetScoreUI != null) targetScoreUI.color = textColor;
    }
}
