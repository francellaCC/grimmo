using System;
using System.Collections;
using System.Collections.Generic;
// using Spine.Unity;
using UnityEngine;
using static UnityEngine.Physics;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;   
    private bool Grounded;
    public float walkSpeed = 3f;
    public float JumpForce;
    // private SkeletonAnimation skeletonAnimation;
    // private string previousState, currentState;
    // public GameObject[] waterSurface;
    // private bool isFloating = false;
    private float xAxis;
    private float yAxis;

    // Start is called before the first frame update
    void Start()
    {
        // skeletonAnimation = GetComponent<SkeletonAnimation>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Eje X
        xAxis = Input.GetAxis("Horizontal");

         if(xAxis < 0.0f) //Si va a la izquierda
        {
            transform.localScale = new Vector3(-0.4f, 0.477415f, 0.477415f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if(xAxis > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(0.4f, 0.477415f, 0.477415f);
        }

        // currentState = xAxis == 0 ? "Idle" : "Run"; //Si el hAxis es 0, el estado es Idle, si no, es Run
        //Eje X

        //------Eje Y------
        yAxis = Input.GetAxis("Vertical");
        
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else{
            // currentState = "jump_static"; 
            Grounded = false;
            }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
        }
        
        //-----Eje Y------


        // Cambio de animaciones
        // if(previousState != currentState)
        // {
        //     skeletonAnimation.state.SetAnimation(0, currentState, true);
        // }

        // previousState = currentState;
        // Cambio de animaciones
        
        // if(isFloating){
        //     walkSpeed = 0.5f;
        // }

    }

    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);
    }


}