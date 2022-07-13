using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DungeonGenerator generator;
    public FloorNumberUI floorUI;
    // Start is called before the first frame update
    void Awake()
    {
        GlobalVar.size.x = 4;
        GlobalVar.size.y = 4;
        generator = GetComponent<DungeonGenerator>();
        generator.RoomsAvalible();
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NewFloor();
            
        }
        if(GlobalVar.newFloor == true)
        {           
            NewFloor();
        }
    }

    void StartNewGame()
    {
        GlobalVar.floorNum = 0;
        NewFloor();
        
    }

    public void DisposeOldDungeon()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    void NewFloor()
    {
        GlobalVar.floorNum += 1;
        if (GlobalVar.floorNum % 3 == 0)
        {
            Debug.Log("Expanding floor size");
            GlobalVar.size.x += 1;
            GlobalVar.size.y += 1;
        }
        floorUI.UpdateFloorNumberText();
        DisposeOldDungeon();
        generator.MazeGenerator();
        GlobalVar.newFloor = false;
    }

}
