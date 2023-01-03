using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    [field: SerializeField]
    public int startingHealth { get; private set; } = 1;
    [field: SerializeField, HideInInspector]
    public int crrtHealth { get; private set; }

    public void Start()
    {
        crrtHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        crrtHealth -= damage;
        Debug.Log(crrtHealth);
    }
}
