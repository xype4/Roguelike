using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbientRandomizer : MonoBehaviour
{
    [Range(0,1)] public float chance = 0.5f;
    void Start()
    {
        if(chance>Random.Range(0f,1f));
            gameObject.SetActive(false);
    }

}
