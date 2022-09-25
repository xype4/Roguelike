using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject F_Panel;
    public Inventory inventory;

    void Start()
    {
        F_Panel.SetActive(false);
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<DoorOpen>() || other.gameObject.GetComponent<Item>())
        {
            F_Panel.SetActive(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey("f") && other.gameObject.GetComponent<DoorOpen>())
            other.gameObject.GetComponent<DoorOpen>().Open();

        if(Input.GetKey("f")&& other.gameObject.GetComponent<Item>())
        {
            inventory.AddItem(other.gameObject.GetComponent<Item>().id, other.gameObject.GetComponent<Item>().count);
            Destroy(other.gameObject, 0);
            F_Panel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<DoorOpen>() || other.gameObject.GetComponent<Item>())
        {
            F_Panel.SetActive(false);
        }
    }
}
