using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObstacleMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public int resistance;

    void Start()
    {
        speed = 100;
        resistance = 5;
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
            //Destroy(gameObject);
            other.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Proyectile triggers " + gameObject.tag);
        resistance--;
        if(resistance <= 0)
        {
            Destroy(gameObject);
        }
    }
}