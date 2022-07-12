using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Door door;
    void Start()
    {
        door = transform.Find("Door_01").GetComponent<Door>();
        if (door.enabled == false)
        {
            return;
        }        
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (door.enabled == true)
        {
            if (collider.tag == "Player")
            {
                door.GetComponent<Door>().DoorInteract();
            }
        }
    }

}
