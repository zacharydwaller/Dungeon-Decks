using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDatabase
{
    public static List<Card> cardList;

    public static void LoadDatabase()
    {
        cardList = new List<Card>();

        TextAsset textAsset = Resources.Load<TextAsset>("Cards.xml");
        XDocument db = XDocument.Parse(textAsset.text);
        XNamespace dbSpace = db.Root.Name.Namespace;

        // Read each card
        foreach(var cardData in db.Descendants("Card"))
        {
            Card newCard = new Card();

            newCard.name = cardData.Element("Name").Value;
            newCard.color = ColorExtensions.ParseRBG(cardData.Element("Color").Value);
            newCard.image = Resources.Load<Sprite>(cardData.Element("Image").Value);
            newCard.target = (Card.Target) Enum.Parse(typeof(Card.Target), cardData.Element("Target").Value);
            newCard.targetStat = (Card.TargetStat) Enum.Parse(typeof(Card.TargetStat), cardData.Element("TargetStat").Value);
            newCard.effectAmount = Int32.Parse(cardData.Element("EffectAmount").Value);
            newCard.level = Int32.Parse(cardData.Element("Level").Value);
        }
    }

    public static List<Card> GetCards(int level, int minimum)
    {
        // Iterate through cards, putting appropriate leveled cards in return list
        // If don't have minimum amount of cards, reduce level by one
        // Repeat until we have minimum in return list

        return null;
    }
}
