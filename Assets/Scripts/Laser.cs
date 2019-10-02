using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage;
    public float force;

    void Start()
    {
        force = 1000f;
        rb = GetComponent<Rigidbody2D>();
        Vector3 directon = new Vector3(force, 0, 0);
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