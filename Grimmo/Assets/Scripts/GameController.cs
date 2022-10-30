using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level = 0;

    public GameObject winSpot;

    private BoxCollider2D winCollider;

    public GameObject player;

    private CapsuleCollider2D playerCollider;

    public GameObject[] checkPoints;

    public bool isFinal;

    // Start is called before the first frame update
    void Start()
    {
        // winCollider = winSpot.GetComponent<BoxCollider2D>();
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        
    }

    void Update()
    {
        // if (winCollider.IsTouching(playerCollider))
        // {
        //     if(isFinal){
        //         SceneManager.LoadScene("Win"); //Para cargar la escena del ultimo nivel
        //     }else{
        //         SceneManager.LoadScene("Level" + (level + 1));
        //     }
        // }
        // foreach (var checkpoint in checkPoints)
        // {
        //     BoxCollider2D checkPointCollider =
        //         checkpoint.GetComponent<BoxCollider2D>();
        //     if (checkPointCollider.IsTouching(playerCollider))
        //     {
        //         player.GetComponent<Player>().respawnpoint = player.transform.position;
        //     }
        // }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }

    public void Pause(){
        if(Time.timeScale > 0){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }
}


