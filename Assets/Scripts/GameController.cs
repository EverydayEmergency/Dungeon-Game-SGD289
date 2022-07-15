using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DungeonGenerator generator;
    public FloorNumberUI floorUI;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVar.size.x = 4;
        GlobalVar.size.y = 4;
        generator = GetComponent<DungeonGenerator>();
        generator.RoomsAvalible();
        GlobalVar.floorNum = 0;
        NewFloor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GlobalVar.newFloor == true)
        {           
            NewFloor();
        }
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
        GlobalVar.newFloor = false;
        GlobalVar.floorNum += 1;
        if (GlobalVar.floorNum % 3 == 0)
        {
            GlobalVar.size.x += 1;
            GlobalVar.size.y += 1;
        }
        floorUI.UpdateFloorNumberText();
        DisposeOldDungeon();
        generator.MazeGenerator();      
    }

}
