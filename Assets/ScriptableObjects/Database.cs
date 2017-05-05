using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Database")]
public class Database : ScriptableObject
{
    public ItemList[] itemLists;

    // Returns a random card of a certain level, including cards an amount of levels down
    public DBItem GetRandomItemOfLevel(int level, int levelsDown = 3)
    {
        ArrayList items = new ArrayList();

        // If level too high, clamp it to max card level
        if(level >= itemLists.Length) level = itemLists.Length - 1;

        // If there are no cards of a certain level, lower required level
        while(itemLists[level] == null || itemLists[level].items.Length == 0) level--;

        for(int i = level; i >= level - levelsDown; i--)
        {
            if(i < 0) break;
            if(itemLists[i].items.Length == 0)
            {
                levelsDown++;
                continue;
            }

            items.AddRange(itemLists[i].items);
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
