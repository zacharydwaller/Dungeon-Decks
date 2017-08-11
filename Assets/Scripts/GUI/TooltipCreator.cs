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

        Vector2 anchorMin = rect.anchorMin;
        Vector2 anchorMax = rect.anchorMax;
        Vector2 pivot = rect.pivot;


        if(Camera.main.pixelHeight - Input.mousePosition.y < rect.sizeDelta.y)
        {

            anchorMin.y = 1;
            anchorMax.y = 1;
            pivot.y = 1;
        }
        else
        {
            anchorMin.y = 0;
            anchorMax.y = 0;
            pivot.y = 0;
        }

        if(Camera.main.pixelWidth - Input.mousePosition.x < rect.sizeDelta.x)
        {

            anchorMin.x = 1;
            anchorMax.x = 1;
            pivot.x = 1;
        }
        else
        {
            anchorMin.x = 0;
            anchorMax.x = 0;
            pivot.x = 0;
        }

        rect.anchorMax = anchorMax;
        rect.anchorMin = anchorMin;
        rect.pivot = pivot;

        tooltipObj.transform.position = Input.mousePosition;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        tooltipObj = Instantiate(tooltipRef, GameObject.FindGameObjectWithTag("Canvas").transform);

        if(item != null)
        {
            tooltipObj.GetComponent<Tooltip>().SetItem(item);
        }
        else
        {
            tooltipObj.GetComponent<Tooltip>().SetItem();
        }
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        Destroy(tooltipObj);
    }
}
