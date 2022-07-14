using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject background;
    private bool isActive = false;
    public void changeVisability()
    {
        isActive = !isActive;
    }
}
