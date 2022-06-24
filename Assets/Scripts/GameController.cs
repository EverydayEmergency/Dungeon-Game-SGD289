using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private DungeonGenerator generator;
    private bool floorEndReached;
    // Start is called before the first frame update
    void Start()
    {
        generator = GetComponent<DungeonGenerator>();
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartNewGame()
    {
        GlobalVar.floorNum = 1;
        NewFloor();
        floorEndReached = false;
    }

    void NewFloor()
    {
        generator.MazeGenerator();
    }

    void OnFloorEndTrigger(GameObject trigger, GameObject other)
    {
        NewFloor();
    }
}
