using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DiceManager diceManager;
    [SerializeField] private SerializedDictionary<DiceType, DiceSelectorUi> diceSelectorUIs;
    [SerializeField] private ScoreDisplayUI scoreDisplayUI;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        if (diceManager == null)
        {
            Debug.LogError("DiceManager не назначен в инспекторе!", this);
            return;
        }

        diceManager.OnDiceCountChanged += HandleCountChange;
        diceManager.OnGameOutcome += HandleGameOutcome;

        foreach (var pair in diceSelectorUIs)
        {
            var type = pair.Key;
            var diceSelectorUI = pair.Value;

            diceSelectorUI.UpdateValue(diceManager.GetDiceTypeAmount(type));

            diceSelectorUI.onCountChanged.AddListener(count =>
            {
                int currentCount = diceManager.GetDiceTypeAmount(type);
                int delta = count - currentCount;

                if (delta > 0)
                    for (int i = 0; i < delta; i++)
                        diceManager.SpawnDice(type);

                else if (delta < 0)
                    for (int i = 0; i < - delta; i++)
                        diceManager.RemoveDice(type);
            });
        }

        scoreDisplayUI.UpdateValue(0, 0);
    }

    private void HandleCountChange(DiceType type, int count)
    {
        if (diceSelectorUIs.TryGetValue(type, out var targetUI))
        {
            targetUI.UpdateValue(count);
        }
    }

    private void HandleGameOutcome(int score, int targetScore, GameResult condition)
    {
        scoreDisplayUI.UpdateValue(score, targetScore, condition);
    }

    private void OnDestroy()
    {
        diceManager.OnDiceCountChanged -= HandleCountChange;
        diceManager.OnGameOutcome -= HandleGameOutcome;
    }

}
