using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empuje : MonoBehaviour
{
 /*private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<CambateJugador>().TomarDaño(20,other.GetContact(0).normal);
        }
    }

*/
       private void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.collider.GetComponent<Player>();
        EnemigoDisparo  enemigo = collision.collider.GetComponent<EnemigoDisparo>();
        if (player != null)
        {
            player.Hit2();
        }
        if (enemigo != null)
        {
            enemigo.Hit();
        }
       if(collision.gameObject.CompareTag("Player")){
        collision.gameObject.GetComponent<CambateJugador>().TomarDaño(20,collision.GetContact(0).normal);
       }
    }
}
