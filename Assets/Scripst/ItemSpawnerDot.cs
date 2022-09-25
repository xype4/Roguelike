using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerDot : MonoBehaviour
{
    public Vector3 cords;
    public int level;

    public ItemSpawnerDot(Vector3 cords, int level)
    {
        this.cords = cords;
        this.level = level;
    }
}
