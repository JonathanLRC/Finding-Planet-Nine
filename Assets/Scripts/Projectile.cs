using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        PlayerProjectile,
        EnemyProjectile,
        BossProjectile,
        NotAssignedYet
    }
    Rigidbody2D rb;
    public int damage;
    Vector2 velocity;
    ProjectileType type = ProjectileType.NotAssignedYet;

    IEnumerator SetType(ProjectileType type)
    {
        this.type = type;
        if(type == ProjectileType.PlayerProjectile)
        {
            velocity = DataBase.ins.XmlDataBase.proyectileDB.list[0].velocity;
            Sprite projectileSprite = Resources.Load<Sprite>("Sprites/" + DataBase.ins.XmlDataBase.proyectileDB.list[0].texture);
            GetComponent<SpriteRenderer>().sprite = projectileSprite;
            rb = GetComponent<Rigidbody2D>();
            Vector3 directon = new Vector3(velocity.x, velocity.y, 0);
            rb.AddForce(directon, ForceMode2D.Impulse);
            gameObject.transform.localScale *= DataBase.ins.XmlDataBase.proyectileDB.list[0].dimensions.width / projectileSprite.rect.width;

            Destroy(GetComponent<PolygonCollider2D>());
            yield return new WaitForFixedUpdate();
            gameObject.AddComponent<PolygonCollider2D>();
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        else if(type == ProjectileType.EnemyProjectile)
        {
            velocity = DataBase.ins.XmlDataBase.proyectileDB.list[1].velocity;
            Sprite projectileSprite = Resources.Load<Sprite>("Sprites/" + DataBase.ins.XmlDataBase.proyectileDB.list[1].texture);
            GetComponent<SpriteRenderer>().sprite = projectileSprite;
            rb = GetComponent<Rigidbody2D>();

            Vector2 move = new Vector2(-1, 0);
            GameObject player = GameObject.Find("Player");
            Player other = (Player)player.GetComponent(typeof(Player));
            move = new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized;
            rb.velocity = move * velocity;

            Vector3 moveDirection = other.transform.position - gameObject.transform.position;
            if (moveDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            gameObject.transform.localScale *= DataBase.ins.XmlDataBase.proyectileDB.list[1].dimensions.width / projectileSprite.rect.width;

            Destroy(GetComponent<PolygonCollider2D>());
            yield return new WaitForFixedUpdate();
            gameObject.AddComponent<PolygonCollider2D>();
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        else if(type == ProjectileType.BossProjectile)
        {
            velocity = DataBase.ins.XmlDataBase.proyectileDB.list[2].velocity;
            Sprite projectileSprite = Resources.Load<Sprite>("Sprites/" + DataBase.ins.XmlDataBase.proyectileDB.list[2].texture);
            GetComponent<SpriteRenderer>().sprite = projectileSprite;
            rb = GetComponent<Rigidbody2D>();

            Vector2 move = new Vector2(-1, 0);
            GameObject player = GameObject.Find("Player");
            Player other = (Player)player.GetComponent(typeof(Player));
            move = new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized;
            rb.velocity = move * velocity;

            Vector3 moveDirection = other.transform.position - gameObject.transform.position;
            if (moveDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                Debug.Log("angle = " + angle);
                if(Mathf.Abs(angle) < 45 || Mathf.Abs(angle-180) < 45 || Mathf.Abs(angle + 180) < 45)
                {
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    Destroy(gameObject);
                    yield break;
                }
            }

            gameObject.transform.localScale *= DataBase.ins.XmlDataBase.proyectileDB.list[2].dimensions.width / projectileSprite.rect.width;

            Destroy(GetComponent<PolygonCollider2D>());
            yield return new WaitForFixedUpdate();
            gameObject.AddComponent<PolygonCollider2D>();
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (type == ProjectileType.PlayerProjectile)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
        else if (type == ProjectileType.EnemyProjectile || type == ProjectileType.BossProjectile)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
    }

    void SetDamage(int damage)
    {
        this.damage = damage;
    }
}