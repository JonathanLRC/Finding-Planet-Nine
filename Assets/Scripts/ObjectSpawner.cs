using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{

    //variable for position, which will be used for calculating random position between two points
    //delay between spawns
    public float spawnDelay;
    //variable for prefab, which should be spawn
    public GameObject Item;
    public float upperbound;
    public float lowerbound;

    //will be executed once at start
    void Start()
    {
        //"Spawn" function will be called repeatedly
        upperbound = transform.position.y;
        lowerbound = transform.position.y-800;
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);
    }

    //spawn function
    void Spawn()
    {
        //calculate random position between AsteroidSpawner and RighPosition
        Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(lowerbound, upperbound), 0);
        //place prefab at calculated position
        Instantiate(Item, spawnPos, transform.rotation);
    }
}