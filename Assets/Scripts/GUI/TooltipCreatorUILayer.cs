using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipCreatorUILayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipRef;

    object item;
    GameObject tooltipObj;
    bool mouseOver = false;
    bool showTooltip = true;

    private void Update()
    {
        if (!showTooltip) return;

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

    public void SetShowTooltip(bool show)
    {
        if(show)
        {
            showTooltip = true;
        }
        else
        {
            showTooltip = false;
            mouseOver = false;
            
            if(tooltipObj)
            {
                Destroy(tooltipObj);
            }
        }
    }

    private void MoveTooltip()
    {
        if (!showTooltip) return;

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

    public void OnPointerEnter(PointerEventData e)
    {
        if (!showTooltip) return;

        mouseOver = true;
        tooltipObj = Instantiate(tooltipRef, GameObject.FindGameObjectWithTag("Canvas").transform);

        tooltipObj.GetComponent<Tooltip>().SetItem(item);
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (!showTooltip) return;

        mouseOver = false;
        Destroy(tooltipObj);
    }
}
