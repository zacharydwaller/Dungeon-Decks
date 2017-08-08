using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Database")]
public class Database : ScriptableObject
{
    public ItemList[] tiers;

    // Returns a random card of a certain tier, including cards an amount of levels down
    public DBItem GetItemOfTier(int tier, int levelsDown = 1)
    {
        ArrayList items = new ArrayList();

        // If level too high, clamp it to max card level
        if(tier >= tiers.Length) tier = tiers.Length - 1;

        for(int i = tier; i >= tier - levelsDown; i--)
        {
            if(i < 0) break;

            items.AddRange(tiers[i].items);
        }

        int rand = Random.Range(0, items.Count);
        return (DBItem) items[rand];
    }
}

[System.Serializable]
public class ItemList
{
    public DBItem[] items;
}
