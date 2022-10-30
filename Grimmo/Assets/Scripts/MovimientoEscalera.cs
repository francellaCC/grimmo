using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEscalera : MonoBehaviour
{

    private float vertical;
    private float speed = 8f;
    private bool isEscalera;
    private bool isSubiendo;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isEscalera && Mathf.Abs(vertical) > 0f)
        {
            isSubiendo = true;
        }
    }

    private void FixedUpdate() {
        if (isSubiendo)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ladder"))
        {
            isEscalera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder"))
        {
            isEscalera = false;
            isSubiendo = false;
        }
    }
}
