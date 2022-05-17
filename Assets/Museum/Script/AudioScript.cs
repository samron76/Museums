using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public List<AudioClip> audios = new List<AudioClip>();
    public AudioSource audioSource;
    public bool playAudio;
    [SerializeField]
    private int _numbAudio;
    // Start is called before the first frame update
    void Start()
    {
        _numbAudio = Random.Range(0, audios.Count);
        audioSource.clip = audios[_numbAudio];
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
       
            if (!audioSource.isPlaying && playAudio)
            {
                if (_numbAudio + 1 > audios.Count)
                {
                    _numbAudio = 0;

                }
                else
                {
                    _numbAudio++;
                }
                audioSource.clip = audios[_numbAudio];
                audioSource.Play();
            }
        
    }
}
