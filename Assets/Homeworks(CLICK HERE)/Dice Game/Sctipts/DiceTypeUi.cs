using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceTypeUi : MonoBehaviour
{
    [SerializeField] private DiceType diceType;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    public UnityEvent<int> onCountChanged;

    private int currentCount;

    public DiceType Type => diceType;


    public void UpdateValue(int count)
    {
        currentCount = count;
        inputField.text = count.ToString();
    }

    private void Awake() 
    {
        leftArrow.onClick.AddListener(() => UpdateModelValue(currentCount - 1));
        rightArrow.onClick.AddListener(() => UpdateModelValue(currentCount + 1));
    }

    private void UpdateModelValue(int count)
    {
        onCountChanged?.Invoke(count);
    }
}
