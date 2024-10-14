using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }


    public void GetDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Debug.Log("DIE");
        }
    }
}
