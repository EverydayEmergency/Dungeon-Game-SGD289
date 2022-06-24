using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 = cannot spawn, 1 = can spawn, 2 = HAS to spawn

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1; //if its obligatory return 2 otherwise return 1
            }

            return 0;
        }
    }

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset; //distance between each room

    List<Cell> board;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVar.floorNum += 1;
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                
                if (currentCell.visited && currentCell != board[0])
                {
                    int randomRoom = -1;
                    List<int> avalibleRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        if(p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if(p == 1)
                        {
                            avalibleRooms.Add(k);
                        }

                        if(randomRoom == -1)
                        {
                            if (avalibleRooms.Count > 0)
                            {
                                randomRoom = avalibleRooms[Random.Range(0, avalibleRooms.Count)];
                            }
                            else
                            {
                                randomRoom = 0;
                            }
                        }

                    }

                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRoom(board[(i + j * size.x)].status);

                    newRoom.name += " " + i + "-" + j;
                }                
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        
        //Makes the board
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while(k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cells neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0) //if there are no avalible neighbors
            {
                if(path.Count == 0) //last cell on this path
                {
                    break;
                }
                else //current cell will become the last cell
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                
                int newCell = neighbors[Random.Range(0, neighbors.Count)];          

                if(newCell > currentCell)
                {
                    //down or right
                    if(newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else 
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if(cell - size.x >= 0 && !board[(cell - size.x)].visited) //first checks if there is a neighbor up and then checks if it has been visited
        {
            neighbors.Add((cell - size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited) 
        {
            neighbors.Add((cell + size.x));
        }
        //check right neighbor
        if ((cell + 1) % size.x  != 0 && !board[(cell + 1)].visited) //first makes sure cell has to be different then 0
        {
            neighbors.Add((cell + 1));
        }
        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited) //first makes sure cell has to be different then 0
        {
            neighbors.Add((cell - 1));
        }
        return neighbors;
    }
}
