using System.Collections;
using UnityEngine;

namespace Assets.Homeworks_CLICK_HERE_.Lec_5.Scripts
{
    public class PlayerParameters : MonoBehaviour
    {
        private float hp;
        private float stamina;
        private float hunger;

        [SerializeField] private PlayerData playerData;
        private PlayerModifiers playerModifiers = new();
        

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
            private set
            {
                hp = Mathf.Clamp(value, 0, MaxHp);
            }
        }

        public float Stamina
        {
            get => stamina;
            private set
            {
                stamina = Mathf.Clamp(value, 0, MaxStamina);
            }
        }

        public float Hunger
        {
            get => hunger;
            private set
            {
                hunger = Mathf.Clamp(value, 0, MaxHunger);
            }
        }

        public bool CanJump => Stamina >= JumpStaminaCost && Hunger >= JumpHungerCost;
        public bool CanSprint => Stamina >= SprintStaminaCost && Hunger >= SprintHungerCost;

        private void Start()
        {
            hp = MaxHp;
            stamina = MaxStamina;
            hunger = MaxHunger;
        }
        public void TakeDamage(float damage)
        {
            Hp -= damage * playerModifiers.DamageReductionModifier;
        }

        public void TakeHeal(float heal)
        {
            Hp += heal;
        }

        public void Eat(/* food */) 
        {
           /* Hunger += food.hungerAmount */
        }
        public void TakeFoodHeal(float heal)
        {
            Hp += heal;
            Hunger -= HealHungerCost;
        }

        public void Sprint()
        {
            if (!CanSprint) return;

            Stamina -= SprintStaminaCost;
            Hunger -= SprintHungerCost;
        }

        public void Jump()
        {
            if (!CanJump) return;
            Stamina -= JumpStaminaCost;
            Hunger -= JumpHungerCost;
        }

        public void RestoreStamina(float amount = 1)
        {
            Stamina += amount;
        }
    }
}