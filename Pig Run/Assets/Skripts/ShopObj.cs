using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopObj : MonoBehaviour
{
    [SerializeField] private GameObject[] shopObj;

    public Text coinText;

    void Update()
    {
        coinText.text = SaveProgress.allCoin.ToString();
    }

    public void ScaleShopObjBeforExit()
    {
        foreach (GameObject obj in shopObj)
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
}
