using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    
    public Game game;
    public GameObject on;
    public GameObject off;
    public static Level current_level;
    void Start()
    {
        game = new Game();
        /*if (!game.AssertSec())
        {
            Debug.Log("An error has occured!\nReinstall the game!");
            return;
        }*/
        Debug.Log(current_level);
        
    }

    
    void Update()
    {
        
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }
}
