using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceSelectorUi : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    public UnityEvent<int> onCountChanged;

    private int currentCount;

    public void UpdateValue(int count)
    {
        currentCount = Mathf.Max(0, count);
        inputField.text = count.ToString();
    }

    private void Awake() 
    {
        leftArrow.onClick.AddListener(OnLeftClicked);
        rightArrow.onClick.AddListener(OnRightClicked);
    }

    private void OnLeftClicked()
    {
        if (currentCount > 0)
            onCountChanged?.Invoke(currentCount -1);
    }

    private void OnRightClicked()
    {
        onCountChanged?.Invoke(currentCount + 1);
    }
}
