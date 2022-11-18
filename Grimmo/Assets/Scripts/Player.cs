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
    private GameObject destinyWarp;
    private Animator animator;
    public float walkSpeed = 4f;
    public float JumpForce;
    public Vector2 velocidadRebote;

    public int life = 2;
    public GameObject livesPanel;
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
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        respawnpoint = initialPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //Eje X
        xAxis = Input.GetAxis("Horizontal");

        if (xAxis < 0.0f) //Si va a la izquierda
        {
            transform.localScale = new Vector3(-0.15f, 0.14f, 0.15f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if (xAxis > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(0.15f, 0.14f, 0.15f);
        }
        animator.SetBool("isRunning", xAxis != 0.0f);

        // currentState = xAxis == 0 ? "Idle" : "Run"; //Si el hAxis es 0, el estado es Idle, si no, es Run
        //Eje X

        //------Eje Y------
        yAxis = Input.GetAxis("Vertical");

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
            animator.SetBool("isJumping", true);
        }

        if (climbingAllowed)
        {
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
            // animator.SetBool("isShooting", true);
            StartCoroutine(shootingTime());
            Shoot();
            lastShoot = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.S) && destinyWarp)
        {
            transform.position = destinyWarp.transform.position;
        }

        DeathOnFall();

    }


    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);

        if (climbingAllowed)
        {
            Rigidbody2D.isKinematic = true;
            Rigidbody2D.velocity = new Vector2(xAxis, yAxis);
        }
        else
        {
            Rigidbody2D.isKinematic = false;
            Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);
        }

        // if(!isInCooldown){
        // Rigidbody2D.velocity = new Vector2(xAxis, Rigidbody2D.velocity.y);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if(collider.name == "Tilemap")
        {
            Grounded = true;
        }

        if (collider.CompareTag("Ladder"))
        {
            climbingAllowed = true;
        }
        if (collider.name == "PointA" || collider.name == "PointB")
        {
            GameObject warp = collider.transform.parent.gameObject;
            if (collider.name == "PointA")
            {
                destinyWarp = warp.transform.Find("PointB").gameObject;
            }
            else
            {
                destinyWarp = warp.transform.Find("PointA").gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {

        if(collider.name == "Tilemap")
        {
            Grounded = false;
            animator.SetBool("isJumping", false);
        }

        //Al salir de cualquier collider
        walkSpeed = 4f;

        if (collider.CompareTag("Ladder"))
        {
            climbingAllowed = false;
        }
        if (collider.name == "PointA" || collider.name == "PointB")
        {
            destinyWarp = null;
        }
    }

    //cuando el jugador es tocado por el enemigo, el jugador
    //rebota a la dirrecion contraria
    public void Rebote(Vector2 puntoGolpe)
    {
        Rigidbody2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

    public void Death()
    {
        transform.position = initialPosition;
        respawnpoint = initialPosition;
        if (life <= 0)
        {
            life = 2;
            for (int i = 0; i < livesPanel.transform.childCount; i++)
            {
                livesPanel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void Hit(float knockback, GameObject enemy)
    {
        if (!isInCooldown)
        {
            StartCoroutine(cooldown());
            if (life >= 0)
            {
                livesPanel.transform.GetChild(life).gameObject.SetActive(false);
                life -= 1;
                if (enemy)
                {
                    Vector2 difference = (transform.position - enemy.transform.position);
                    float knockbackDirection = difference.x >= 0 ? 1 : -1; //Esto es como un if. Si es > - me tira 1 o sino -1
                    Rigidbody2D.velocity = new Vector2(knockbackDirection * knockback, knockback / 2);
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

    IEnumerator shootingTime()
    {
        animator.SetBool("isShooting", true); 
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isShooting", false);
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
        // animator.SetBool("isShooting", false);
    }

    public void Hit2()
    {
        if (life >= 0)
        {
            livesPanel.transform.GetChild(life).gameObject.SetActive(false);
            life -= 1;
        }
        else
        {
            Death();
        }
    }

}


