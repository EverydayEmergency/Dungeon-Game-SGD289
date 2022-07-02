using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Enemies
    {
        public GameObject enemy;
        public float probabilityOfSpawning;
    }

    public Enemies[] enemies;
    public GameObject[] spawnPoints;

    [HideInInspector]
    public bool[] activeSpawnPoints;

    [HideInInspector]
    public List<float> probability, cumulativeByRarity;
   
    int maxSpawnNum = 3;

    void Start()
    {
        activeSpawnPoints = new bool[spawnPoints.Length];
        EnemyProbability();
        GenerateEnemies();
    }

    void GenerateEnemies()
    {
        int spawnNumber = 0;
        RefreshActiveSpawnPoints();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int enemyIndex = GetEnemyByProbabilityRarity(probability); //chooses which enemy to spawn
            bool spawn = ProbablityCheck(enemies[enemyIndex].probabilityOfSpawning); //checks if that enemy is able to spawn
            if (spawn == true && spawnNumber < maxSpawnNum)
            {
                int spawnIndex = ChooseSpawnPoints();
                if (spawnIndex != -1 && activeSpawnPoints[spawnIndex]) //If the spawnIndex is valid
                {
                    var enemy = Instantiate(enemies[enemyIndex].enemy, spawnPoints[spawnIndex].transform); // Creates enemies
                    enemy.tag = "Enemy"; //tags enemies with the Enemy tag
                    spawnNumber++; //Increases spawn number to prevent spawning over the maximum number
                    
                    activeSpawnPoints[spawnIndex] = false;
                    
                }
            }
        }
    }

    public bool ProbablityCheck(float enemyProbablility)
    {
        float rnd = Random.Range(0, 101);
        if (rnd <= enemyProbablility)
            return true;
        else
            return false;
    }

    void EnemyProbability()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            probability.Add(enemies[i].probabilityOfSpawning);
        }
        MakeCumulativeByProbabilityRarity(probability); //Gives error if probability exceeds 100%
    }


    public int GetEnemyByProbabilityRarity(List<float> probabilityRarity)
    {
        //if your game will use this a lot of time it is best to build the arry just one time
        //and remove this function from here.
        MakeCumulativeByProbabilityRarity(probabilityRarity);

        float rnd = Random.Range(1, 101); //Get a random number between 0 and 100

        for (int i = 0; i < probabilityRarity.Count; i++)
        {
            if (rnd <= cumulativeByRarity[i]) //if the probility reach the correct sum
            {
                return i; //return the item index that has been chosen 
            }
        }
        return -1; //return -1 if some error happens
    }


    //this function creates the cumulative list
    void MakeCumulativeByProbabilityRarity(List<float> probabilityRarity)
    {
        float probabilitiesSum = 0;

        cumulativeByRarity = new List<float>(); //reset the Array

        float ProbilityModifier = GetprobabilityByRarityModifer(probabilityRarity);

        for (int i = 0; i < probabilityRarity.Count; i++)
        {
            probabilitiesSum += probabilityRarity[i] * ProbilityModifier; //add the probability to the sum
            cumulativeByRarity.Add(probabilitiesSum); //add the new sum to the list
        }

        //No need to check if it's bigger than 100 because it'll always be 100 max
    }

    //This function is used to get a modifer to be able to get the probabilityRarity List to fit in the cumulativeByRarity list from 0 to 100
    float GetprobabilityByRarityModifer(List<float> probabilityRarity) // 5 , 20 , 2
    {
        float itemRaritySum = 0;

        for (int i = 0; i < probabilityRarity.Count; i++)
            itemRaritySum += probabilityRarity[i];

        return 100 / itemRaritySum;
    }

    //Checks if spawnPoints are active
    void RefreshActiveSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            activeSpawnPoints[i] = true;
        }
    }

    int ChooseSpawnPoints() //Randomly chooses a spawn point
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (activeSpawnPoints[i])
            {              
                int rnd = Random.Range(0, spawnPoints.Length);
                return rnd;
            }
        }
        return -1;
    }

}
