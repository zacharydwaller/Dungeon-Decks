using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyDatabase
{
    public static List<EnemyInfo> EnemyList;

    // Start is called before the first frame update
    static EnemyDatabase()
    {
        EnemyList = Resources.LoadAll("Enemies", typeof(EnemyInfo)).Cast<EnemyInfo>().ToList();
    }

    public static EnemyInfo GetEnemyOfTier(int tier)
    {
        List<EnemyInfo> enemyTier;

        do
        {
            enemyTier = EnemyList.Where(e => e.tier == tier).ToList();

            // May have to lower tier
            tier--;
        } while (enemyTier.Count == 0 && tier > 0);

        if (enemyTier.Count == 0) return null;

        int enemyRoll = Random.Range(0, enemyTier.Count);
        return enemyTier[enemyRoll];
    }
}
