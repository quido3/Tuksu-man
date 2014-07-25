using UnityEngine;
using System.Collections;

public class jumpScript : MonoBehaviour
{

    Vector3 origSpot = Vector3.zero;

    public float jumpForce = 0;

    // Use this for initialization
    void Start()
    {
        origSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= origSpot.y)
        {
            rigidbody2D.velocity = Vector3.zero;
            Vector2 jump = new Vector2(0, jumpForce);
            this.rigidbody2D.AddForce(jump);
        }
    }
}
