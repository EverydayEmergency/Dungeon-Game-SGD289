using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gm.player.transform.position = spawnPoint.transform.position;
        GameManager.gm.player.transform.Rotate(0, 180, 0);
    }
}
