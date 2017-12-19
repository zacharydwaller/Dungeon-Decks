using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TooltipCreator))]
public class CardPickup : Entity
{
    public CardInfo card;

    public GameObject cardGraphicRef;

    TooltipCreator tooltipCreator;

    protected override void Awake()
    {
        base.Awake();

        data.type = Entity.Type.Card;
        data.info = card;

        tooltipCreator = GetComponent<TooltipCreator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Pickup()
    {
        GameManager.singleton.UnregisterEntity(this);
        Destroy(gameObject);
    }

    public override void SetData(Data newData)
    {
        base.SetData(newData);

        card = (CardInfo) data.info;
        tooltipCreator.SetItem(card);
    }

    public void SetCard(CardInfo newCard)
    {
        card = newCard;
        data.info = card;
        tooltipCreator.SetItem(card);
    }

    public override void DoTurn() { }
    public override void TakeDamage(float amount) { }
}
