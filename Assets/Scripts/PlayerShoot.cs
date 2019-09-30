using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject laser;
    public float delayTime;
    bool canShoot = true;

    void Update()
    {
        if (canShoot && Input.GetKeyDown("space"))
        {
            delayTime = 0.1f;
            canShoot = false;
            Instantiate(laser, transform.position, transform.rotation);
            StartCoroutine(NoFire());
        }
    }

    IEnumerator NoFire()
    {
        yield return new WaitForSeconds(delayTime);
        canShoot = true;
    }
}