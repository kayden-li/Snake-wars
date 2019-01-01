using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour {
    public AudioClip[] music;
    public AudioClip end;
    public AudioSource source;
    private bool flag = true;
    void Awake()
    {
        int i = Random.Range(0, music.Length);
        source.clip = music[i];
        source.Play();
    }
    private void Update()
    {
        if (Data.isGameOver && flag && !Data.isTime)
        {
            source.clip = end;
            source.loop = false;
            source.Play();
            flag = false;
        }
        else if(!Data.isGameOver)
        {
            flag = true;
            source.loop = true;
        }
    }
}
