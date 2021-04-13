using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot //class that is data holder
{
    public PowerUp thisLoot;
    public int lootChance;
}
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;//array for loot objects

    public PowerUp LootPowerUp()
    {
        int cumProb=0;//all prob combined
        int currentProb=Random.Range(0,100);//range of how probable it is to appear
        for (int i=0; i<loots.Length;i++)
        {
            cumProb +=loots[i].lootChance;
            if(currentProb<=cumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }

}
