using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObj : MonoBehaviour
{
    public string nowObj;
    public GameObject selectObj, buyObj;
    public Text stateText;

    public static bool isSelected;

    void Start()
    {
        if(PlayerPrefs.GetString("Pig") != "Selected" || PlayerPrefs.GetString("Pig") != "Open")
        {
            if (PlayerPrefs.GetString("Pig") != "Open")
            {
                PlayerPrefs.SetString("Pig", "Selected");
                selectObj.SetActive(false);
                buyObj.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        nowObj = other.gameObject.name;
        other.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        
        if (PlayerPrefs.GetString(other.gameObject.name) == "Open")
        {
            selectObj.SetActive(true);
            buyObj.SetActive(false);
            stateText.text = "Open".ToString();
        }
        else if (PlayerPrefs.GetString(other.gameObject.name) == "Selected")
        {
            selectObj.SetActive(false);
            buyObj.SetActive(false);
            stateText.text = "Selected".ToString();
        }
        else
        {
            selectObj.SetActive(false);
            buyObj.SetActive(true);
            stateText.text = "250 coins".ToString();
        }
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }

    void FixedUpdate()
    {
        if (isSelected)
        {
            stateText.text = "Selected".ToString();
            isSelected = false;
        }
    }
}
