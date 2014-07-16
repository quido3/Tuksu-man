/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy2 : MonoBehaviour
{

    public GameObject tuksu;
    private Enums.Direction dir = Enums.Direction.None;
    public float speed = 5;

    public float enableTime = 0;
    private float enableTimer = 0;

    private float addAmount = 0.8f;
    private float addAmount2 = 0.1f;
    private float checkLength = 0.6f;

    Vector2 lastPos;
    Vector2 curPos;

    float timer = 0;

    Enums.Direction[] dirs = { Enums.Direction.Up, Enums.Direction.Down, Enums.Direction.Left, Enums.Direction.Right };

    bool enabled = false;

    // Use this for initialization
    void Start()
    {
        if (enableTime != 0)
        {
            this.disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        curPos = this.transform.position;

        if (startingCheck())
        {
            if (activeCheck())
            {
                if (dir == Enums.Direction.None)
                {
                    chooseFirstDir();
                }
                turningLoop();
                //move(dir);
            }
        }
        this.lastPos = curPos;
    }

    private bool activeCheck()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            enable();
            return true;
        }
        return false;
    }

    private bool startingCheck()
    {
        if (enableTime < enableTimer)
        {
            this.enable();
            return true;
        }
        else
        {
            enableTimer += Time.deltaTime;
        }
        return false;
    }

    private void chooseFirstDir()
    {
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                dir = Enums.Direction.Up;
                break;
            case 2:
                dir = Enums.Direction.Down;
                break;
            case 3:
                dir = Enums.Direction.Left;
                break;
            case 4:
                dir = Enums.Direction.Right;
                break;
        }
    }

    public void reset()
    {
        this.transform.position = new Vector2(9, 9);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        dir = Enums.Direction.None;
        timer = 15;
    }

    private void disable()
    {
        this.transform.position = new Vector2(9, 9);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        dir = Enums.Direction.None;
    }

    private void enable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void turningLoop()
    {
        //raycast stuffz
        Vector2 localScale = transform.localScale;
        Vector2 center = transform.position + (transform.localScale / 2);
        Vector2 zero = new Vector2(0, 0);

        //object corners
        Vector2 botLeft = transform.position;
        Vector2 botRight = botLeft;
        botRight.x += localScale.x;
        Vector2 topLeft = botLeft;
        topLeft.y += localScale.y;
        Vector2 topRight = topLeft;
        topRight.x += localScale.x;

        //raycast etc
        float posX = transform.position.x;
        float posY = transform.position.y;
        int layerMask = 1 << 8;
        float castDistance = 0.8f;

        //Lists
        List<Enums.Direction> dirList = new List<Enums.Direction>();
        List<Enums.Direction> finaldirList = new List<Enums.Direction>();

        //Hits for raycast
        RaycastHit2D hit = new RaycastHit2D();
        RaycastHit2D hit2 = new RaycastHit2D();

        //check positions with single raycast from center
        foreach (Enums.Direction tmpdir in dirs)
        {
            switch (tmpdir)
            {
                case Enums.Direction.Up:
                    hit = Physics2D.Raycast(center, Vector2.up, castDistance, layerMask);
                    break;
                case Enums.Direction.Down:
                    hit = Physics2D.Raycast(center, -Vector2.up, castDistance, layerMask);
                    break;
                case Enums.Direction.Left:
                    hit = Physics2D.Raycast(center, Vector2.right, castDistance, layerMask);
                    break;
                case Enums.Direction.Right:
                    hit = Physics2D.Raycast(center, -Vector2.right, castDistance, layerMask);
                    break;
            }

            //add to list if not hitting
            if (hit.point == zero)
            {
                dirList.Add(tmpdir);
            }
        }

        //check possible turns with two rays
        foreach (Enums.Direction tmpdir in dirList)
        {
            switch (tmpdir)
            {
                case Enums.Direction.Up:
                    hit = Physics2D.Raycast(topLeft, Vector2.up, castDistance, layerMask);
                    hit2 = Physics2D.Raycast(topRight, Vector2.up, castDistance, layerMask);
                    break;
                case Enums.Direction.Down:
                    hit = Physics2D.Raycast(botLeft, -Vector2.up, castDistance, layerMask);
                    hit2 = Physics2D.Raycast(botRight, -Vector2.up, castDistance, layerMask);
                    break;
                case Enums.Direction.Left:
                    hit = Physics2D.Raycast(botLeft, -Vector2.right, castDistance, layerMask);
                    hit2 = Physics2D.Raycast(topLeft, -Vector2.right, castDistance, layerMask);
                    break;
                case Enums.Direction.Right:
                    hit = Physics2D.Raycast(botRight, Vector2.right, castDistance, layerMask);
                    hit2 = Physics2D.Raycast(topRight, Vector2.right, castDistance, layerMask);
                    break;
            }

            //add to final list if not hitting
            if (hit.point == zero && hit2.point == zero)
            {
                finaldirList.Add(tmpdir);
            }
        }



        //make a list of most preferred directions based on players position or something
        foreach (Enums.Direction tmpdir in dirs)
        {

        }


        foreach (Enums.Direction tmpdir in finaldirList)
        {
            print("can turn " + tmpdir);
        }



    }








    void corridorTurning()
    {
        List<Enums.Direction> dirList = new List<Enums.Direction>();

        Enums.Direction opposite = Enums.Direction.None;
        foreach (Enums.Direction tmpDir in dirs)
        {
            if (isOppositeDir(dir, tmpDir) == false)
            {
                if (checkTurning(tmpDir))
                {
                    dirList.Add(tmpDir);
                }
            }
            else
            {
                opposite = tmpDir;
            }
        }
        float length = Vector2.Distance(curPos, lastPos);
        print(length);
        if (dirList.Count == 0 && opposite != Enums.Direction.None && length < 0.05f)
        {
            dirList.Add(opposite);
        }
        bool turned = false;
        foreach (Enums.Direction tmpDir in dirList)
        {
            if (turned == false)
            {

                {
                    if (Random.Range(1, 100) <= 70)
                    {
                        this.dir = tmpDir;
                        turned = true;
                    }
                }
            }
        }
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

    bool checkTurning(Enums.Direction tmpdir)
    {
        RaycastHit2D hit = new RaycastHit2D();
        RaycastHit2D hit2 = new RaycastHit2D();
        int layerMask = 1 << 8;
        Vector2 pos = transform.position;
        switch (tmpdir)
        {
            case Enums.Direction.Up:
                pos.y += addAmount;
                pos.x += addAmount2;
                hit = Physics2D.Raycast(pos, Vector2.up, 1, layerMask);
                Debug.DrawRay(pos, Vector2.up, Color.red);

                pos.x += addAmount;
                hit2 = Physics2D.Raycast(pos, Vector2.up, checkLength, layerMask);
                Debug.DrawRay(pos, Vector2.up, Color.red);
                break;
            case Enums.Direction.Down:
                hit = Physics2D.Raycast(pos, -Vector2.up, checkLength, layerMask);
                pos.x += addAmount2;
                Debug.DrawRay(pos, -Vector2.up, Color.red);

                pos.x += addAmount;
                hit2 = Physics2D.Raycast(pos, -Vector2.up, checkLength, layerMask);
                Debug.DrawRay(pos, -Vector2.up, Color.red);
                break;
            case Enums.Direction.Left:
                hit = Physics2D.Raycast(pos, -Vector2.right, checkLength, layerMask);
                pos.y += addAmount2;
                Debug.DrawRay(pos, -Vector2.right, Color.red);

                pos.y += addAmount;
                hit2 = Physics2D.Raycast(pos, -Vector2.right, checkLength, layerMask);
                Debug.DrawRay(pos, -Vector2.right, Color.red);
                break;
            case Enums.Direction.Right:
                pos.x += addAmount;
                pos.y += addAmount2;
                hit = Physics2D.Raycast(pos, Vector2.right, checkLength, layerMask);
                Debug.DrawRay(pos, Vector2.right, Color.red);

                pos.y += addAmount;
                hit2 = Physics2D.Raycast(pos, Vector2.right, checkLength, layerMask);
                Debug.DrawRay(pos, Vector2.right, Color.red);
                break;
        }
        Vector2 zero = new Vector2(0, 0);
        if (hit.point == zero)
        {
            if (hit2.point != zero)
            {
                Debug.DrawLine(hit2.point, zero, Color.green);
            }
            else
            {
                return true;

            }
        }
        else
        {
            Debug.DrawLine(hit.point, zero, Color.green);
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
        //transform.position += move * speed * Time.deltaTime;
    }
}*/