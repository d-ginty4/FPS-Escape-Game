using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas deadMenu;
    public Canvas victoryMenu;
    public Slider slider;
    public Animator storageDoor;
    public Transform StorageDoorPosition;
    public Animator mainDoor;
    public Transform MainDoorPosition;
    public Animator exitDoor;
    public Transform ExitDoorPosition;
    public Transform player;
    Slider healthBar;
    float stoargeDoorDistance;
    float mainDoorDistance;
    float exitDoorDistance;

    // Start is called before the first frame update
    void Start()
    {
        victoryMenu.enabled = false;
        StartGame();
        healthBar = deadMenu.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        ControlHealth();

        // if the player comes within 10 units of this door open it
        stoargeDoorDistance = Vector3.Distance(player.position, StorageDoorPosition.position);
        if(stoargeDoorDistance <= 10f)
        {
            storageDoor.SetBool("Open", true);
        }

        // if the player comes within 10 units of this door and has the keys open it
        mainDoorDistance = Vector3.Distance(player.position, MainDoorPosition.position);
        if(mainDoorDistance <= 5f && Player.items.Contains("Key 1") && Player.items.Contains("Key 2") )
        {
            mainDoor.SetBool("Open", true);
        }

        // if the player comes within 10 units of this door open it
        // since this is the final door end the game shortly after opening it
        exitDoorDistance = Vector3.Distance(player.position, ExitDoorPosition.position);
        if (exitDoorDistance <= 5f)
        {
            exitDoor.SetBool("Open", true);
            StartCoroutine(FinishGame());
        }
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(2);

        VictoryUI();
    }

    // UI displayed on death
    // stops the game and player movement and enables use of the mouse again
    public void DeadUI()
    {
        startMenu.enabled = true;
        deadMenu.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // stop the game, enable the mouse and display victory GUI
    public void VictoryUI()
    {
        startMenu.enabled = false;
        deadMenu.enabled = false;
        victoryMenu.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start the game
    // allow player to move and hide the mouse
    public void StartGame()
    {
        startMenu.enabled = false;
        deadMenu.enabled = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.moveable = true;
    }

    // Used in Death GUI to start game again
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // controls the player health
    public void ControlHealth()
    {
        healthBar.value = Player.playerHealth;
    }
}
