using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float TotalHp = 10;
    public float CurrentHp;
    public RectTransform redBar;
    public RectTransform heart;
    
    void Start()
    {
        TotalHp = 10;
        CurrentHp = TotalHp;
        //Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width,UnityEngine.Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector3(UnityEngine.Screen.width -370, UnityEngine.Screen.height -40, 1);
        redBar.position = new Vector3(UnityEngine.Screen.width - 370, UnityEngine.Screen.height - 40, 1);
        heart.position = new Vector3(UnityEngine.Screen.width - 390, UnityEngine.Screen.height - 40, 1);

    }

    void Update()
    {
        CurrentHp = Player.hp;
        if (CurrentHp <= 10)
        {
            if(CurrentHp / 10 >= 0)
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
