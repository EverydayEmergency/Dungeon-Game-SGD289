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

    public void MakeNewFloor()
    {
        GlobalVar.floorNum += 1;
        if (GlobalVar.floorNum % 3 == 0)
        {
            Debug.Log("Expanding floor size");
            GlobalVar.size.x += 1;
            GlobalVar.size.y += 1;
        }
        GlobalVar.newFloor = true;
        floorUI.UpdateFloorNumberText();
    }
}
