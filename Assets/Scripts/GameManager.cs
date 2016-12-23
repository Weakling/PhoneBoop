using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Vector3 tempTouch;
    public ParticleSystem particleCurrentTile;

    // UI
    public Text remainingTilesText, pressedTilesText;   // updated in check win condition
    public Button undoButton;                           // set active or disabled

    public int gameMode;         // selected game mode
    public bool gameOver;        // game has ended
    public bool gameWon;         // game won
    public bool gameLost;        // game lost

    public int totalTiles;       // each tile script increments this value on start
    public int pressedTiles;     // incremented on valid tile click
    public int remainingTiles;   // unpressed tiles. updated in checkwincondtion()

    // queen mode
    public Tile[] queen;
    public int totalQueens;      // total allowed queens in queen game mode
    public int placedQueens;     // placed queens in queen game mode
    public int remainingQueens;  // remaining queens to place in queen game mode


    public Tile currentTile;
    public Tile lastTile;

	// Use this for initialization
	void Start ()
    {
        // queens
        queen = new Tile[6];
        placedQueens = 0;
        totalQueens = 6;

        // UI
        undoButton.interactable = false;                            // disable starting undo

        gameMode = PlayerPrefs.GetInt("GameMode");                  // set to knight mode for testing
        Debug.Log(gameMode);
        gameWon = false;                                            // game set to not won
        gameLost = false;                                           // game set to not lost
        pressedTiles = 0;                                           // tiles pressed set to 0
        remainingTiles = totalTiles;                                // remaining tiles set to total tiles
	}
	
	
	void Update ()
    {
        if(lastTile != null)
        {
            undoButton.interactable = true;
        }

        // touch stuff that doesn't work yett
        if (Input.touchCount > 0)
        {
            tempTouch = Input.GetTouch(0).position;
            // touch space is within array..
            if(Mathf.CeilToInt(tempTouch.x) >= 0 &&
                Mathf.CeilToInt(tempTouch.x) < LevelManager.xSizeValue &&
                Mathf.CeilToInt(tempTouch.y) >= 0 &&
                Mathf.CeilToInt(tempTouch.y) < LevelManager.ySizeValue)
            {
                // touch space is NOT pressed
                if (LevelManager.tileGrid[Mathf.CeilToInt(tempTouch.x), Mathf.CeilToInt(tempTouch.y)].pressed == false)
                {
                    LevelManager.tileGrid[Mathf.CeilToInt(tempTouch.x), Mathf.CeilToInt(tempTouch.y)].TileClicked();    // run clicked method for tile space
                }
            }
        }
	}

    // checks current game mode and then that mode's win conditions
    // called on succesfull tile press
    public void CheckWinCondition()
    {
        bool tileOpen = false;  // if at end of game modes 1 or 2, tileOpen is still false, then game lost

        // NOT queen mode..
        if(gameMode != 3)
        {
            MoveParticles();    // move particles to current tile
        }
        
        // knight game mode..
        if (gameMode == 1)
        {
            UpdateTileCount();

            // pressed all tiles..
            if (pressedTiles == totalTiles)
            {
                GameOver(false);    // game won
            }
            // tiles are not all pressed..
            else
            {
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 2 >= 0 && (int)currentTile.transform.position.y + 1 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 2, (int)currentTile.transform.position.y + 1].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 1 >= 0 && (int)currentTile.transform.position.y + 2 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 1, (int)currentTile.transform.position.y + 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 1 < LevelManager.xSizeValue && (int)currentTile.transform.position.y + 2 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 1, (int)currentTile.transform.position.y + 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 2 < LevelManager.xSizeValue && (int)currentTile.transform.position.y + 1 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 2, (int)currentTile.transform.position.y + 1].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 2 < LevelManager.xSizeValue && (int)currentTile.transform.position.y - 1 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 2, (int)currentTile.transform.position.y - 1].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 1 < LevelManager.xSizeValue && (int)currentTile.transform.position.y - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 1, (int)currentTile.transform.position.y - 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 1 >= 0 && (int)currentTile.transform.position.y - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 1, (int)currentTile.transform.position.y - 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 2 >= 0 && (int)currentTile.transform.position.y - 1 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 2, (int)currentTile.transform.position.y - 1].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // no moves remain
                if(tileOpen == false)
                {
                    GameOver(true);
                }
            }
        }
        
        // checkers game mode..
        else if(gameMode == 2)
        {
            UpdateTileCount();

            // pressed all tiles..
            if (pressedTiles == totalTiles)
            {
                GameOver(false); // game won
                return;
            }
            // tiles are not all pressed..
            else
            {
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 2, (int)currentTile.transform.position.y].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 2 >= 0 && (int)currentTile.transform.position.y + 2 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 2, (int)currentTile.transform.position.y + 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.y + 2 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x, (int)currentTile.transform.position.y + 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 2 < LevelManager.xSizeValue && (int)currentTile.transform.position.y + 2 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 2, (int)currentTile.transform.position.y + 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 2 < LevelManager.xSizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 2, (int)currentTile.transform.position.y].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 2 < LevelManager.xSizeValue && (int)currentTile.transform.position.y - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 2, (int)currentTile.transform.position.y - 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.y - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x, (int)currentTile.transform.position.y - 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 2 >= 0 && (int)currentTile.transform.position.y - 2 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 2, (int)currentTile.transform.position.y - 2].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 3 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 3, (int)currentTile.transform.position.y].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 3 >= 0 && (int)currentTile.transform.position.y + 3 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 3, (int)currentTile.transform.position.y + 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.y + 3 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x, (int)currentTile.transform.position.y + 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 3 < LevelManager.xSizeValue && (int)currentTile.transform.position.y + 3 < LevelManager.ySizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 3, (int)currentTile.transform.position.y + 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 3 < LevelManager.xSizeValue)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 3, (int)currentTile.transform.position.y].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x + 3 < LevelManager.xSizeValue && (int)currentTile.transform.position.y - 3 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x + 3, (int)currentTile.transform.position.y - 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.y - 3 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x, (int)currentTile.transform.position.y - 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // tile space is in array range..
                if ((int)currentTile.transform.position.x - 3 >= 0 && (int)currentTile.transform.position.y - 3 >= 0)
                {
                    // tile space is OPEN
                    if (LevelManager.tileGrid[(int)currentTile.transform.position.x - 3, (int)currentTile.transform.position.y - 3].pressed == false)
                    {
                        tileOpen = true;
                    }
                }
                // no move remains
                if (tileOpen == false)
                {
                    GameOver(true);
                }
            }
        }

        // queen game mode..
        else if (gameMode == 3)
        {
            //Tile currentQueen;
            int ctrQ = 0;
            int xPosTemp = 1;
            int yPosTemp = 1;
            bool wonQueenGame = true;
            //currentQueen = queen[ctrQ];
            remainingQueens = totalQueens - placedQueens;                       // update remaining tiles
            pressedTilesText.text = "Pressed tiles: " + placedQueens;           // update pressed tiles text
            remainingTilesText.text = "Remaining tiles: " + remainingQueens;    // updated remaining tiles text

            // ctr is less than total queens and cycling thru
            while (ctrQ < totalQueens)
            {
                // queen is real..
                if (queen[ctrQ] != null)
                {
                    //LEFT

                    // x is within tile array..
                    while ((int)queen[ctrQ].transform.position.x - xPosTemp >= 0)
                    {
                        // tile space is pressed..
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x - xPosTemp, (int)queen[ctrQ].transform.position.y].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    // UP LEFT

                    // x and y are within tile array..
                    while ((int)queen[ctrQ].transform.position.x - xPosTemp >= 0 && (int)queen[ctrQ].transform.position.y + yPosTemp < LevelManager.ySizeValue)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x - xPosTemp, (int)queen[ctrQ].transform.position.y + yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                        yPosTemp++; // increment y position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    //UP

                    // y is within tile array..
                    while ((int)queen[ctrQ].transform.position.y + yPosTemp < LevelManager.ySizeValue)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x, (int)queen[ctrQ].transform.position.y + yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        yPosTemp++; // increment x position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    // UP RIGHT

                    // x and y are within tile array..
                    while ((int)queen[ctrQ].transform.position.x + xPosTemp < LevelManager.xSizeValue && (int)queen[ctrQ].transform.position.y + yPosTemp < LevelManager.ySizeValue)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x + xPosTemp, (int)queen[ctrQ].transform.position.y + yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                        yPosTemp++; // increment y position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    //LEFT

                    // x is within tile array..
                    while ((int)queen[ctrQ].transform.position.x + xPosTemp < LevelManager.xSizeValue)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x + xPosTemp, (int)queen[ctrQ].transform.position.y].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    // DOWN RIGHT

                    // x and y are within tile array..
                    while ((int)queen[ctrQ].transform.position.x + xPosTemp < LevelManager.xSizeValue && (int)queen[ctrQ].transform.position.y - yPosTemp >= 0)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x + xPosTemp, (int)queen[ctrQ].transform.position.y - yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                        yPosTemp++; // increment y position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    //UP

                    // y is within tile array..
                    while ((int)queen[ctrQ].transform.position.y - yPosTemp >= 0)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x, (int)queen[ctrQ].transform.position.y - yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        yPosTemp++; // increment x position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                    // DOWN LEFT

                    // x and y are within tile array..
                    while ((int)queen[ctrQ].transform.position.x - xPosTemp >= 0 && (int)queen[ctrQ].transform.position.y - yPosTemp >= 0)
                    {
                        // tile space is pressed
                        if (LevelManager.tileGrid[(int)queen[ctrQ].transform.position.x - xPosTemp, (int)queen[ctrQ].transform.position.y - yPosTemp].pressed == true)
                        {
                            wonQueenGame = false;
                        }
                        xPosTemp++; // increment x position
                        yPosTemp++; // increment y position
                    }
                    xPosTemp = 1;
                    yPosTemp = 1;
                }
                ctrQ++; // increment queen's array cycle
            }
            // won queen game and all queens are placed..
            if (wonQueenGame && placedQueens == totalQueens)
            {
                GameOver(false);
                return;
            }
        }
    }

    public void GameOver(bool lost)
    {
        particleCurrentTile.transform.gameObject.SetActive(false);
        if(gameMode == 1)
        {
            PlayerPrefs.SetInt("HighScoreKnights", pressedTiles);
        }
        if(gameMode == 2)
        {
            PlayerPrefs.SetInt("HighScoreCheckers", pressedTiles);
        }
        if(gameMode == 3)
        {
            PlayerPrefs.SetInt("HighScoreQueens", pressedTiles);
        }

        if (lost)
        {
            gameWon = false;
            gameLost = true;
        }
        else
        {
            gameWon = true;
            gameLost = false;
        }
    }

    // update UI tile counts
    public void UpdateTileCount()
    {
        remainingTiles = totalTiles - pressedTiles;                         // update remaining tiles
        pressedTilesText.text = "Pressed tiles: " + pressedTiles;           // update pressed tiles text
        remainingTilesText.text = "Remaining tiles: " + remainingTiles;     // updated remaining tiles text
    }

    // move particles to current tile
    public void MoveParticles()
    {
        particleCurrentTile.transform.gameObject.SetActive(false);                  // turn on particles
        particleCurrentTile.transform.position = currentTile.transform.position;    // set particles to current tile
        particleCurrentTile.transform.gameObject.SetActive(true);                   // turn on particles
    }

    // undo last action (limit one)
    public void Undo()
    {
        if(lastTile != null && currentTile != null)
        {
            currentTile.pressed = false;                                                // unpress current tile
            currentTile = lastTile;                                                     // set current tile to last tile
            lastTile = null;                                                            // set last tile to null to prevent infinite undos
            MoveParticles();                                                            // set particles to current tile
            pressedTiles--;                                                             // decrement pressed tiles
            UpdateTileCount();                                                          // update UI tile counts
            undoButton.interactable = false;                                            // disable additional presses
        }
    }

    // go to main menu
    public void MainMenuReturn()
    {
        SceneManager.LoadScene(0);
    }
}
