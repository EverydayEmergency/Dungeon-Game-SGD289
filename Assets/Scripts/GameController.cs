using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DungeonGenerator generator;
    
    // Start is called before the first frame update
    void Awake()
    {
        GlobalVar.size.x = 3;
        GlobalVar.size.y = 3;
        generator = GetComponent<DungeonGenerator>();
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVar.newFloor == true)
        {
            GlobalVar.newFloor = false;
            GlobalVar.floorNum += 1;
            NewFloor();
        }
    }

    void StartNewGame()
    {
        GlobalVar.floorNum = 1;
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
        DisposeOldDungeon();
        generator.MazeGenerator();
    }

}
