using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected string currentState;
    protected float lockTill;
    [SerializeField] protected GameObject cursor;
    [SerializeField] protected float cursorRadius;
    [SerializeField] protected float cursorCorrector;
    [SerializeField] protected float health;
    [SerializeField] protected float invincibilityTime;
    protected float invincibilityCurrent;
    protected float currentHealth;
    protected float angle;
    public Slider healthSlider;

    protected virtual void Start()
    {
        currentHealth = health;
        healthSlider.maxValue = health; 
        healthSlider.value = health; 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    protected virtual void Update()
    {
        var state = GetState();
        if (currentState != state)
        {
            currentState = state;
            animator.CrossFade(state, 0.2f, 0);
        }
        RotateCursorAroundPlayer();
        FlipCharacter();

        if (invincibilityCurrent >= 0) {
            invincibilityCurrent -= Time.deltaTime;
        }
    }

    protected virtual void FlipCharacter()
    {
        spriteRenderer.flipX = cursor.transform.localPosition.x > 0 ? false : true;
    }

    protected virtual void RotateCursorAroundPlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;
        cursor.transform.position = transform.position + direction * cursorRadius;

        angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - cursorCorrector;
        cursor.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected virtual string GetState()
    {
        if (Time.time < lockTill)
            return currentState;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) != 0)
        {
            return "move";
        }

        return "idle";

        string LockState(string anim, float t)
        {
            lockTill = Time.time + t;
            return anim;
        }
    }

    protected virtual void CharGetDamage(float damage)
    {

        currentHealth -= damage;
        healthSlider.value = currentHealth;
        Debug.Log(currentHealth);
        StartCoroutine(Blinking());
        if (currentHealth <= 0)
        {

            Debug.Log("Man is Dead");

        }


    }

    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (invincibilityCurrent <= 0)
        {

            if (coll.gameObject.TryGetComponent(out Enemy enemy))
            {
                invincibilityCurrent = invincibilityTime;

                CharGetDamage(enemy.damage);
            }
        }

    }

    IEnumerator Blinking()
    {
        bool flag = true;
        while (true)
        {
            
            if (invincibilityCurrent > 0)
            {
                yield return new WaitForSeconds(invincibilityCurrent / 10);
                if (!flag)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,0.2f);
                }
                else
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                }
                 flag = !flag;

            }
            else
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                break;
            }
        }



    }
}
