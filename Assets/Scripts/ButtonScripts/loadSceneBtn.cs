using UnityEngine;
using System.Collections;

public class loadSceneBtn : MonoBehaviour
{

    public bool loadStart;
    public bool loadPlay;
    public bool loadContinue;
    public bool loadHighScore;
    public bool loadCredits;


    void OnMouseDown()
    {

        if (loadStart)
        {
            Application.LoadLevel("Start");
        }
        else if (loadPlay)
        {
            Application.LoadLevel("Play");
        }
        else if (loadContinue)
        {
            Application.LoadLevel("PlayContinue");
        }
        else if (loadHighScore)
        {
            Application.LoadLevel("Score");
        }
        else if (loadCredits)
        {
            Application.LoadLevel("Credits");
        }
    }
}
