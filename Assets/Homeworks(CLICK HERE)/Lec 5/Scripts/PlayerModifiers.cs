using UnityEngine;

public class PlayerModifiers
{
    private float maxHpModifier = 1;
    private float maxStaminaModifier = 1;
    private float maxHungerModifier = 1;

    private float hungerLoseModifier = 1;
    private float staminaLoseModifier = 1;
    private float damageReductionModifier = 1;


    public float MaxHpModifier
    {
        get => maxHpModifier;
        set => maxHpModifier = value;
    }
    public float MaxStaminaModifier
    {
        get => maxStaminaModifier;
        set => maxStaminaModifier = value;
    }
    public float MaxHungerModifier
    {
        get => maxHungerModifier;
        set => maxHungerModifier = value;
    }

    public float HungerLoseModifier
    {
        get => hungerLoseModifier;
        set => hungerLoseModifier = value;
    }
    public float StaminaLoseModifier
    {
        get => staminaLoseModifier;
        set => staminaLoseModifier = value;
    }
    public float DamageReductionModifier
    {
        get => damageReductionModifier;
        set => damageReductionModifier = value;
    }
}
