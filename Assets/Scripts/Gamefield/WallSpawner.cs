using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallSpawner : MonoBehaviour
{
    public GameObject sBall;
    public GameObject bBall;
    public GameObject crossRoad;
    public GameObject spawnSpot;

    public List<GameObject> wallPieces = new List<GameObject>();

    public GameObject ballParent;
    public GameObject crossroadParent;
    public GameObject spawnParent;

    public PlayerScript tuksu;
    public GUIScript gui;
    public List<EnemyScript> enemies = new List<EnemyScript>();

    int gameWidth = 11, gameHeight = 13;

    int[,] walls;
    List<int[,]> mapList = new List<int[,]>();
    int ballCount = 0;

    // Use this for initialization
    void Start()
    {
        walls = new int[13, 11]{    {3,0,0,0,0,1,0,0,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,1,1,1,0,1,0,0},
                                    {0,1,1,0,0,3,0,0,1,1,0},
                                    {0,0,0,0,1,2,1,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,4,0,0,1,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,0,1,1,1,0,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,3}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{    {3,0,0,0,1,2,1,0,0,0,3},
                                    {0,1,1,0,0,3,0,0,1,1,0},
                                    {0,1,0,0,1,0,1,0,0,1,0},
                                    {0,1,0,1,1,0,1,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,1,1,0,1,1,1,0,1,1,1},
                                    {0,0,0,0,0,4,0,0,0,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,1,0,1,0,1,0,0,0},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {3,0,0,0,0,1,0,0,0,0,3}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{    {0,0,0,0,0,0,0,0,0,0,3},
                                    {0,1,1,1,0,1,0,1,0,1,0},
                                    {3,2,1,1,0,1,0,1,0,0,0},
                                    {0,1,1,1,0,1,0,1,0,1,1},
                                    {0,1,0,0,0,1,0,1,0,0,0},
                                    {0,1,0,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,0,0,0,0,4,0},
                                    {0,1,0,1,1,0,1,1,1,1,0},
                                    {0,1,0,1,0,0,1,0,0,0,0},
                                    {0,1,0,0,0,1,1,3,1,1,0},
                                    {0,1,0,1,0,0,1,0,0,0,0},
                                    {0,1,0,1,1,0,1,1,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,0}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{    {3,0,0,1,0,4,0,1,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,1,1,1,1,1,1,0,1},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,1,0,1,0,3,0,1,0,1,0},
                                    {3,0,0,1,1,2,1,1,0,0,3}
                                    };
        mapList.Add(walls);
        int rand = Random.Range(0, 4);
        walls = mapList[rand];

        for (int y = 0; y < gameHeight; y++)
        {
            for (int x = 0; x < gameWidth; x++)
            {
                if (walls[y, x] == 2)
                {
                    foreach (EnemyScript enemy in enemies)
                    {
                        enemy.setSpawnSpot(new Vector3(x + 0.5f, gameHeight - y - 0.5f, -2));
                    }
                    spawnCrossroad(x, y);
                    GameObject tile = (GameObject)Instantiate(spawnSpot, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1), Quaternion.identity);
                    tile.transform.parent = this.transform;
                }
                else if (walls[y, x] == 4)
                {
                    tuksu.setSpawn(new Vector3(x + 0.5f, gameHeight - y - 0.5f, -2));
                    spawnCrossroad(x, y);
                }
                else if (walls[y, x] == 1)
                {
                    int r = Random.Range(0, wallPieces.Count);
                    GameObject tile = (GameObject)Instantiate(wallPieces[r], new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1), Quaternion.identity);
                    tile.transform.parent = this.transform;
                }
                else if (walls[y, x] == 3)
                {
                    spawnCrossroad(x, y);
                    spawnBall(true, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1));

                }
                else
                {
                    spawnCrossroad(x, y);
                    spawnBall(false, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1));
                }
            }
        }

        gui.setBallCount(ballCount);

    }

    void spawnBall(bool big, Vector3 spot)
    {
        ballCount++;
        if (big)
        {
            GameObject ball = (GameObject)Instantiate(bBall, spot, Quaternion.identity);
            ball.transform.parent = ballParent.transform;
        }
        else
        {
            GameObject ball = (GameObject)Instantiate(sBall, spot, Quaternion.identity);
            ball.transform.parent = ballParent.transform;
        }
    }

    void spawnCrossroad(int x, int y)
    {
        List<Enums.Direction> dirs = new List<Enums.Direction>();
        bool closeSpawn = false;

        if (x - 1 >= 0)
        {
            if (walls[y, x - 1] == 2)
            {
                closeSpawn = true;
            }
            if (walls[y, x - 1] == 0 || walls[y, x - 1] == 3 || walls[y, x - 1] == 4)
            {
                dirs.Add(Enums.Direction.Left);
            }
        }
        if (x + 1 < gameWidth)
        {
            if (walls[y, x + 1] == 2)
            {
                closeSpawn = true;
            }
            if (walls[y, x + 1] == 0 || walls[y, x + 1] == 3 || walls[y, x + 1] == 4)
            {
                dirs.Add(Enums.Direction.Right);
            }
        }
        if (y - 1 >= 0)
        {
            if (walls[y - 1, x] == 2)
            {
                closeSpawn = true;
            }
            if (walls[y - 1, x] == 0 || walls[y - 1, x] == 3 || walls[y - 1, x] == 4)
            {
                dirs.Add(Enums.Direction.Up);
            }
        }
        if (y + 1 < gameHeight)
        {
            if (walls[y + 1, x] == 2)
            {
                closeSpawn = true;
            }
            if (walls[y + 1, x] == 0 || walls[y + 1, x] == 3 || walls[y + 1, x] == 4)
            {
                dirs.Add(Enums.Direction.Down);
            }
        }

        if (dirs.Count == 2 && closeSpawn == false)
        {
            if (isOppositeDir(dirs[0], dirs[1]))
            {
                dirs.Clear();
            }
        }

        if (dirs.Count != 0)
        {
            GameObject turn = (GameObject)Instantiate(crossRoad, new Vector3(x + 0.5f, gameHeight - y - 0.5f, 0), Quaternion.identity);
            crossRoadScript script = turn.GetComponent<crossRoadScript>();

            foreach (Enums.Direction dir in dirs)
            {
                script.addTurn(dir);
            }

            turn.transform.parent = crossroadParent.transform;
        }
    }

    bool isOppositeDir(Enums.Direction tmpdir, Enums.Direction newDir)
    {
        switch (newDir)
        {
            case Enums.Direction.Down:
                if (tmpdir == Enums.Direction.Up)
                {
                    return true;
                }
                break;
            case Enums.Direction.Left:
                if (tmpdir == Enums.Direction.Right)
                {
                    return true;
                }
                break;
            case Enums.Direction.Right:
                if (tmpdir == Enums.Direction.Left)
                {
                    return true;
                }
                break;
            case Enums.Direction.Up:
                if (tmpdir == Enums.Direction.Down)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public int[,] getMap()
    {
        return this.walls;
    }

}
