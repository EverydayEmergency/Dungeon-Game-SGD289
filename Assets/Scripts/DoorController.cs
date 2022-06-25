using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInChildren<Animator>();         
                anim.SetTrigger("Open");           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            Animator anim = other.GetComponentInChildren<Animator>();
            anim.SetTrigger("Close");
        }
    }

}
