using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empuje : MonoBehaviour
{
 private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<CambateJugador>().TomarDaño(20,other.GetContact(0).normal);
        }
    }
}
