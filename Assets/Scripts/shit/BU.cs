/*using UnityEngine;
using System.Collections;

public class BU : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
            RaycastHit2D hit = new RaycastHit2D();
                int layerMask = 1 << 8;
                Vector2 pos = transform.position;
                switch (tmpdir)
                {
                    case Enums.Direction.Up:
                        if (dir == Enums.Direction.Left)
                        {
                            pos.x += 1;
                            hit = Physics2D.Raycast(pos, Vector2.up, 1, layerMask);
                            Debug.DrawRay(pos, Vector2.up, Color.red);
                        }
                        else
                        {
                            hit = Physics2D.Raycast(pos, Vector2.up, 1, layerMask);
                            Debug.DrawRay(pos, Vector2.up, Color.red);
                        }
                        break;
                    case Enums.Direction.Down:

                        if (dir == Enums.Direction.Left)
                        {
                            pos.x += 1;
                            hit = Physics2D.Raycast(pos, -Vector2.up, 1, layerMask);
                            Debug.DrawRay(pos, -Vector2.up, Color.red);
                        }
                        else
                        {
                            hit = Physics2D.Raycast(pos, -Vector2.up, 1, layerMask);

                            Debug.DrawRay(pos, -Vector2.up, Color.red);
                        }
                        break;
                    case Enums.Direction.Left:

                        if (dir == Enums.Direction.Down)
                        {
                            pos.y += 1;
                            hit = Physics2D.Raycast(pos, -Vector2.right, 1, layerMask);
                            Debug.DrawRay(pos, -Vector2.right, Color.red);
                        }
                        else
                        {
                            hit = Physics2D.Raycast(pos, -Vector2.right, 1, layerMask);
                            Debug.DrawRay(pos, -Vector2.right, Color.red);
                        }
                        break;
                    case Enums.Direction.Right:

                        if (dir == Enums.Direction.Down)
                        {
                            pos.y += 1;
                            hit = Physics2D.Raycast(pos, Vector2.right, 1, layerMask);
                            Debug.DrawRay(pos, Vector2.right, Color.red);
                        }
                        else
                        {
                            hit = Physics2D.Raycast(pos, Vector2.right, 1, layerMask);
                            Debug.DrawRay(pos, Vector2.right, Color.red);
                        }
                        break;
                }
                Vector2 zero = new Vector2(0, 0);
                if (hit.point == zero)
                {
                    print("didnt hit");
                    return true;
                }
                print("hit");
                return false;

    }
}*/
