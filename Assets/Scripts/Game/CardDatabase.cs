using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public CardList[] cardLists;

    // Returns a random card of a certain level, including cards an amount of levels down
    public CardInfo GetRandomCardOfLevel(int level, int levelsDown = 0)
    {
        ArrayList cards = new ArrayList();

        // If level too high, clamp it to max card level
        if(level >= cardLists.Length) level = cardLists.Length - 1;

        // If there are no cards of a certain level, lower required level
        while(cardLists[level] == null || cardLists[level].cards.Length == 0) level--;

        for(int i = level; i >= level - levelsDown; i--)
        {
            if(i < 0) break;
            if(cardLists[i] == null)
            {
                levelsDown++;
                continue;
            }

            cards.AddRange(cardLists[i].cards);
        }

        // If no cards were found, keep decreasing level until we find a populated card list
        if(cards.Count == 0)
        {
            level -= (levelsDown + 1);

            while(cards.Count == 0 && level >= 0)
            {
                cards.AddRange(cardLists[level].cards);
                level--;
            }

            // If no cards were found
            if(cards.Count == 0) return null;
        }

        int rand = Random.Range(0, cards.Count);
        return (CardInfo) cards[rand];
    }
}

[System.Serializable]
public class CardList
{
    public CardInfo[] cards;
}
