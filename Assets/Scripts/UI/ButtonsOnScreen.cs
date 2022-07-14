using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsOnScreen : MonoBehaviour
{
    public Button button;
    bool isActive;
    public GameObject panel;

    public void SwitchActiveness()
    {
        isActive = !isActive;
        if (isActive)
            panel.SetActive(true);
        else if (isActive == false)
            panel.SetActive(false);
    }
}
