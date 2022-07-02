using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DungeonGenerator generator;
    
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GlobalVar.floorNum += 1;
            NewFloor();
            
        }
        if(GlobalVar.newFloor == true)
        {           
            GlobalVar.floorNum += 1;
            NewFloor();
            GlobalVar.newFloor = false;
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
        GlobalVar.size.x++;
        GlobalVar.size.y++;
        DisposeOldDungeon();
        generator.MazeGenerator();
    }

}
