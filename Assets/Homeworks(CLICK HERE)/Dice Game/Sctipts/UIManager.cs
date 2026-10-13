using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DiceManager diceManager;
    [SerializeField] private DiceTypeUI UI; //затем лист из dicetype ui

    private void Awake()
    {
        //затем заполнение листа dicetype ui
    }

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        diceManager.OnDiceCountChanged += HandleCountChange;

        //затем будет цикл с итерацией по всем dicetypeUI

        var type = UI.Type;
        UI.UpdateValue(diceManager.GetDiceTypeAmount(type));

        UI.onCountChanged.AddListener(delta =>
        {
            if (delta > 0)
                diceManager.SpawnDice(type);
            else if (delta < 0)
                diceManager.RemoveDice(type);
        });
    }

    private void HandleCountChange(DiceType type, int count)
    {
        UI.UpdateValue(count);
    }

    private void OnDestroy()
    {
        diceManager.OnDiceCountChanged -= HandleCountChange;
    }

}
