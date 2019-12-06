using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage;
    Vector2 velocity;

    Vector2 fasterSpeed;

    public bool isFaster = false;
    public bool canDestroy = true;
    public float f = 100;

    int dam;

    Effect effect;
    //public ObjectSpawner os;

    public float multiplier = 1.1f;
    private float duration = 7f;
    private SpriteRenderer sprite;
    public const string layerName = "Top";
    public int sortingorder = -30;
    public float speed;

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
    /*
    void Start()
    {
        velocity = new Vector2(100f,0);
        rb = GetComponent<Rigidbody2D>();
        Vector3 directon = new Vector3(-velocity.x, velocity.y, 0);
        rb.AddForce(directon, ForceMode2D.Impulse);
        //os = GameObject.Find("PowerUpSpawner").GetComponent<ObjectSpawner>();
    }
    */
    void OnBecameInvisible()
    {
        if(canDestroy)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Activate(other));
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("si se junt");
        }
        if (other.gameObject.tag == "PlayerProyectile")
        {
            Debug.Log("Bye moneda");
            Destroy(gameObject);
        }
    }

    void SetEffect(Tuple<Effect, int> args)
    {
        effect = args.Item1;

        foreach (var powerup in DataBase.ins.XmlDataBase.powerupDB.list)
        {
            if (powerup.effect == effect)
            {
                Sprite sp = Resources.Load<Sprite>("Sprites/" + powerup.texture);
                GetComponent<SpriteRenderer>().sprite = sp;
                Destroy(GetComponent<PolygonCollider2D>());
                gameObject.AddComponent<PolygonCollider2D>();
                gameObject.transform.localScale *= powerup.dimensions.width / sp.rect.width;
                break;
            }
        }
        switch (args.Item2)
        {
            case 0:
                duration = 6.0f;
                break;
            case 1:
                duration = 7.0f;
                transform.localScale *= 1.1f;
                break;
            case 2:
                duration = 5.0f;
                transform.localScale *= 0.9f;
                break;
            default:
                break;
        }
    }

    IEnumerator Activate(Collider2D player)
    {
        canDestroy = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Player.isInTutorial)
        {
            player.gameObject.SendMessage("SetPowerUpOfTutorial", SendMessageOptions.DontRequireReceiver);
        }
        if(effect == Effect.Immunity)
        {
            Debug.Log("Immunity PowerUp");
            player.gameObject.SendMessage("ActivateShield", duration, SendMessageOptions.DontRequireReceiver);
            player.transform.localScale *= multiplier;

            dam = Player.hp;
            Player.hp = 1000;

            sprite = GetComponent<SpriteRenderer>();
            sprite.transform.localScale = sprite.transform.localScale / 1000;

            GetComponent<PolygonCollider2D>().enabled = false;
            Debug.Log(Player.hp + "  daman"); //1000
            yield return new WaitForSeconds(duration);

            Deactivate(player);
        }
        else if(effect == Effect.Speed)
        {
            Debug.Log("Speed PowerUp");
            Player.velocity = new Vector2(50000.0f, 50000.0f);

            sprite = GetComponent<SpriteRenderer>();
            sprite.transform.localScale = sprite.transform.localScale / 1000;

            GetComponent<PolygonCollider2D>().enabled = false;
            yield return new WaitForSeconds(duration);

            Deactivate(player);
        }
        else if(effect == Effect.Strength)
        {
            Debug.Log("Strength PowerUp");
            Player.laserDamage = 3;

            sprite = GetComponent<SpriteRenderer>();
            sprite.transform.localScale = sprite.transform.localScale / 1000;

            GetComponent<PolygonCollider2D>().enabled = false;
            yield return new WaitForSeconds(duration);

            Deactivate(player);
        }
        else if(effect == Effect.TripleShoot)
        {
            Debug.Log("TripleShoot PowerUp");
            Player.tripleShoot = true;

            sprite = GetComponent<SpriteRenderer>();
            sprite.transform.localScale = sprite.transform.localScale / 1000;

            GetComponent<PolygonCollider2D>().enabled = false;
            yield return new WaitForSeconds(duration);

            Deactivate(player);
        }
    }

    void Deactivate(Collider2D player)
    {
        if (effect == Effect.Immunity)
        {
            Debug.Log("Immunity PowerUp Finished");
            player.gameObject.SendMessage("DeactivateShield", SendMessageOptions.DontRequireReceiver);
            Player.hp = dam;
            Debug.Log(Player.hp + "  damdes");//99*
            dam = 0;
            player.transform.localScale = player.transform.localScale / multiplier;

            Destroy(gameObject);
        }
        else if (effect == Effect.Speed)
        {
            Player.velocity = new Vector2(25000.0f, 25000.0f);
            Debug.Log("Speed PowerUp Finished");
            Destroy(gameObject);
        }
        else if (effect == Effect.Strength)
        {
            Player.laserDamage = 1;
            Debug.Log("Strength PowerUp Finished");
            Destroy(gameObject);
        }
        else if (effect == Effect.TripleShoot)
        {
            Player.tripleShoot = false;
            Debug.Log("TripleShoot PowerUp Finished");
            Destroy(gameObject);
        }
    }
}
