using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSkin : MonoBehaviour
{
    public GameObject[] skins;
    private int count=0;

    void Start()
    {
        foreach(GameObject skin in skins){
            if (PlayerPrefs.GetString(skin.name) == "Selected")
            {
                skin.SetActive(true);
            }
            else
            {
                skin.SetActive(false);
                count++;
            }
        }
        if(count == skins.Length)
        {
            foreach(GameObject skin in skins)
            {
                if (skin.name == "Pig")
                    skin.SetActive(true);
            }
        }
    }

}
