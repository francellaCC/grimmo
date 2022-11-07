using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // public GameObject player;
    // public Transform bottomEdge;

  
    // // Update is called once per frame
    // void Update()
    // {
    //     if(player == null)
    //     {
    //         Debug.LogError("No Player");
    //     }
    //     Vector3 position = transform.position;
    //     // position.x = player.transform.position.x;
    //     position.y = player.transform.position.y;
    //     transform.position = position;
    // }

    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset; //Este timeOffset es el tiempo entre la camara y el jugador para que se vea fluido

    [SerializeField]
    Vector2 posOffset;

    //Modificando el BottomLimit podemos hacer el nivel lo más alto que queramos

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float topLimit;

    void Start(){
        
    }

    void Update(){
        //La posicion inicial de la camara
        Vector3 startPos = transform.position;

        //La posicion actual del jugador
        Vector3 endPos = player.transform.position;
        
        // endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        transform.position = new Vector3
        (
            //Clamp limita un valor a un rango que definimos. Aqui limita la posicion x con los limites Left y Right
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
            Mathf.Clamp(transform.position.y, topLimit, bottomLimit),
            transform.position.z
        ); 

    }

   void OnDrawGizmos() {

        //Gizoms son lineas o elementos que se usan com referencias en unity

        Gizmos.color = Color.red;
        //Top boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        //Right boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        //Bottom boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        //Left boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));

    }
}
