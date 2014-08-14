using UnityEngine;
using System.Collections;

public class musicScript : MonoBehaviour
{

    public AudioClip startSound;
    public AudioClip loopSound;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        if (startSound != null)
        {
            source.clip = startSound;
            source.loop = false;
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying == false)
        {
            source.clip = loopSound;
            source.loop = true;
            source.Play();
        }
    }
}
