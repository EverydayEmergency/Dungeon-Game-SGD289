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
            // 0 = cannot spawn, 1 = can spawn, 2 = HAS to spawn, 3 = the starting room, 4 = the ending room

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1; //if its obligatory return 2 otherwise return 1
            }

            return 0;
        }

        public bool EndRoom(int x, int y)
        {
            if (x == GlobalVar.size.x - 1 && y == GlobalVar.size.y - 1)
            {
                return true;
            }
            return false;
        }

    }

    
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset; //distance between each room

    List<Cell> board;

    public void GenerateDungeon()
    {
        for (int i = 0; i < GlobalVar.size.x; i++)
        {
            for (int j = 0; j < GlobalVar.size.y; j++)
            {
                Cell currentCell = board[i + j * GlobalVar.size.x];
                
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> avalibleRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);
                        bool e = rooms[k].EndRoom(i, j);
                        
                        if(p == 2) //Obligatory
                        {
                            randomRoom = k;
                            break;
                        }
                        else if(p == 1)
                        {
                            avalibleRooms.Add(k);
                        }
                        else if(e == true)
                        {
                            randomRoom = k;
                            break;
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
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                    newRoom.tag = "Generated";
                    newRoom.gameObject.layer = LayerMask.NameToLayer("Ground");
                }                
            }
        }
    }

    

    public void MazeGenerator()
    {
        board = new List<Cell>();
        
        //Makes the board
        for (int i = 0; i < GlobalVar.size.x; i++)
        {
            for (int j = 0; j < GlobalVar.size.y; j++)
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
                int newCell;
                if(currentCell == 0) //If starting room
                {
                    newCell = neighbors[neighbors.Count - 1];
                }
                else 
                { 
                    newCell = neighbors[Random.Range(0, neighbors.Count)]; 
                }
                          

                if(newCell > currentCell)
                {
                    //down or right
                    if(newCell - 1 == currentCell)//right
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else //down
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)//left
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else//up
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
        if(cell - GlobalVar.size.x >= 0 && !board[(cell - GlobalVar.size.x)].visited) //first checks if there is a neighbor up and then checks if it has been visited
        {
            neighbors.Add((cell - GlobalVar.size.x));
        }

        //check down neighbor
        if (cell + GlobalVar.size.x < board.Count && !board[(cell + GlobalVar.size.x)].visited) 
        {
            neighbors.Add((cell + GlobalVar.size.x));
        }

        //check right neighbor
        if ((cell + 1) % GlobalVar.size.x  != 0 && !board[(cell + 1)].visited) //first makes sure cell has to be different then 0
        {
            neighbors.Add((cell + 1));
        }

        //check left neighbor
        if (cell % GlobalVar.size.x != 0 && !board[(cell - 1)].visited) //first makes sure cell has to be different then 0
        {
            neighbors.Add((cell - 1));
        }
        return neighbors;
    }
}
