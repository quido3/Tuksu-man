using UnityEngine;
using System.Collections;

public class PointBallScript : MonoBehaviour
{
    //public int pointAmount = 5;

    //float timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                enable();
            }
        }*/

    }

    public void disable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //timer = 45;
    }

    private void enable()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
