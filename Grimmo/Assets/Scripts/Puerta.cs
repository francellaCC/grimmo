using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{
    public int level = 0;
    public bool abrir = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (abrir)
            {
                SceneManager.LoadScene("Nivel" + (level + 1));
            }
        }
    }

    public void conLlave(bool llave)
    {
        abrir = llave;
    }
}
