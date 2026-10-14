using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [Header("Цвета кондишенов")]
    [SerializeField] private Color winColor = Color.green;
    [SerializeField] private Color drawColor = Color.gray;
    [SerializeField] private Color loseColor = Color.red;
    [SerializeField] private Color noneColor = Color.white;

    public void UpdateValue(int score, int targetScore, GameResult condition = GameResult.None)
    {
        string color = ColorUtility.ToHtmlStringRGB(GetColor(condition)); //гугл спас
        string targetStr = targetScore > 0 ? targetScore.ToString() : "-";

        scoreText.text = $"<color=#{color}>{score}</color> <color=#FFFFFF>/</color> <color=#{color}>{targetStr}</color>";
    }

    private Color GetColor(GameResult condition) => condition switch
    {
        GameResult.Win => winColor,
        GameResult.Lose => loseColor,
        GameResult.Draw => drawColor,
        _ => noneColor
    };
}
