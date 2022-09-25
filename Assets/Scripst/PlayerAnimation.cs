using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public int lightsCount;
    private Animator animation;
    public GameObject light;
    public GameObject torch;
    public Transform spawnPoint;
    private bool busy = false;
    private bool torchOn = false;
    private List<GameObject> lights = new List<GameObject>();

    void Start()
    {
        animation = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown("q") && !busy)
        {
            StartCoroutine(SpawnLight());
        }

        if(Input.GetKeyDown("e") && !busy)
        {
            StartCoroutine(TorchOnOff());
        }
    }

    IEnumerator SpawnLight()
    {
        busy = true;
        if(lights.Count >= lightsCount)
        {
            Destroy(lights[0],0);
            lights.RemoveAt(0);
        }

        animation.SetTrigger("Light");
        yield return new WaitForSeconds(1f);        
        lights.Add(Instantiate(light, spawnPoint.position, new Quaternion(0,0,0,0)));

        yield return new WaitForSeconds(2f);        
        busy = false;
    }

    IEnumerator TorchOnOff()
    {
        busy = true;
        animation.SetTrigger("TorchOnOff");

        if(torchOn == true)
        {
            yield return new WaitForSeconds(0.2f);        
            torch.SetActive(false);
            torchOn = false;
        }

        else
        {
            yield return new WaitForSeconds(0.2f);    
            torch.SetActive(true);
            torchOn = true;    
        }

        yield return new WaitForSeconds(2f);    
        busy = false;
    }
}
