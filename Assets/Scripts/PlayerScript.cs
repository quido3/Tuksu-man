using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private Enums.Direction dir = Enums.Direction.None;
    public float speed = 5;
    public int points = 0;
    public bool super;
    public GUIScript gui;
    private Enums.Direction pendingDir = Enums.Direction.None;
    float timer = 0;

    public TrailRenderer trailRenderer;

    private float addAmount = 0.9f;
    private float addAmount2 = 0f;
    private float checkLength = 0.9f;

    public bool animate;

    private Vector2 spawnPoint = new Vector2(5.5f, 4.5f);

    // Use this for initialization
    void Start()
    {
        this.points = gui.getPoints();
        trailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (super == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                super = false;
                trailRenderer.enabled = false;
            }
        }
        checkTouch();
        //setPendingDir();
        if (dir != pendingDir)
        {
            if (checkTurning(pendingDir))
            {
                dir = pendingDir;
            }
        }
        move(dir);
    }

    public Enums.Direction getDirection()
    {
        return dir;
    }

    private void checkTouch()
    {
        if (Input.touchCount == 1)
        {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
        {
        //if (Input.GetMouseButtonDown(0))
        //{
            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 dist = (Vector2)this.transform.position - pos;
            if (Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
            {
                if (dist.x < 0)
                {
                    pendingDir = Enums.Direction.Right;
                }
                else
                {
                    pendingDir = Enums.Direction.Left;
                }
            }
            else
            {
                if (dist.y < 0)
                {
                    pendingDir = Enums.Direction.Up;
                }
                else
                {
                    pendingDir = Enums.Direction.Down;
                }
            }

        }
        }

    }

    public bool superOn()
    {
        return this.super;
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

    void setPendingDir()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (Mathf.Abs(move.x) > 0 || Mathf.Abs(move.y) > 0)
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
            {
                if (move.x > 0)
                {
                    pendingDir = Enums.Direction.Right;
                }
                else
                {
                    pendingDir = Enums.Direction.Left;
                }
            }
            else
            {
                if (move.y > 0)
                {
                    pendingDir = Enums.Direction.Up;
                }
                else
                {
                    pendingDir = Enums.Direction.Down;
                }
            }
        }
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
        //transform.rigidbody2D.AddForce(move * speed * Time.deltaTime);
        transform.rigidbody2D.MovePosition(transform.position + move * speed * Time.deltaTime);
        //transform.position += move * speed * Time.deltaTime;
    }

    bool checkCorridor(Enums.Direction checkDir)
    {
        if (checkDir != Enums.Direction.None)
        {
            Vector2 start1 = transform.position;
            Vector2 start2 = transform.position;
            Vector2 end1 = transform.position;
            Vector2 end2 = transform.position;
            int layerMask = 1 << 8;

            start1.x += 0.1f;
            start1.y += 0.1f;
            start2.x += 0.1f;
            start2.y += 0.1f;

            switch (checkDir)
            {
                case Enums.Direction.Up:
                    start1.y += 0.8f;
                    start2.y += 0.8f;
                    start2.x += 0.8f;
                    end1 = start1;
                    end2 = start2;

                    end1.y += 1;
                    end2.y += 1;
                    break;
                case Enums.Direction.Down:
                    start2.x += 0.8f;
                    end1 = start1;
                    end2 = start2;

                    end1.y += -1;
                    end2.y += -1;
                    break;
                case Enums.Direction.Left:
                    start2.y += 0.8f;
                    end1 = start1;
                    end2 = start2;

                    end1.x += -1;
                    end2.x += -1;
                    break;
                case Enums.Direction.Right:
                    start1.x += 0.8f;
                    start2.y += 0.8f;
                    start2.x += 0.8f;
                    end1 = start1;
                    end2 = start2;

                    end1.x += 1;
                    end2.x += 1;
                    break;
            }
            Debug.DrawLine(start1, end1, Color.red);
            Debug.DrawLine(start2, end2, Color.red);
            RaycastHit2D hit1 = Physics2D.Raycast(start1, end1, 0.5f, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(start2, end2, 0.5f, layerMask);
            Vector2 zero = new Vector2(0, 0);
            if (hit1.point != zero || hit2.point != zero)
            {
                return false;
            }
            else
            {
            }
        }
        return true;
    }

    void checkDirection()
    {
        Enums.Direction tmpDir = dir;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (Mathf.Abs(move.x) > 0 || Mathf.Abs(move.y) > 0)
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
            {
                if (move.x > 0)
                {
                    tmpDir = Enums.Direction.Right;
                }
                else
                {
                    tmpDir = Enums.Direction.Left;
                }
            }
            else
            {
                if (move.y > 0)
                {
                    tmpDir = Enums.Direction.Up;
                }
                else
                {
                    tmpDir = Enums.Direction.Down;
                }
            }
        }
        else
        {

        }
        if (checkCorridor(tmpDir))
        {
            dir = tmpDir;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "sBall(Clone)")
        {
            PointBallScript ballScript = other.gameObject.GetComponent<PointBallScript>();
            ballScript.disable();
            addPoints(5);
            gui.ballEaten();
        }
        else if (other.gameObject.name == "bBall(Clone)")
        {

            PointBallScript ballScript = other.gameObject.GetComponent<PointBallScript>();
            ballScript.disable();
            addPoints(25);
            gui.ballEaten();
            superOn(5);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ghost")
        {
            if (super == true)
            {
                EnemyScript ghost = other.gameObject.GetComponent<EnemyScript>();
                ghost.reset();
                addPoints(150);
            }
            else
            {
                die();
            }
        }
    }

    void die()
    {
        this.transform.position = spawnPoint;
        gui.deductLife();
        superOn(1);
    }

    void superOn(float time)
    {
        //Add ghosts run away and are eatable.
        timer = time;
        super = true;
        trailRenderer.enabled = true;
    }

    void addPoints(int amount)
    {
        this.points += amount;
        gui.addPoints(amount);
    }

    void deductPoints(int amount)
    {
        this.points -= amount;
        gui.deductPoints(amount);
    }

    public int getPoints()
    {
        return gui.getPoints();
    }
}
