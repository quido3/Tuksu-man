using UnityEngine;
using System.Collections;

public class HeartSpawner : MonoBehaviour
{

    public GameObject lingonberry;
    public WallSpawner wallSpawner;

    int mapWidth = 11, mapHeight = 13;

    public float timer = 20;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 || !noChild())
        {
            timer -= Time.deltaTime;
        }
        else
        {
            spawnHeart();
        }
    }

    bool noChild()
    {
        if (this.transform.childCount == 0)
        {
            return true;
        }
        return false;
    }

    void spawnHeart()
    {
        int[,] map = wallSpawner.getMap();
        bool spawned = false;
        int tries = 0;

        while (spawned == false && tries < 1000)
        {
            tries++;
            int randX = Random.Range(0, mapWidth);
            int randY = Random.Range(0, mapHeight);
            if (map[randY, randX] == 0)
            {
                if (!closeToPlayer())
                {
                    GameObject berry = (GameObject)Instantiate(lingonberry, new Vector3(randX + 0.5f, mapHeight - randY - 0.5f, -1), Quaternion.identity);
                    berry.transform.parent = this.transform;
                    timer = 40;
                    spawned = true;
                }
            }
        }
    }

    bool closeToPlayer()
    {
        return false;
    }
}
