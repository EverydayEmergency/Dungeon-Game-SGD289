using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public RectTransform characterPanel;
    bool menuIsActive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //characterPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            menuIsActive = !menuIsActive;
            characterPanel.gameObject.SetActive(menuIsActive);
        }
    }

    
}
