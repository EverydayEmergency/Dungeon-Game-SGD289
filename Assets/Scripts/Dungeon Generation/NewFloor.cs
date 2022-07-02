using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GlobalVar.size.x += 1;
            GlobalVar.size.y += 1;            
            GlobalVar.newFloor = true;           
        }
    }
}
