using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public float spawnDelay;
    public GameObject Item;
    public GameObject Boss;
    public float upperbound;
    public float lowerbound;
    public int difficulty;
    void Start()
    {
        upperbound = transform.position.y;
        lowerbound = transform.position.y - 800;
        spawnDelay = 10.0f;
        int currentLevel = DataBase.ins.XmlDataBase.gameDB.status;
        if(currentLevel < 0 || currentLevel > 9)
        {
            currentLevel = 8;
        }

        foreach (var level in DataBase.ins.XmlDataBase.levelDB.list)
        {
            if (level.number == currentLevel)
            {
                difficulty = level.difficulty;
                spawnDelay = difficulty / 10.0f;
                foreach (var obstacle in level.Obstacles)
                {
                    if(obstacle == ObstacleType.Alien)
                    {
                        Boss.gameObject.SetActive(true);
                        Boss.gameObject.transform.position = new Vector2(650, 0);
                    }
                    else
                    {
                        StartCoroutine(Spawn(obstacle, Random.Range(0.0f, 2.0f), spawnDelay));
                    }
                }
                break;
            }
        }
    }

    IEnumerator Spawn(ObstacleType type, float initialDelay, float spawnDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(lowerbound, upperbound), 0);
            GameObject obstacle = Instantiate(Item, spawnPos, transform.rotation);
            obstacle.SendMessage("SetType", type, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(Random.Range(spawnDelay-0.5f, spawnDelay+0.5f));
        }
    }
}