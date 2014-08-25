using UnityEngine;
using System.Collections;

public class SceneControllerScript : MonoBehaviour
{

    public AudioSource music;

    //Hopefully this fixes the 30fps on iPad
    // Use this for initialization
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
