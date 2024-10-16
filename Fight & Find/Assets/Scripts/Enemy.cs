using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    [SerializeField] Vector2 direction;
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


    protected void Update()
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0, 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void Move() 
    {
        Vector2 PlayerPos = character.transform.position;

         direction = PlayerPos - (Vector2)transform.position;

        Rigidbody2D.MovePosition((Vector2)transform.position + new Vector2(Mathf.Clamp(direction.x,-1,1), Mathf.Clamp(direction.y,-1,1)) * speed * Time.deltaTime);
    }


}
