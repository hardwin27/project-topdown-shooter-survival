using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthStats
{
    public float CurrentHealth { set; get; }
    public float MaxHealth { set; get; }

    public void AddHealth(float addedValue);
}
