using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpSpawner : MonoBehaviour
{
    public float spawnDelay = 15.0f;
    public GameObject Item;
    public float upperbound;
    public float lowerbound;
    List<Effect> allEfects;
    void Start()
    {
        upperbound = transform.position.y;
        lowerbound = transform.position.y-800;
    }

    IEnumerator Spawn(float initialDelay, float spawnDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(lowerbound, upperbound), 0);
            GameObject powerup = Instantiate(Item, spawnPos, transform.rotation);
            System.Tuple<Effect, int> args = new System.Tuple<Effect, int>(allEfects[Random.Range(0, allEfects.Count)], Random.Range(0, 3));
            powerup.SendMessage("SetEffect",args, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(Random.Range(spawnDelay - 0.5f, spawnDelay + 0.5f));
        }
    }

    void OnEnable()
    {
        allEfects = new List<Effect>();
        foreach (var powerup in DataBase.ins.XmlDataBase.powerupDB.list)
        {
            allEfects.Add(powerup.effect);
        }
        if (!Player.isInTutorial)
        {
            spawnDelay = 20.0f;
            StartCoroutine(Spawn(Random.Range(0.0f, 2.0f), spawnDelay));
        }
    }

    void StartTutorialRoutine()
    {
        spawnDelay = 4.0f;
        StartCoroutine(SpawnTutorialPowerUps(1.0f, spawnDelay));
    }

    IEnumerator SpawnTutorialPowerUps(float initialDelay, float spawnDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        Debug.Log("Starting spawn powerup tutorial coroutine");
        Vector3 spawnPos = new Vector3(transform.position.x, 0, 0);
        int i = 0;
        while (true)
        {
            for(int j = 0; j < 3; j++)
            {
                spawnPos.y = -300 + 300*j;
                GameObject powerup = Instantiate(Item, spawnPos, transform.rotation);
                System.Tuple<Effect, int> args = new System.Tuple<Effect, int>(allEfects[i], System.Math.Abs(j-2));
                powerup.SendMessage("SetEffect", args, SendMessageOptions.DontRequireReceiver);
            }
            Debug.Log("Spawned " + allEfects[i] + " powerup, waiting " + spawnDelay + " seconds");
            i++;
            if(i >= allEfects.Count)
            {
                i = 0;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}