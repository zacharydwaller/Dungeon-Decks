using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipCreator : MonoBehaviour
{
    public GameObject tooltipRef;

    object item;
    GameObject tooltipObj;
    bool mouseOver = false;

    private void Update()
    {
        if(mouseOver)
        {
            MoveTooltip();
        }
    }

    public void OnDestroy()
    {
        Destroy(tooltipObj);
    }

    public void SetItem(object newItem)
    {
        item = newItem;
    }

    private void MoveTooltip()
    {
        RectTransform rect = tooltipObj.GetComponent<RectTransform>();
        if(Camera.main.pixelHeight - Input.mousePosition.y < rect.sizeDelta.y)
        {

            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
            rect.pivot = new Vector2(0, 1);
        }
        else
        {
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);
        }

        tooltipObj.transform.position = Input.mousePosition;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        tooltipObj = Instantiate(tooltipRef, GameObject.FindGameObjectWithTag("Canvas").transform);

        tooltipObj.GetComponent<Tooltip>().SetItem(item);
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        Destroy(tooltipObj);
    }
}
