using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObstacleController : MonoBehaviour
{
    public static ObstacleController instance;


    Rigidbody2D rb;
    public float speed;
    public int resistance;
    public static int damage;
    float currentTime;
    float lifeSpan;
    float latestDirectionChangeTime;
    float directionChangeTime;
    bool inc = true;
    public GameObject laser;

    ObstacleType type;

    IEnumerator Start()
    {
        speed = 100.0f;
        rb = GetComponent<Rigidbody2D>();
        Vector3 move = new Vector3(-1, 0, 0);
        rb.velocity = move * speed;

        Destroy(GetComponent<PolygonCollider2D>());
        yield return new WaitForFixedUpdate();
        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > lifeSpan)
        {
            Vector2 move = calcuateExitMovementVector();
            rb.velocity = move * speed;
        }
        else if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            Vector2 move = calcuateNewMovementVector();
            rb.velocity = move * speed;
            latestDirectionChangeTime = Time.time;
        }
    }
    Vector2 calcuateNewMovementVector()
    {
        Vector2 ret = new Vector2(-1, 0);
        switch (type)
        {
            case ObstacleType.Asteroid:
                speed += 20.0f;
                ret = new Vector2(-1, 0).normalized;
                break;
            case ObstacleType.BlackHole:
                ret = new Vector2(-1, 0.5f * Mathf.Sin(5 * currentTime)).normalized;
                break;
            case ObstacleType.ExtraAlien:
                speed = 300;
                GameObject player = GameObject.Find("Player");
                ret = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
                break;
            case ObstacleType.ShootingStar:
                speed = 2000;
                Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, Camera.main.transform.position.z));
                ret = new Vector2(-screenBounds.x -150 - transform.position.x, -screenBounds.y -150 - transform.position.y).normalized;
                break;
            case ObstacleType.SpaceTrash:
                ret = new Vector2(Random.Range(-1.0f, -0.2f), Random.Range(-1.0f, 1.0f)).normalized;
                break;
            case ObstacleType.Star:
                if(gameObject.transform.localScale.x > 300.0f)
                {
                    inc = false;
                }
                else if(gameObject.transform.localScale.x < 50.0f)
                {
                    inc = true;
                }
                if (inc)
                {
                    gameObject.transform.localScale *= 1.1f;
                }
                else
                {
                    gameObject.transform.localScale *= 0.9f;
                }
                ret = new Vector2(Random.Range(-1.0f, -0.2f), Random.Range(-1.0f, 1.0f)).normalized;
                break;
            case ObstacleType.Alien:
                ret = new Vector2(-1, 0).normalized;
                break;
            default:
                ret = new Vector2(-1, 0).normalized;
                break;
        }
        return ret;
    }
    Vector2 calcuateExitMovementVector()
    {
        Vector2 ret = new Vector2(-1, 0);
        return ret;
    }
    void SetType(ObstacleType obstacleType)
    {
        damage = 3;
        resistance = 3;
        currentTime = 0.0f;
        speed = 150;
        lifeSpan = Mathf.Infinity;
        latestDirectionChangeTime = 0.0f;
        type = obstacleType;
        foreach(var obstacle in DataBase.ins.XmlDataBase.obstacleDB.list)
        {
            if(obstacle.type == obstacleType)
            {
                Sprite sp = Resources.Load<Sprite>("Sprites/" + obstacle.texture);
                if (obstacleType == ObstacleType.ShootingStar || obstacleType == ObstacleType.SpaceTrash)
                {
                    sp = Resources.Load<Sprite>("Sprites/" + obstacle.texture + Random.Range(0,2));
                }
                else if(obstacleType == ObstacleType.Star)
                {
                    sp = Resources.Load<Sprite>("Sprites/" + obstacle.texture + Random.Range(0, 4));
                }
                else
                {
                    sp = Resources.Load<Sprite>("Sprites/" + obstacle.texture);
                }
                GetComponent<SpriteRenderer>().sprite = sp;
                Destroy(GetComponent<PolygonCollider2D>());
                gameObject.AddComponent<PolygonCollider2D>();
                //Debug.Log("Multiplier: " + obstacle.dimensions.width / sp.rect.width);
                gameObject.transform.localScale *= obstacle.dimensions.width / sp.rect.width;
                if(obstacleType == ObstacleType.Asteroid)
                {
                    int r = Random.Range(1, 4);
                    if(r == 1)
                    {
                        gameObject.transform.localScale *= 0.6f;
                    }
                    else if(r == 3)
                    {
                        gameObject.transform.localScale *= 1.5f;
                    }
                }
                break;
            }
        }
        switch (obstacleType)
        {
            case ObstacleType.Asteroid:
                directionChangeTime = 0.0f;
                speed = 300;
                break;
            case ObstacleType.BlackHole:
                directionChangeTime = 0.1f;
                break;
            case ObstacleType.ExtraAlien:
                directionChangeTime = 0.0f;
                speed = 150;
                lifeSpan = Mathf.Infinity;
                if(Random.Range(0,2) == 1)
                {
                    StartCoroutine(ShootRoutine(1.0f));
                }
                break;
            case ObstacleType.ShootingStar:
                directionChangeTime = 0.0f;
                if (transform.position.y < 0)
                {
                    Vector3 viewPos = transform.position;
                    viewPos.y *= -1;
                    transform.position = viewPos;
                }
                break;
            case ObstacleType.SpaceTrash:
                //transform.Rotate(Vector3.forward * -35);
                directionChangeTime = 2.0f;
                speed = 300;
                break;
            case ObstacleType.Star:
                directionChangeTime = 0.1f;
                speed = 200;
                break;
            case ObstacleType.Alien:
                directionChangeTime = Mathf.Infinity;
                speed = 0;
                transform.position = new Vector2(0, 0);
                Debug.Log("Boss Spawned");
                break;
            default:
                directionChangeTime = 0.0f;
                break;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Die(true);
        }
    }
    
    void Shoot()
    {
        GameObject laserInstance = Instantiate(laser, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        laserInstance.SendMessage("SetType", Projectile.ProjectileType.EnemyProjectile, SendMessageOptions.DontRequireReceiver);
        laserInstance.SendMessage("SetDamage", 1, SendMessageOptions.DontRequireReceiver);
        //playOne();
    }

    IEnumerator ShootRoutine(float delay)
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(delay);
        }
    }

    public void MakeDamage(int damage)
    {
        resistance = resistance - damage;
        //Debug.Log("Obscatcle resistance: " + resistance);
        if (resistance <= 0)
        {
            Die(false);
        }
    }

    public void Die(bool collidedWithPlayer)
    {
        Player.instance.playTwo(collidedWithPlayer);
        Destroy(gameObject);
    }
}