using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    Rigidbody2D rb;
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
}