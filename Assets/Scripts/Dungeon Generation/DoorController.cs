using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Door door;
    void Start()
    {
        door = GetComponentInChildren<Door>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, .01f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Door")
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (door.gameObject.activeSelf == true)
        {
            if (collider.tag == "Player")
            {
                door.DoorInteract();
            }
        }
    }

}
