using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparo : MonoBehaviour
{
    public bool indestructible = false;
    public float life = 3;
    public float knockback = 10f;
    public float distancia;
    public GameObject Player;
    private float LastShoot;
    public GameObject BulletPrefab;
    public float shootTime = 0.50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (distance < 3.0f && Time.time > LastShoot + shootTime)
        {
            ShootEnemy();
            LastShoot = Time.time;
        }
    }

    public void Hit()
    {
        life -= 1;
        if (!indestructible && life <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            player.Hit(knockback, gameObject);
        }
    }

    private void ShootEnemy()
    {
        Vector3 direction;
        if (transform.localScale.x <= 0f)
            direction = Vector3.left;
        else
            direction = Vector3.right;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
