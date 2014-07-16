/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy3 : MonoBehaviour
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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "crossRoad(Clone)")
        {
            crossRoadScript crossroad = other.GetComponent<crossRoadScript>();
            GameObject child = crossroad.getChild();

            Vector2 botLeft = transform.position;
            Vector2 botRight = botLeft;
            botRight.x += transform.localScale.x;
            Vector2 topLeft = botLeft;
            topLeft.y += transform.localScale.y;
            Vector2 topRight = topLeft;
            topRight.x += transform.localScale.x;
            if (other.bounds.Contains(botLeft) && other.bounds.Contains(botRight) && other.bounds.Contains(topLeft) && other.bounds.Contains(topRight))
            {
                print("inside collider");
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