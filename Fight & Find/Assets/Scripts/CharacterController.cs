using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        rb.MovePosition( rb.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))  * speed * Time.fixedDeltaTime / (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == Mathf.Abs(Input.GetAxisRaw("Vertical")) ? 1.5f: 1));
    }
}
