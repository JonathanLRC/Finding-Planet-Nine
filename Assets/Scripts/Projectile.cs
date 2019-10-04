using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage;
    Vector2 velocity;

    void Start()
    {
        velocity = new Vector2(1000f, 0);
        rb = GetComponent<Rigidbody2D>();
        Vector3 directon = new Vector3(velocity.x, velocity.y, 0);
        rb.AddForce(directon, ForceMode2D.Impulse);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}