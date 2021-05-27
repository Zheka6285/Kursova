using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    public GameObject shopObj;
    private Vector3 screenPint, offset;
    private float _LockedYPos;

    void Update()
    {
        if (shopObj.transform.position.x > 0)
        {
            shopObj.transform.position = Vector3.MoveTowards(shopObj.transform.position, new Vector3(0f, shopObj.transform.position.y, shopObj.transform.position.z), Time.deltaTime * 10f);
        }
        else if (shopObj.transform.position.x < -3.5f)
        {
            shopObj.transform.position = Vector3.MoveTowards(shopObj.transform.position, new Vector3(-3.25f, shopObj.transform.position.y, shopObj.transform.position.z), Time.deltaTime * 10f);
        }
    }

    void OnMouseDown()
    {
        _LockedYPos = 1.07f;
        offset = shopObj.transform.position - Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPint.z));
        Cursor.visible = false;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPint.z);
        Vector3 curPosition = Camera.main.ScreenToViewportPoint(curScreenPoint) + offset;
        curPosition.y = _LockedYPos;
        shopObj.transform.position = curPosition;
    }

    void OnMouseUp()
    {
        Cursor.visible = true;
    }

}
