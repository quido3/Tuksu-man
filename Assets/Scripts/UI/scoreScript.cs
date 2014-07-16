using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scoreScript : MonoBehaviour
{
    public GameObject newScore, list1, list2, list3, list4, list5, list6, list7, list8, list9, list10;

    private int playerScore;

    List<int> scoreList = new List<int>();

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        playerScore = PlayerPrefs.GetInt("newScore");
        newScore.GetComponent<TextMesh>().text = "" + playerScore;

        addScores();
        saveScores();
        displayScores();
    }

    void displayScores()
    {
        list1.GetComponent<TextMesh>().text = "" + scoreList[0];
        list2.GetComponent<TextMesh>().text = "" + scoreList[1];
        list3.GetComponent<TextMesh>().text = "" + scoreList[2];
        list4.GetComponent<TextMesh>().text = "" + scoreList[3];
        list5.GetComponent<TextMesh>().text = "" + scoreList[4];
        list6.GetComponent<TextMesh>().text = "" + scoreList[5];
        list7.GetComponent<TextMesh>().text = "" + scoreList[6];
        list8.GetComponent<TextMesh>().text = "" + scoreList[7];
        list9.GetComponent<TextMesh>().text = "" + scoreList[8];
        list10.GetComponent<TextMesh>().text = "" + scoreList[9];
    }

    void saveScores()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < scoreList.Count)
            {
                PlayerPrefs.SetInt(i + 1 + "Score", int.Parse(scoreList[i].ToString()));
            }
            else
            {
                PlayerPrefs.SetInt(i + 1 + "Score", 0);
            }
        }
        PlayerPrefs.SetInt("newScore", 0);
    }

    void addScores()
    {
        //get all the scores to array
        for (int i = 1; i <= 10; i++)
        {
            int score = PlayerPrefs.GetInt(i + "Score");
            scoreList.Add(score);
        }
        scoreList.Add(playerScore);
        //sort them
        scoreList.Sort();
        scoreList.Reverse();
    }

    void printList(List<int> list)
    {
        print("Printing List");
        foreach (int i in list)
        {
            print(i);
        }
        print("Print done");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
