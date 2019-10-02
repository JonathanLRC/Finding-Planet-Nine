using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public float spawnDelay;
    public GameObject Item;
    public float upperbound;
    public float lowerbound;

    void Start()
    {
        upperbound = transform.position.y;
        lowerbound = transform.position.y-800;
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);
    }

    void Spawn()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(lowerbound, upperbound), 0);

        Instantiate(Item, spawnPos, transform.rotation);
    }
}