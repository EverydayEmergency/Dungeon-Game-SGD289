using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get { return Level * 25; } }

    public bool levelUp = false;
    public GameObject levelUpNotification;
    public AudioSource levelUpSound;
    // Start is called before the first frame update
    void Start()
    {
        CombatEvents.OnEnemyDeath += EnemyToExperience;
        Level = 1;
        UIEventHandler.PlayerLevelChanged();
    }

    private void Update()
    {
        if (levelUp)
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        levelUpSound.Play(0);
        levelUpNotification.SetActive(true);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(5)); 
        levelUpNotification.SetActive(false);
        levelUp = false;
    }

    public void EnemyToExperience(IEnemy enemy)
    {
        GrantExperience(enemy.Experience);
    }
    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;
        while (CurrentExperience >= RequiredExperience)
        {
            levelUp = true;
            CurrentExperience -= RequiredExperience;
            Level++;
        }
        UIEventHandler.PlayerLevelChanged();
    }
}
