using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private float maxStamina;
    [SerializeField] private float maxHunger;

    [SerializeField] private float jumpStaminaCost;
    [SerializeField] private float sprintStaminaCost;

    [SerializeField] private float jumpHungerCost;
    [SerializeField] private float sprintHungerCost;
    [SerializeField] private float healHungerCost;

    public float MaxHp => maxHp;
    public float MaxStamina => maxStamina;
    public float MaxHunger => maxHunger;


    public float SprintStaminaCost => sprintStaminaCost;
    public float JumpStaminaCost => jumpStaminaCost;

    public float SprintHungerCost => sprintHungerCost;
    public float JumpHungerCost => jumpHungerCost;
    public float HealHungerCost => healHungerCost;

}
