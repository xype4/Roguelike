using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private bool open = false;  
    private GameObject rotationDot;
    private GameObject doorWeed; // Да, дверной косяк и что?
    private Vector3 doorWeedPosition;
    private Quaternion doorWeedRotation;


    void Start()
    {
        rotationDot = gameObject.transform.parent.gameObject;
        for(int i = 0; i < rotationDot.transform.childCount; i++)
        {
            if(rotationDot.transform.GetChild(i).gameObject.tag == "DoorAddon")
            {
                doorWeed = rotationDot.transform.GetChild(i).gameObject;
                doorWeedPosition = doorWeed.transform.position;
                doorWeedRotation = doorWeed.transform.rotation;
                return;
            }
        }
    }

    public void Open()
    {
        if(!open)
        {
            open = true;
            StartCoroutine(OpenDoor());
            return;
        }
    }

    IEnumerator OpenDoor()
    {
        for(int i = 0; i < 75; i++)
        {
            yield return new WaitForSeconds(0.01f);        
            rotationDot.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
            doorWeed.transform.position = doorWeedPosition;
            doorWeed.transform.rotation = doorWeedRotation;
        }
    }
}
