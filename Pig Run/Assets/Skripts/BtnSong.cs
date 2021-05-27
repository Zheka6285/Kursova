using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSong : MonoBehaviour
{
    public AudioSource btnSong;
    public AudioClip btnDown;
    public AudioClip btnUp;

    public void BtnDown()
    {
        btnSong.PlayOneShot(btnDown);
    }
    
    public void BtnUp()
    {
        btnSong.PlayOneShot(btnUp);
    }
}
