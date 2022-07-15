using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public CharacterStats characterStats;
   
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    NewFloor newFloor;
    public PickupItem pickupItem;
    public string mainMenuScene;
    public PlayerLevel playerLevel { get; set; }
    public GameObject gameOverScreen;
    public AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        playerLevel = GetComponent<PlayerLevel>();
        characterStats = new CharacterStats(10, 10, 10, 10);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GlobalVar.playerDead = false;
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);    
        UIEventHandler.HealthChanged(currentHealth, maxHealth);
    }

    IEnumerator Die()
    {
        GetComponent<FpsMovement>().FreezeScreen();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        gameOverSound.Play(0);
        //Wait for 4 seconds
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(7));
        SceneManager.LoadScene(mainMenuScene);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (other.TryGetComponent(typeof(PickupItem), out Component pickupitem))
                {
                    this.pickupItem = pickupitem.GetComponent<PickupItem>();
                    Debug.Log(pickupItem.ItemDrop.ItemName);
                    pickupItem.Interact();
                }
            }
        }
    }
}
public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
