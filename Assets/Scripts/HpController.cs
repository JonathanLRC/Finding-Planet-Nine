using UnityEngine;
using System.Collections;

public class HpController : MonoBehaviour
{

    //variable for health points
    public int hp;

    //funkton for damage calculation (we will get damage from other functions)
    void MakeDamage(int damage)
    {
        //decrease hp variable
        hp = hp - damage;
        //check if hp is negative or zero
        if (hp <= 0)
        {
            //delete gameobject
            Destroy(gameObject);
        }
    }
}