using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenObjectOnContact : MonoBehaviour
{
    public GameObject sleepingObject;
    public string triggeringTag = "Player";

    public void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggeringTag)
        sleepingObject.SetActive(true);
        Destroy(gameObject);
    }
}
