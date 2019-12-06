using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthAlienBoss : MonoBehaviour
{
    public float TotalHp = 10;
    public float CurrentHp;
    void Start()
    {
        TotalHp = 10;
        CurrentHp = TotalHp;
    }

    void Update()
    {
        CurrentHp = AlienBoss.resistance;
        //Debug.Log("Esto vale la Total" + 10);
        //Debug.Log( "esto vale la div " + CurrentHp / 10);
        if (CurrentHp <= 100)
        {
            CurrentHp = CurrentHp / 10;
            if (CurrentHp / 10 >= 0)
            {
                transform.localScale = new Vector3((CurrentHp / 10), 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(0, 1, 1);
            }
        }

    }
}
