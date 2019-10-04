using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Vector2 velocity;
    int hp = 5;
    class PlayerControls
    {
        public KeyCode up, down, right, left, shoot;
        public PlayerControls(KeyCode up, KeyCode down, KeyCode right, KeyCode left, KeyCode shoot)
        {
            this.up = up;
            this.down = down;
            this.right = right;
            this.left = left;
            this.shoot = shoot;
        }
    }
    PlayerControls Controls;

    private bool isDead = false;
    int direction = 0;
    //private Animator anim;
    private Rigidbody2D rb2d;

    public GameObject laser;
    public float laserDelay;
    bool canShoot = true;

    void Start()
    {
        hp = 5;
        velocity = new Vector2(25000f, 25000f);
        Controls = new PlayerControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.Space);
        laserDelay = 0.1f;
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead == false)
        {
            if (canShoot && Input.GetKeyDown(Controls.shoot))
            {
                Shoot();
            }
            if (Input.GetKey(Controls.up))
            {
                //anim.SetTrigger("Up");
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, velocity.x));
                direction = 1;
            }
            else if(direction == 1)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.right))
            {
                //anim.SetTrigger("Right");
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(velocity.y, y));
                direction = 2;
            }
            else if (direction == 2)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.left))
            {
                //anim.SetTrigger("Left");
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(-velocity.y, y));
                direction = 3;
            }
            else if (direction == 3)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.down))
            {
                //anim.SetTrigger("Down");
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, -velocity.x));
                direction = 4;
            }
            else if (direction == 4)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if(direction == 0)
            {
                Vector2 v = rb2d.velocity;
                v.x = 0;
                v.y = (v.y > 0 ? 0 : v.y);
                rb2d.velocity = v;
            }
        }
    }

    void Shoot()
    {
        canShoot = false;
        Instantiate(laser, transform.position, transform.rotation);
        StartCoroutine(NoFire());
    }

    IEnumerator NoFire()
    {
        yield return new WaitForSeconds(laserDelay);
        canShoot = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            hp--;
            if(hp <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        //anim.SetTrigger("Die");
        GameControl.instance.PlayerDied();
    }

    void OnBecameInvisible()
    {
        Die();
    }
}