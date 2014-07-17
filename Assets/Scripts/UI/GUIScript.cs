using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{

    public GameObject pointsText;
    public GameObject timeText;
    public GameObject heart1, heart2, heart3;
    public bool end = false;
    public WallSpawner spawner;

    private int balls = 96;
    private int eaten = 0;

    float time = 120;
    int points = 0;

    // Use this for initialization
    void Start()
    {
        points = PlayerPrefs.GetInt("newScore");
        pointsText.GetComponent<TextMesh>().text = "" + points;
        timeText.GetComponent<TextMesh>().text = "" + (int)time;
    }

    public void setBallCount(int count)
    {
        this.balls = count;
    }

    public int getPoints()
    {
        return points;
    }

    // Update is called once per frame
    void Update()
    {
        if (end == true)
        {
            endGame();
        }
        time -= Time.deltaTime;
        updateTime();
        if (time <= 0)
        {
            endGame();
        }
    }

    public void deductLife()
    {
        if (heart3.GetComponent<SpriteRenderer>().enabled == true)
        {
            heart3.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (heart2.GetComponent<SpriteRenderer>().enabled == true)
        {
            heart2.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (heart1.GetComponent<SpriteRenderer>().enabled == true)
        {
            heart1.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            endGame();
        }
    }

    public void addLife()
    {
        if (heart1.GetComponent<SpriteRenderer>().enabled == false)
        {
            heart1.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (heart2.GetComponent<SpriteRenderer>().enabled == false)
        {
            heart2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (heart3.GetComponent<SpriteRenderer>().enabled == false)
        {
            heart3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void ballEaten()
    {
        eaten++;
        if (eaten == balls)
        {
            PlayerPrefs.SetInt("newScore", points);
            Application.LoadLevel("playContinue");
        }
    }

    private void endGame()
    {
        PlayerPrefs.SetInt("newScore", points);
        Application.LoadLevel("score");
    }

    public void addPoints(int amount)
    {
        this.points += amount;
        updatePoints();
    }

    public void addTime(float amount)
    {
        this.time += amount;
        updateTime();
    }

    public void deductPoints(int amount)
    {
        this.points -= amount;
        updatePoints();
    }

    public void deductTime(float amount)
    {
        this.time -= amount;
        updateTime();
    }

    private void updatePoints()
    {
        pointsText.GetComponent<TextMesh>().text = "" + points;
    }

    private void updateTime()
    {
        timeText.GetComponent<TextMesh>().text = "" + (int)time;
    }
}
