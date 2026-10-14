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

    public void UpdateValue(int count)
    {
        inputField.text = count.ToString();
    }

    private void Awake() 
    {
        leftArrow.onClick.AddListener(() => onCountChanged.Invoke(-1));
        rightArrow.onClick.AddListener(() => onCountChanged.Invoke(1));
    }
}
