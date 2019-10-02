using UnityEngine;
using System.Collections;

public class HpController : MonoBehaviour
{
    public int hp;

    void MakeDamage(int damage)
    {
        hp = hp - damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}