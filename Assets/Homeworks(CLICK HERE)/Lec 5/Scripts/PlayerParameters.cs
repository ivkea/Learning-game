using System.Collections;
using UnityEngine;

namespace Assets.Homeworks_CLICK_HERE_.Lec_5.Scripts
{
    public class PlayerParameters : MonoBehaviour
    {
        private float hp;
        private float stamina;
        private float hunger;

        [SerializeField] PlayerData playerData;
        [SerializeField] PlayerModifiers playerModifiers;
        

        public float MaxHp => playerData.MaxHp * playerModifiers.MaxHpModifier;
        public float MaxStamina => playerData.MaxStamina * playerModifiers.MaxStaminaModifier;
        public float MaxHunger => playerData.MaxHunger * playerModifiers.MaxHungerModifier;
        public float HealHungerCost => playerData.HealHungerCost * playerModifiers.HungerLoseModifier;

        public float SprintStaminaCost => playerData.SprintStaminaCost * playerModifiers.StaminaLoseModifier;
        public float JumpStaminaCost => playerData.JumpStaminaCost * playerModifiers.StaminaLoseModifier;
        public float SprintHungerCost => playerData.SprintHungerCost * playerModifiers.HungerLoseModifier;
        public float JumpHungerCost => playerData.JumpHungerCost * playerModifiers.HungerLoseModifier;

        public float Hp
        {
            get => hp;
            set
            {
                hp = Mathf.Clamp(value, 0, MaxHp);
            }
        }

        public float Stamina
        {
            get => stamina;
            set
            {
                stamina = Mathf.Clamp(value, 0, MaxStamina);
            }
        }

        public float Hunger
        {
            get => hunger;
            set
            {
                hunger = Mathf.Clamp(value, 0, MaxHunger);
            }
        }


        public void TakeDamage(float damage)
        {
            Hp -= damage * playerModifiers.DamageReductionModifier;
        }

        public void TakeHeal(float heal)
        {
            Hp += heal;
        }

        public void TakeFoodHeal(float heal)
        {
            Hp += heal;
            Hunger -= SprintHungerCost;
        }

        public void Sprint()
        {
            Stamina -= SprintStaminaCost;
            Hunger -= SprintHungerCost;
        }

        public void Jump()
        {
            Stamina -= JumpStaminaCost;
            Hunger -= JumpHungerCost;
        }
    }
}