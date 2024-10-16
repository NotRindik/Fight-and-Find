using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    private float currentHealth;
    private Characters character;
    private Rigidbody2D Rigidbody2D;


    protected void Awake()
    {
        currentHealth = health;
        character = FindAnyObjectByType<Characters>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void GetDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Debug.Log("DIE");
            
            

            Destroy(gameObject);
        }
    }


    private void Update()
    { 
    
        Vector2 PlayerPos = character.transform.position;

        Rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position,PlayerPos,speed * Time.deltaTime));

    }

    protected void Move() 
    { 
    
    

    
    }


}
