using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIvents : MonoBehaviour
{
    public void StartEvent(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log("0");
                return;
                
            case 1:
                Debug.Log("1");
                return;

            default:
                return;
        }
    }
}
