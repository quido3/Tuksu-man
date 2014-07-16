using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chaser : MonoBehaviour
{

    public GameObject tuksu;
    PlayerScript tuksuScript;

    private Enums.Direction dir = Enums.Direction.None;
    public float speed = 5;

    public float enableTime = 0;
    private float enableTimer = 0;

    private Vector2 spawnSpot = new Vector2(5.5f, 6.5f);

    Vector2 prevTurnSpot = new Vector2(0, 0);

    private bool aiEnabled = false;

    // Use this for initialization
    void Start()
    {
        tuksuScript = tuksu.GetComponent<PlayerScript>();
        this.disable();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "crossRoad(Clone)")
        {
            if (prevTurnSpot.x != other.transform.position.x || prevTurnSpot.y != other.transform.position.y)
            {
                crossRoadScript crossroad = other.GetComponent<crossRoadScript>();

                Vector2 botLeft = transform.position;
                Vector2 botRight = botLeft;
                botRight.x += transform.localScale.x;
                Vector2 topLeft = botLeft;
                topLeft.y += transform.localScale.y;
                Vector2 topRight = topLeft;
                topRight.x += transform.localScale.x;
                if (other.bounds.Contains(botLeft) && other.bounds.Contains(botRight) && other.bounds.Contains(topLeft) && other.bounds.Contains(topRight))
                {
                    List<Enums.Direction> list = new List<Enums.Direction>(crossroad.getTurningDirs());

                    Enums.Direction d = Enums.Direction.None;

                    foreach (Enums.Direction tmpdir in list)
                    {
                        if (isOppositeDir(tmpdir, dir))
                        {
                            d = tmpdir;
                        }
                    }

                    if (d != Enums.Direction.None)
                    {
                        list.Remove(d);
                    }


                    this.dir = selectPreferredDir(list);
                    prevTurnSpot = other.transform.position;

                }
            }
        }
    }

    Enums.Direction selectPreferredDir(List<Enums.Direction> possDirs)
    {
        Vector2 dir = Vector2.zero;
        switch (tuksuScript.getDirection())
        {
            case Enums.Direction.Up:
                dir = Vector2.up;
                break;
            case Enums.Direction.Down:
                dir = -Vector2.up;
                break;
            case Enums.Direction.Right:
                dir = Vector2.right;
                break;
            case Enums.Direction.Left:
                dir = -Vector2.right;
                break;
        }

        LayerMask MyLayerMask = 1 << LayerMask.NameToLayer("turns");

        RaycastHit2D hit = Physics2D.Raycast(tuksu.transform.position + tuksu.transform.localScale / 2, dir, 5f, MyLayerMask);

        Vector2 getToPos = hit.point;

        print("going to: " + getToPos + " , Tuksu is at: " + tuksu.transform.position);

        float distU, distD, distR, distL;
        Vector2 dist = (Vector2)this.transform.position - getToPos;

        if (tuksuScript.superOn())
        {
            dist = dist * -1;
        }

        if (dist.x > 0)
        {
            distR = Mathf.Abs(dist.x);
            distL = 0;
        }
        else
        {
            distL = Mathf.Abs(dist.x);
            distR = 0;
        }

        if (dist.y > 0)
        {
            distU = Mathf.Abs(dist.x);
            distD = 0;
        }
        else
        {
            distD = Mathf.Abs(dist.x);
            distU = 0;
        }

        List<float> dirDist = new List<float>();

        //originalli iterates possDirs
        foreach (Enums.Direction tmpdir in possDirs)
        {
            switch (tmpdir)
            {
                case Enums.Direction.Up:
                    dirDist.Add(distU);
                    break;
                case Enums.Direction.Down:
                    dirDist.Add(distD);
                    break;
                case Enums.Direction.Left:
                    dirDist.Add(distL);
                    break;
                case Enums.Direction.Right:
                    dirDist.Add(distR);
                    break;
            }
        }

        dirDist.Sort();




        Enums.Direction firstDir = Enums.Direction.None;
        Enums.Direction secondDir = Enums.Direction.None;
        Enums.Direction thirdDir = Enums.Direction.None;
        Enums.Direction fourthDir = Enums.Direction.None;

        List<Enums.Direction> directionList = new List<Enums.Direction>();

        if (Mathf.Abs(dist.y) > Mathf.Abs(dist.x))
        {
            if (dist.y < 0)
            {
                firstDir = Enums.Direction.Up;
                fourthDir = Enums.Direction.Down;
            }
            else
            {
                firstDir = Enums.Direction.Down;
                fourthDir = Enums.Direction.Up;

            }
            if (dist.x > 0)
            {
                secondDir = Enums.Direction.Right;
                thirdDir = Enums.Direction.Left;
            }
            else
            {
                secondDir = Enums.Direction.Left;
                thirdDir = Enums.Direction.Right;
            }
        }
        else
        {
            if (dist.x < 0)
            {
                firstDir = Enums.Direction.Right;
                fourthDir = Enums.Direction.Left;
            }
            else
            {
                firstDir = Enums.Direction.Left;
                fourthDir = Enums.Direction.Right;

            }
            if (dist.y > 0)
            {
                secondDir = Enums.Direction.Up;
                thirdDir = Enums.Direction.Down;
            }
            else
            {
                secondDir = Enums.Direction.Down;
                thirdDir = Enums.Direction.Up;
            }
        }

        directionList.Add(firstDir);
        directionList.Add(secondDir);
        directionList.Add(thirdDir);
        directionList.Add(fourthDir);



        foreach (Enums.Direction d in directionList)
        {
            if (possDirs.Contains(d))
            {
                return d;
            }
        }
        return Enums.Direction.None;
    }

    // Update is called once per frame
    void Update()
    {

        if (enableTime < enableTimer)
        {
            int layerMask = 1 << 6;
            RaycastHit2D hit = Physics2D.Raycast(spawnSpot, Vector2.up, 1.5f, layerMask);

            if (hit.point == Vector2.zero)
            {
                this.enable();
                aiEnabled = true;
            }
            else
            {
                print("hits" + hit.point);
            }
            if (aiEnabled)
            {
                //First thing to select random direction
                if (dir == Enums.Direction.None)
                {
                    dir = Enums.Direction.Up;
                }
                move(dir);
            }
        }
        else
        {
            enableTimer += Time.deltaTime;
        }
    }

    public void reset()
    {
        this.transform.position = spawnSpot;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        dir = Enums.Direction.None;
        enableTimer = 0;
        enableTime = 5;
    }

    private void disable()
    {
        aiEnabled = false;
        this.transform.position = spawnSpot;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        dir = Enums.Direction.None;
    }

    private void enable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    bool isOppositeDir(Enums.Direction tmpdir, Enums.Direction newDir)
    {
        switch (newDir)
        {
            case Enums.Direction.Down: if (tmpdir == Enums.Direction.Up)
                {
                    return true;
                }
                break;
            case Enums.Direction.Left: if (tmpdir == Enums.Direction.Right)
                {
                    return true;
                }
                break;
            case Enums.Direction.Right: if (tmpdir == Enums.Direction.Left)
                {
                    return true;
                }
                break;
            case Enums.Direction.Up: if (tmpdir == Enums.Direction.Down)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    void move(Enums.Direction dir)
    {
        Vector3 move;
        switch (dir)
        {
            case Enums.Direction.Up:
                move = new Vector3(0, 1, 0);
                break;
            case Enums.Direction.Down:
                move = new Vector3(0, -1, 0);
                break;
            case Enums.Direction.Left:
                move = new Vector3(-1, 0, 0);
                break;
            case Enums.Direction.Right:
                move = new Vector3(1, 0, 0);
                break;
            default: move = new Vector3(0, 0, 0);
                break;
        }
        transform.rigidbody2D.MovePosition(transform.position + move * speed * Time.deltaTime);
    }
}