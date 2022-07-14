using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFloor : MonoBehaviour
{
    FloorNumberUI floorUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GlobalVar.newFloor = true;
        }
    }
}
