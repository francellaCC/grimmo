using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1.0f;
    public bool friendBullet = true;
    public float bulletTime = 0.5f; 
    private Rigidbody2D bulletRigidbody;
    private Vector2 direction;
    public GameObject Player; 


    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(BulletTime());
    }

    void FixedUpdate(){
        bulletRigidbody.velocity = direction * speed;
    }

    public void SetDirection(Vector2 dir){
        direction = dir;
    }

    public void DestroyBullet(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null && friendBullet)
        {
            enemy.Hit();
            DestroyBullet();
        }
    }

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
        DestroyBullet();
    }

    IEnumerator BulletTime(){
        yield return new WaitForSeconds(bulletTime);
        Destroy(gameObject);
    }

}
