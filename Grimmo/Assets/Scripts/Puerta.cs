using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{
    public int level = 0;
    [SerializeField] private GameObject llave;
    public bool abrir = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
