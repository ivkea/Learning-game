
using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;



[RequireComponent(typeof(PlayerInput))]
public class DiceManager : MonoBehaviour
{
    [SerializeField] private InputRebinder rebinder;
    [SerializeField] private InputActionReference rollActionRef;

    [SerializeField] private SerializedDictionary<DiceType, Dice> prefabDict;
    [SerializeField] private SerializedDictionary<DiceType, int> dices = new() { { DiceType.D4, 1 }, { DiceType.D6, 1 } };

    private List<Dice> activeDices;

    public event Action<DiceType, int> OnDiceCountChanged;
    public event Action<int, int, GameResult> OnGameOutcome; // score/target/gameResult

    [SerializeField] private float minForce = 1f;
    [SerializeField] private float maxForce = 2f;
    [SerializeField] private float minTorque = 10f;
    [SerializeField] private float maxTorque = 16f;

    private int targetScore = 0;

    public int TargetScore => targetScore;

    private void Awake()
    {
        activeDices = new List<Dice>();

        var initialDices = new SerializedDictionary<DiceType, int>(dices);
        dices.Clear();

        foreach (var pair in initialDices)
        {
            for (var _ = 0; _ < pair.Value; _++)
                SpawnDice(pair.Key);
        }

        initialDices.Clear();
    }

    private void OnRoll(InputValue value)
    {
        if (value.isPressed)
        {
            GenerateTargetScore();
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

            var count = GetDiceTypeAmount(type) + 1;
            dices[type] = count;
            OnDiceCountChanged?.Invoke(type, count);
        }
    }

    public void RemoveDice(Dice dice)
    {
        if (activeDices.Contains(dice))
        {
            activeDices.Remove(dice);
            Destroy(dice.gameObject);

            var type = dice.Type;
            var count = GetDiceTypeAmount(type) - 1;
            dices[type] = count;
            OnDiceCountChanged?.Invoke(type, count);
        }
    }

    public void RemoveDice(DiceType type)
    {
        var dice = activeDices.Find(d => d.Type == type);
        if (dice != null)
        {
            RemoveDice(dice);
        }
    }

    private bool AllDiceStopped() => activeDices.All(dice => dice.IsStopped);

    public int GetAllValues() => activeDices.Sum(dice => dice.GetValue());

    public int GetDiceTypeAmount(DiceType type) => dices.TryGetValue(type, out var count) ? count : 0;

    public int GetMaxScore() => dices.Sum(pair => (int)pair.Key * pair.Value);

    public int GetMaxScore()
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
        if (activeDices.Count == 0) return;

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
        yield return new WaitForSeconds(0.3f); // для того чтоб кубики успели разлететься
        
        while (!AllDiceStopped())
        {
            yield return new WaitForSeconds(0.1f);
        }
        var totalScore = GetAllValues();
        GameResult result = totalScore > targetScore ? GameResult.Win
                          : totalScore == targetScore ? GameResult.Draw
                          : GameResult.Lose;

        OnGameOutcome?.Invoke(totalScore, targetScore, result);
        Debug.Log($"cумма очков {totalScore}");
    }

    private void GenerateTargetScore()
    {
        targetScore = Mathf.RoundToInt(GetMaxScore() * Random.Range(0.4f, 0.8f));

        OnGameOutcome?.Invoke(0, targetScore, GameResult.None);
    }

}
