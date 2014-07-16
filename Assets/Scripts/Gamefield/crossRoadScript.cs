using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class crossRoadScript : MonoBehaviour
{

    /*private bool turnUp = false;
    private bool turnDown = false;
    private bool turnLeft = false;
    private bool turnRight = false;*/

    public List<Enums.Direction> possibleTurns = new List<Enums.Direction>();

    public GameObject child;

    public List<Enums.Direction> getTurningDirs()
    {/*
        if (turnUp == true)
        {
            possibleTurns.Add(Enums.Direction.Up);
        }
        if (turnDown == true)
        {
            possibleTurns.Add(Enums.Direction.Down);
        }
        if (turnLeft == true)
        {
            possibleTurns.Add(Enums.Direction.Left);
        }
        if (turnRight == true)
        {
            possibleTurns.Add(Enums.Direction.Right);
        }*/

        return possibleTurns;
    }

    public GameObject getChild()
    {
        return child;
    }

    /*public void setTurnUp(bool tf)
    {
        this.turnUp = tf;
    }

    public void setTurnDown(bool tf)
    {
        this.turnDown = tf;
    }

    public void setTurnLeft(bool tf)
    {
        this.turnLeft = tf;
    }

    public void setTurnRight(bool tf)
    {
        this.turnRight = tf;
    }*/

    public void addTurn(Enums.Direction dir)
    {
        possibleTurns.Add(dir);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
