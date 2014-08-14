using UnityEngine;
using System.Collections;

public class flotAway : MonoBehaviour
{

    public bool left = false;
    public GameObject shadow;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Color c = shadow.GetComponent<SpriteRenderer>().color;
        c.a = c.a - 0.05f;
        shadow.GetComponent<SpriteRenderer>().color = c;
        Vector2 force = Vector2.zero;
        if (left == true)
        {
            force.x = -30;
        }
        else
        {
            force.x = 30;
        }
        this.rigidbody2D.AddForce(force);

        if (this.transform.position.x < -10 || this.transform.position.x > 20)
        {
            Destroy(this);
        }
    }
}
