using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : Entity
{
    public CardInfo card;

    public GameObject cardGraphicRef;

    GameObject cgObject;
    bool mouseOver = false;

    protected override void Awake()
    {
        base.Awake();

        data.type = Entity.Type.Card;
        data.card = card;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(mouseOver)
        {
            MoveTooltip();
        }
    }

    public void Pickup()
    {
        GameManager.singleton.UnregisterEntity(this);

        if(cgObject)
        {
            Destroy(cgObject);
        }

        Destroy(gameObject);
    }

    public override void SetData(Data newData)
    {
        base.SetData(newData);

        card = data.card;
    }

    public void SetCard(CardInfo newCard)
    {
        card = newCard;
        data.card = card;
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

    public override void DoTurn() { }
    public override void TakeDamage(int amount) { }
}
