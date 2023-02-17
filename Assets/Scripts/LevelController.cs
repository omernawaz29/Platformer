using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    [SerializeField] TextMeshProUGUI ninjaPickupText;
    [SerializeField] TextMeshProUGUI dragonPickupText;
    [SerializeField] TextMeshProUGUI livesPickupText;
    [SerializeField] TextMeshProUGUI potionsPickupText;
    [HideInInspector] public bool gameRunning;
    [HideInInspector] public bool moveEnemies;
    [HideInInspector] public int potions;
    int playersAtEnd;

    int ninjaPickups;
    int dragonPickups;
    int lives;

    Vector3 dragonCheckPoint;
    Vector3 ninjaCheckPoint;


    private void Awake()
    {
        restartButton.SetActive(false);
        gameRunning = true;
        moveEnemies = true;
        playersAtEnd = 0;
        Time.timeScale = 1;

        lives = 3;
        ninjaPickups = 0;
        dragonPickups = 0;
        potions = 0;
        


    }
    public void PlayerDied(GameObject player, int id)
    {
        lives--;
        if (lives < 0)
            lives = 0;
        UpdateText();
        if (lives == 0)
        {
            restartButton.SetActive(true);
            gameRunning = false;
        }
        else
        {
            Respawn(player, id);
        }
    }

    void Respawn(GameObject player, int id)
    {
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameRunning = true;

        if (id == 0)
            player.transform.position = dragonCheckPoint;
        else if (id == 1)
            player.transform.position = ninjaCheckPoint;


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void playerAtDoor()
    {
        playersAtEnd++;
        if(playersAtEnd == 2 && ninjaPickups == 0 && dragonPickups == 0)
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%3);
        }
    }

    public void playerExitDoor()
    {
        playersAtEnd--;
    }

    public void PickUpAdd(int id)
    {
        if (id == 0)
            dragonPickups++;
        else if (id == 1)
            ninjaPickups++;

        UpdateText();

    }

    public void PickUpCollected(int id)
    {
        if (id == 0)
            dragonPickups--;
        else if (id == 1)
            ninjaPickups--;
        else if (id == 2)
            potions++;

        UpdateText();
    }

    public void PickUpUsed(int id)
    {
        if (id == 2)
            potions--;
    }

    
    public void UpdateText()
    {
        ninjaPickupText.text = ninjaPickups.ToString();
        dragonPickupText.text = dragonPickups.ToString();
        livesPickupText.text = lives.ToString();
        potionsPickupText.text = potions.ToString();
    }


    public void SetCheckPoint(int id,Vector3 pos)
    {
        if (id == 0)
            dragonCheckPoint = pos;
        else if (id == 1)
            ninjaCheckPoint = pos;
        else if (id == 2)
            ninjaCheckPoint = dragonCheckPoint = pos;
    }

    
}
