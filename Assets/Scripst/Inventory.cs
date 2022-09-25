using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Item> item = new List<Item>();
    public List<UnityEvent> customEvent = new List<UnityEvent>();
    public List<GameObject> itemCountDisplay = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < itemCountDisplay.Count;i++)
        {
            item.Add(new Item(i));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            UseItem(0);
        }
        if(Input.GetKeyDown("2"))
        {
            UseItem(1);
        }
        if(Input.GetKeyDown("3"))
        {
            UseItem(2);
        }
        if(Input.GetKeyDown("4"))
        {
            UseItem(3);
        }
        if(Input.GetKeyDown("5"))
        {
            UseItem(4);
        }
    }

    public void AddItem(int id, int count)
    {
        item[id].count+=count;
        DisplayCount();
    }

    public int CheckItem (int id)
    {
        return item[id].count;
    }

    public bool UseItem (int id)
    {
        if(item[id].count>0)
        {
            customEvent[id].Invoke();
            item[id].count--;
            DisplayCount();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DisplayCount()
    {
        for(int i =0; i < itemCountDisplay.Count;i++)
        {
            itemCountDisplay[i].GetComponent<Text>().text = item[i].count.ToString();
        }
        
    }
}
