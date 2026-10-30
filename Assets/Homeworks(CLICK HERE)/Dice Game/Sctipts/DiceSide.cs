using System;
using UnityEngine;

[Serializable]
public class DiceSide
{
    [SerializeField] private Vector3 localDirection;
    [SerializeField] private int value;

    public Vector3 Direction => localDirection;
    public int Value => value;

    public DiceSide(Vector3 LocalDirection, int sideValue)
    {
        localDirection = LocalDirection.normalized;
        value = sideValue;
    }
}
