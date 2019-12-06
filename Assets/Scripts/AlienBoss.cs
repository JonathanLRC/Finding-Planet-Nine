using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    public static AlienBoss instance;

    public static int resistance = 100;
    Rigidbody2D rb;
    public float speed;
    //
    public GameObject alienGreenBar;
    public GameObject alienRedBar;
    public GameObject heartEnemy;
    public GameObject laser;
    public GameObject player;

    public enum AlienBossState
    {
        FollowingPlayer,
        OnRush
    }
    AlienBossState state;
    public Vector2 targetPosition;
    public float rushStart;
    public float nextRush;
    
    IEnumerator Start()
    {
        Alien alien = DataBase.ins.XmlDataBase.alienDB.list[0];
        state = AlienBossState.FollowingPlayer;

        resistance = alien.resistance;
        nextRush = Time.time + 15.0f;

        rb = GetComponent<Rigidbody2D>();
        Vector3 move = new Vector3(0, 0, 0);
        speed = 100;
        rb.velocity = move * speed;
        Sprite sp = Resources.Load<Sprite>("Sprites/" + alien.texture);
        Debug.Log("texture: " + alien.texture);
        GetComponent<SpriteRenderer>().sprite = sp;
        Destroy(GetComponent<PolygonCollider2D>());
        yield return new WaitForFixedUpdate();
        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.transform.localScale *= alien.dimensions.width / sp.rect.width;

        Debug.Log("Boss Spawned");
        //
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, Camera.main.transform.position.z));

        Debug.Log("scren anchura " + UnityEngine.Screen.width);
        Debug.Log("posution alien " + gameObject.transform.position);


        alienRedBar.gameObject.SetActive(true);
        alienGreenBar.gameObject.SetActive(true);
        heartEnemy.gameObject.SetActive(true);

        alienGreenBar.transform.position = new Vector3(UnityEngine.Screen.width - 650, UnityEngine.Screen.height - 40, 1);
        alienRedBar.transform.position = new Vector3(UnityEngine.Screen.width - 650, UnityEngine.Screen.height - 40, 1);
        heartEnemy.transform.position = new Vector3(UnityEngine.Screen.width - 670, UnityEngine.Screen.height - 40, 1);

        StartCoroutine(ShootRoutine(0.0f, 1.0f));
    }

    void Update()
    {
        if(player.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(state == AlienBossState.FollowingPlayer)
        {
            Vector2 move = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
            rb.velocity = move * speed;
            if(Time.time > nextRush)
            {
                BeginRush();
            }
        }
        else if(state == AlienBossState.OnRush)
        {
            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, Camera.main.transform.position.z));
            if ( Mathf.Abs(transform.position.x) > screenBounds.x
             || Mathf.Abs(transform.position.y) > screenBounds.y )
            {
                Vector2 newTargetPosition = new Vector2(screenBounds.x + 50, Random.Range(0, screenBounds.y));
                if (targetPosition.x > 0)
                {
                    newTargetPosition.x *= -1;
                }
                if (targetPosition.y > 0)
                {
                    newTargetPosition.y *= -1;
                }
                targetPosition = newTargetPosition;
            }
            Vector2 move = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y).normalized;
            rb.velocity = move * speed;
            if(Time.time - rushStart > 10.0f)
            {
                state = AlienBossState.FollowingPlayer;
                speed = 100.0f;
                nextRush = Time.time + 15.0f;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("MakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            if (state == AlienBossState.FollowingPlayer)
            {
                BeginRush();
            }
        }
    }

    void BeginRush()
    {
        state = AlienBossState.OnRush;
        speed = 1000.0f;
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, Camera.main.transform.position.z));
        targetPosition = new Vector2(screenBounds.x + 50, Random.Range(0, screenBounds.y));
        if(Random.Range(0,2) == 1)
        {
            targetPosition.x *= -1;
        }
        if (Random.Range(0, 2) == 1)
        {
            targetPosition.y *= -1;
        }
        rushStart = Time.time;
    }

    IEnumerator ShootRoutine(float initialDelay, float delay)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            if(state == AlienBossState.FollowingPlayer)
            {
                Shoot();
            }
            yield return new WaitForSeconds(delay);
        }
    }

    void Shoot()
    {
        Vector3 bossSize = GetComponent<Renderer>().bounds.size;
        GameObject laserInstance = Instantiate(laser, new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().flipX ? bossSize.x/4 : -bossSize.x/4), transform.position.y), transform.rotation);
        laserInstance.SendMessage("SetType", Projectile.ProjectileType.BossProjectile, SendMessageOptions.DontRequireReceiver);
        laserInstance.SendMessage("SetDamage", 3, SendMessageOptions.DontRequireReceiver);
        //playOne();
    }

    public void MakeDamage(int damage)
    {
        resistance = resistance - damage;
        if (resistance <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Player.instance.playTwo(false);
        Destroy(gameObject);
    }
}
