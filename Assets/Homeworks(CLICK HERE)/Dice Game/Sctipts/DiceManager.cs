
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using AYellowpaper.SerializedCollections;
using System;
using Random = UnityEngine.Random;



[RequireComponent(typeof(PlayerInput))]
public class DiceManager : MonoBehaviour
{
    [SerializeField] private InputRebinder rebinder;
    [SerializeField] private InputActionReference rollActionRef;

    [SerializeField] private SerializedDictionary<DiceType, Dice> prefabDict;
    [SerializeField] private List<DiceType> initialDices = new() { DiceType.D4, DiceType.D6 };

    private List<Dice> activeDices;

    public event Action<DiceType, int> OnDiceCountChanged;
    public event Action<int, int, GameResult> OnGameOutcome; // score/target/gameResult

    [SerializeField] private float minForce = 1f;
    [SerializeField] private float maxForce = 2f;
    [SerializeField] private float minTorque = 10f;
    [SerializeField] private float maxTorque = 16f;



    private void Awake()
    {
        activeDices = new List<Dice>();

        foreach (var type in initialDices)
        {
            SpawnDice(type);
        }
    }

    private void OnRoll(InputValue value)
    {
        if (value.isPressed)
        {
            ThrowAll();
        }
    }

    [ContextMenu("Test Rebind")]
    public void RollRebind()
    {
        rebinder.Rebind(rollActionRef.action);
    }

    public void SpawnDice(DiceType type)
    {
        if (prefabDict.TryGetValue(type, out var prefab) && prefab != null)
        {
            var dice = Instantiate(prefab, transform.position + Random.insideUnitSphere, Quaternion.identity);
            activeDices.Add(dice);

            OnDiceCountChanged?.Invoke(type, GetDiceTypeAmount(type));
        }
        else
            Debug.LogError($"Префаба нет! Запрошен тип: '{type}' (число: {(int)type}). " +
               $"В словаре сейчас лежат ключи: {string.Join(", ", prefabDict.Keys)}");
    }

    public void RemoveDice(Dice dice)
    {
        if (activeDices.Contains(dice))
        {
            activeDices.Remove(dice);
            Destroy(dice.gameObject);
            OnDiceCountChanged?.Invoke(dice.Type, GetDiceTypeAmount(dice.Type));
        }
    }

    public void RemoveDice(DiceType type)
    {
        foreach (var dice in activeDices)
        {
            if (dice.Type == type)
            {
                activeDices.Remove(dice);
                Destroy(dice.gameObject);
                OnDiceCountChanged?.Invoke(type, GetDiceTypeAmount(type));
                break;
            }
        }
    }

    private bool AllDiceStopped()
    {
        foreach (var dice in activeDices)
        {
            if (!dice.IsStopped)
            {
                return false;
            }
        }
        return true;
    }
    public int GetAllValues()
    {
        int total = 0;
        foreach (var dice in activeDices)
        {
            int value = dice.GetValue();
            total += value;
            Debug.Log($"кубик {dice.Type} выбросил {value}");
        }

        return total;
    }

    public int GetDiceTypeAmount(DiceType type)
    {
        var count = 0;
        foreach (var dice in activeDices)
        {
            if (type == dice.Type)
                count++;
        }
        return count;
    }

    private int GetMaxScore()
    {
        var count = 0;
        foreach (var dice in activeDices)
        {
            count += (int)dice.Type;
        }
        return count;
    }

    public void ThrowAll()
    {
        StopAllCoroutines();

        foreach (var dice in activeDices)
        {
            Vector3 forceDirection = (Vector3.up + Random.insideUnitSphere).normalized;
            Vector3 force = forceDirection * Random.Range(minForce, maxForce);

            Vector3 torque = Random.insideUnitSphere.normalized * Random.Range(minTorque, maxTorque);

            dice.Throw(force, torque);
        }

        StartCoroutine(CalculateCoRoutine());
    }

    private IEnumerator CalculateCoRoutine()
    {
        yield return new WaitUntil(AllDiceStopped);

        var totalScore = GetAllValues();
        var targetScore = (int)(GetMaxScore() * Random.Range(0f, 1f));

        OnGameOutcome?.Invoke(totalScore, targetScore, (totalScore > targetScore) ? GameResult.Win : (totalScore == targetScore) ? GameResult.Draw : GameResult.Lose);
        Debug.Log($"cумма очков {totalScore}");
    }
}

public enum GameResult
{
    Win,
    Draw,
    Lose
}
