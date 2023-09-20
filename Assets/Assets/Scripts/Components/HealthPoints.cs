using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public event Action HPExpired;

    [SerializeField]
    private int startHP;

    private int currentHP;

    private void OnEnable()
    {
        currentHP = startHP;
    }

    public void DecreaseHP(int value)
    {
        if (currentHP > value)
        {
            currentHP -= value;
        }
        else
        {
            currentHP = 0;
            HPExpired?.Invoke();
        }
    }
}