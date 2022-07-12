using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorNumberUI : MonoBehaviour
{
    Text floorText;
    // Start is called before the first frame update
    void Start()
    {
        floorText = GetComponent<Text>();
        UpdateFloorNumberText();
    }

    public void UpdateFloorNumberText()
    {
        if(floorText != null)
            floorText.text = "Floor: " + GlobalVar.floorNum;
    }
}
