using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float upForce;
    public float sideForce;
    private bool isDead = false;
    int direction = 0;
    //private Animator anim;
    private Rigidbody2D rb2d;

    void Start()
    {
        upForce = 10000f;
        sideForce = 10000f;
        Debug.Log("upForce = " + upForce);
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetKey("up"))
            {
                //anim.SetTrigger("Up");
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, upForce));
                direction = 1;
            }
            else if(direction == 1)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey("right"))
            {
                //anim.SetTrigger("Up");
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(sideForce, y));
                direction = 2;
            }
            else if (direction == 2)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey("left"))
            {
                //anim.SetTrigger("Up");
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(-sideForce, y));
                direction = 3;
            }
            else if (direction == 3)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey("down"))
            {
                //anim.SetTrigger("Up");
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, -upForce));
                direction = 4;
            }
            else if (direction == 4)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        //anim.SetTrigger("Die");
        GameControl.instance.BirdDied();
    }
}