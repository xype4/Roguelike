using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id = 0;
    public int count = 0;
    public Item(int id)
    {
        this.id = id;
    }
}
