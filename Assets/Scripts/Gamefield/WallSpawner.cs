using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPiece;
    public GameObject sBall;
    public GameObject bBall;
    public GameObject ballParent;
    public GameObject crossRoad;
    int gameWidth = 11, gameHeight = 13;
    int[,] walls;
    List<int[,]> mapList = new List<int[,]>();

    // Use this for initialization
    void Start()
    {
        walls = new int[13,11]{ {3,0,0,0,0,1,0,0,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,0,1,3,1,0,1,1,0},
                                    {0,0,0,0,1,2,1,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,0,1,1,1,0,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,3}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{ {3,0,0,0,0,1,0,0,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,0,1,3,1,0,1,1,0},
                                    {0,0,0,0,1,2,1,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,0,1,1,1,0,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,3}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{ {3,0,0,0,0,1,0,0,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,0,1,3,1,0,1,1,0},
                                    {0,0,0,0,1,2,1,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,0,1,1,1,0,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,3}
                                    };
        mapList.Add(walls);
        walls = new int[13, 11]{ {3,0,0,0,0,1,0,0,0,0,3},
                                    {0,1,0,1,0,1,0,1,0,1,0},
                                    {0,0,0,0,0,0,0,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,0,1,3,1,0,1,1,0},
                                    {0,0,0,0,1,2,1,0,0,0,0},
                                    {1,0,1,0,1,1,1,0,1,0,1},
                                    {0,0,1,0,0,0,0,0,1,0,0},
                                    {0,1,1,1,0,1,0,1,1,1,0},
                                    {0,0,0,0,0,1,0,0,0,0,0},
                                    {0,1,1,0,1,1,1,0,1,1,0},
                                    {3,0,0,0,0,0,0,0,0,0,3}
                                    };
        mapList.Add(walls);

        int rand = Random.Range(0,3);

        switch(rand){

        }




        for (int y = 0; y < gameHeight; y++)
        {
            for (int x = 0; x < gameWidth; x++)
            {
                if (walls[y, x] == 1)
                {

                    GameObject tile = (GameObject)Instantiate(wallPiece, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1), Quaternion.identity);
                    tile.transform.parent = this.transform;
                }
                else if (walls[y, x] == 3)
                {
                    spawnCrossroad(x, y);
                    GameObject ball = (GameObject)Instantiate(bBall, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1), Quaternion.identity);
                    ball.transform.parent = ballParent.transform;
                }
                else
                {
                    spawnCrossroad(x, y);
                    GameObject ball = (GameObject)Instantiate(sBall, new Vector3(x + 0.5f, gameHeight - y - 0.5f, -1), Quaternion.identity);
                    ball.transform.parent = ballParent.transform;
                }
            }
        }
    }

    void spawnCrossroad(int x, int y)
    {
        List<Enums.Direction> dirs = new List<Enums.Direction>();

        if (x - 1 >= 0)
        {
            if (walls[y, x - 1] == 0 || walls[y, x - 1] == 3)
            {
                dirs.Add(Enums.Direction.Left);
            }
        }
        if (x + 1 < gameWidth)
        {
            if (walls[y, x + 1] == 0 || walls[y, x + 1] == 3)
            {
                dirs.Add(Enums.Direction.Right);
            }
        }
        if (y - 1 >= 0)
        {
            if (walls[y - 1, x] == 0 || walls[y - 1, x] == 3)
            {
                dirs.Add(Enums.Direction.Up);
            }
        }
        if (y + 1 < gameHeight)
        {
            if (walls[y + 1, x] == 0 || walls[y + 1, x] == 3)
            {
                dirs.Add(Enums.Direction.Down);
            }
        }

        if (dirs.Count == 2)
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

            turn.transform.parent = ballParent.transform;
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

}
