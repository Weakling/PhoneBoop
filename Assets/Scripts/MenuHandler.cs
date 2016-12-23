using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuHandler : MonoBehaviour {


    public GameObject mainMenu, knightsMenu, checkersMenu, queensMenu, aboutMenu;
    public Text scoreKnights, scoreCheckers, scoreQueens;



	void Start ()
    {
        /*mainMenu = GameObject.Find("MainMenu");
        knightsMenu = GameObject.Find("KnightsMenu");
        checkersMenu = GameObject.Find("CheckersMenu");
        queensMenu = GameObject.Find("QueensMenu");
        aboutMenu = GameObject.Find("AboutMenu");

        knightsMenu.SetActive(false);
        checkersMenu.SetActive(false);
        queensMenu.SetActive(false);
        aboutMenu.SetActive(false);*/
	}
	
    public void GameMenuKnightsbtn()
    {
        mainMenu.SetActive(false);
        knightsMenu.SetActive(true);
        PlayerPrefs.SetInt("GameMode", 1);
        scoreKnights.text = "High score: " + PlayerPrefs.GetInt("HighScoreKnights", 0).ToString();
    }

    public void GameMenuCheckersbtn()
    {
        mainMenu.SetActive(false);
        checkersMenu.SetActive(true);
        PlayerPrefs.SetInt("GameMode", 2);
        scoreCheckers.text = "High score: " + PlayerPrefs.GetInt("HighScoreCheckers", 0).ToString();
    }

    public void GameMenuQueensbtn()
    {
        mainMenu.SetActive(false);
        queensMenu.SetActive(true);
        PlayerPrefs.SetInt("GameMode", 3);
        if(PlayerPrefs.GetInt("HighScoreQueens", 0) == 0)
        {
            scoreQueens.text = "Puzzle solved: no";
        }
        else
        {
            scoreQueens.text = "Puzzle solved: yes";
        }
    }

    public void aboutMenubtn()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void MainMenubtn()
    {
        knightsMenu.SetActive(false);
        checkersMenu.SetActive(false);
        queensMenu.SetActive(false);
        aboutMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("Iwork");
        SceneManager.LoadScene(1);
    }
}
