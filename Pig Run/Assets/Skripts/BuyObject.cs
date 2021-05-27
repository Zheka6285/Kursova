using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class BuyObject : MonoBehaviour
{
    public GameObject selectObj;
    public GameObject buyObj;
    public GameObject whichObject;

    public GameObject[] shopSkins;
    public GameObject[] heroSkins;

    public int price;

    private Save sv = new Save();
    private string path;

    public static bool isBuy = false;

    public void SendPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
    }

    public void SendCoin()
    {
        SendPath();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
    }

    public void WriteCoin()
    {
        SendCoin();
        sv.coin = SaveProgress.allCoin;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
        isBuy = true;
    }

    public void BuyObj()
    {
        if(SaveProgress.allCoin >= price)
        {
            SaveProgress.allCoin -= price;
            WriteCoin();
            PlayerPrefs.SetString(whichObject.GetComponent<SelectObj>().nowObj, "Open");
            selectObj.SetActive(true);
            buyObj.SetActive(false);
        }
    }

    public void SelectedObj()
    {
        foreach(GameObject obj in shopSkins)
        {
            if (PlayerPrefs.GetString(obj.name) == "Selected")
                PlayerPrefs.SetString(obj.name, "Open");
        }
        PlayerPrefs.SetString(whichObject.GetComponent<SelectObj>().nowObj, "Selected");
        foreach(GameObject obj in heroSkins)
        {
            if (whichObject.GetComponent<SelectObj>().nowObj == obj.name)
            {
                obj.SetActive(true);
            }
            else
                obj.SetActive(false);
        }
        SelectObj.isSelected = true;
        selectObj.SetActive(false);
        buyObj.SetActive(false);
    }
}
