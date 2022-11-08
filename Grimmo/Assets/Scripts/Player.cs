using System;
using System.Collections;
using System.Collections.Generic;
// using Spine.Unity;
using UnityEngine;
using static UnityEngine.Physics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;   
    private bool Grounded;
    public float walkSpeed = 4f;
    public float JumpForce;
    public Vector2 velocidadRebote;

    public int life = 2;
    public GameObject bulletPrefab;
    private float lastShoot;
    public Vector2 respawnpoint;
    private Vector2 initialPosition;
    private bool isInCooldown;
    public float cooldownTime = 1f;

    // private SkeletonAnimation skeletonAnimation;
    private string previousState, currentState;
    private float xAxis;
    private float yAxis;
    private bool climbingAllowed = false;

    // Start is called before the first frame update
    void Start()
    {
        // skeletonAnimation = GetComponent<SkeletonAnimation>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        respawnpoint = initialPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //Eje X
        xAxis = Input.GetAxis("Horizontal");

         if(xAxis < 0.0f) //Si va a la izquierda
        {
            transform.localScale = new Vector3(-0.15f, 0.14f, 0.15f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if(xAxis > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(0.15f, 0.14f, 0.15f);
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

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
        }

        if(climbingAllowed){
            yAxis *= walkSpeed;
        }
        
        //-----Eje Y------


        // Cambio de animaciones
        // if(previousState != currentState)
        // {
        //     skeletonAnimation.state.SetAnimation(0, currentState, true);
        // }

        // previousState = currentState;
        // Cambio de animaciones

        if (Input.GetKeyDown(KeyCode.E) && Time.time > lastShoot + 1f)
        {
            Shoot();
            lastShoot = Time.time;
        }

        // if(Input.GetKeyDown(KeyCode.S) && destinyWarp){
        //     transform.position = destinyWarp.transform.position;
        // }

        DeathOnFall();

    }
        

    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);

        if(climbingAllowed){
            Rigidbody2D.isKinematic = true;
            Rigidbody2D.velocity = new Vector2(xAxis, yAxis);
        }else{
            Rigidbody2D.isKinematic = false;
            Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);
        }

        // if(!isInCooldown){
        // Rigidbody2D.velocity = new Vector2(xAxis, Rigidbody2D.velocity.y);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Ladder")){
            climbingAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        //Al salir de cualquier collider
        walkSpeed = 4f;

        if(collider.CompareTag("Ladder")){
            climbingAllowed = false;
        }
    }

    //cuando el jugador es tocado por el enemigo, el jugador
    //rebota a la dirrecion contraria
    public void Rebote(Vector2 puntoGolpe)
    {
       Rigidbody2D.velocity= new Vector2(-velocidadRebote.x * puntoGolpe.x,velocidadRebote.y);
    }

    public void Death()
    {
        transform.position = initialPosition;
        respawnpoint = initialPosition;
        if (life <= 0)
        {
            life = 2;
            //Todavia no tenemos el panel
            // for (int i = 0; i < lifesPanel.transform.childCount; i++)
            // {
            //     lifesPanel.transform.GetChild(i).gameObject.SetActive(true);
            // }
        }
    }

    public void Hit(float knockback, GameObject enemy)
    {
        if(!isInCooldown){
        StartCoroutine(cooldown());
        if (life > 0)
        {
            //lifesPanel.transform.GetChild(life).gameObject.SetActive(false);
            life -= 1;
            if(enemy)
            {
                Vector2 difference = (transform.position - enemy.transform.position);
                float knockbackDirection = difference.x >= 0? 1 : -1; //Esto es como un if. Si es > - me tira 1 o sino -1
                Rigidbody2D.velocity = new Vector2(knockbackDirection * knockback, knockback/2);
            }

        }
        else
        {
            Death();
        }
        }
    }

    IEnumerator cooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isInCooldown = false;
    }

    private void DeathOnFall()
    {
        if (transform.position.y < -10f)
        {
            transform.position = respawnpoint;
            Hit(0, null);
        }
    }

    public void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x > 0)
            direction = Vector3.right;
        else
            direction = Vector3.left;
        GameObject bullet =
            Instantiate(bulletPrefab,
            transform.position + direction * 0.8f,
            Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

}


