using UnityEngine;
using System.Collections;

public class startHighScoreBtn : MonoBehaviour
{

    void OnMouseDown()
    {
        Application.LoadLevel("score");
    }
}
