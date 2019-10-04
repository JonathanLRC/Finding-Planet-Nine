using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObstacleController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public int resistance;
    ObstacleType type;

    void Start()
    {
        speed = 100;
        resistance = 3;
        rb = GetComponent<Rigidbody2D>();
        Vector3 move = new Vector3(-1, 0, 0);
        rb.velocity = move * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }
    }

    void MakeDamage(int damage)
    {
        resistance = resistance - damage;
        if (resistance <= 0)
        {
            Destroy(gameObject);
        }
    }
}