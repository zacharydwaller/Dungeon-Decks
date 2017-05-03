using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    public Card card;

    public GameObject cardGraphicRef;

    GameObject cgObject;
    bool mouseOver = false;

    private void Start()
    {
    }

    private void Update()
    {
        if(mouseOver)
        {
            MoveTooltip();
        }
    }

    public void MoveTooltip()
    {
        RectTransform rect = cgObject.GetComponent<RectTransform>();
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

        cgObject.transform.position = Input.mousePosition;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        cgObject = Instantiate(cardGraphicRef, GameObject.FindGameObjectWithTag("Canvas").transform);

        CardGraphic cardGraphic = cgObject.GetComponent<CardGraphic>();
        cardGraphic.card = card;
        cardGraphic.UpdateCardGraphic();
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        Destroy(cgObject);
    }
}
