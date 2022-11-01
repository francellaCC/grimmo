using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] private GameObject panelLlave;

    Puerta puerta;

    private void Start()
    {
        puerta = FindObjectOfType<Puerta>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panelLlave.gameObject.SetActive(true);
            puerta.conLlave(true);
            gameObject.SetActive(false);
        }
    }
    
}
