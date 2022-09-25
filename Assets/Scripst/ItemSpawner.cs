using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<ItemSpawnerDot> itemSpawnPoints;
    public int maxLevel = 0;
    public GameObject atrifact;
    public Transform parentItemObject;

    public List<float> chance = new List<float>();
    public List<GameObject> items = new List<GameObject>();

    public void StartItemGenerate(List<ItemSpawnerDot> itemSpawnPoints)
    {
        this.itemSpawnPoints = itemSpawnPoints;

        for(int i = 0; i < itemSpawnPoints.Count;i++)
        {
            if(itemSpawnPoints[i].level > maxLevel)
                maxLevel = itemSpawnPoints[i].level;
        }

        for(int i = 0; i < itemSpawnPoints.Count;i++)
        {
            if(itemSpawnPoints[i].level == maxLevel)
            {
                if(atrifact!=null)
                Instantiate(atrifact, itemSpawnPoints[i].cords, new Quaternion(0,0,0,0), parentItemObject);
                itemSpawnPoints.RemoveAt(i);
                break;
            }
        }
        
        for(int i = 0; i < itemSpawnPoints.Count;i++)
        {
            for(int j = 0; j < items.Count;j++)
            {
                if(chance[j]>Random.Range(0f,1f))
                {
                    Instantiate(items[j], itemSpawnPoints[i].cords, new Quaternion(0,0,0,0), parentItemObject);
                    continue;
                }
            }
        }
    }
}
