using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    // var
    public bool pressed;            // tile space is pressed or not
    public bool blankTile;          // if tile is blank

    // get this stuff
    public Tile tileScript;         // gameObject tile script
    private GameObject pressedTile; // pressed tile GO
    private GameManager gameManager;

    // queens
    private int ctrQueenPersonal;

    public void Start()
    {
        //
        gameManager = FindObjectOfType<GameManager>();              // find game manager
        tileScript = GetComponent<Tile>();                          // get gameObject Tile script
        pressedTile = transform.FindChild("pressed").gameObject;    // find child pressed GO
        pressed = false;                                            // set as unpressed
        ctrQueenPersonal = 0;

        // game management
        gameManager.totalTiles++;

        // tile grid
        LevelManager.tileGrid[(int)this.transform.position.x, (int)this.transform.position.y] = this;  // place tile in array according to position
        LevelManager.ctrTileCount++;                                                                   // increment tile ctr
    }

    public void Update()
    {
        // is pressed..
        if(pressed)
        {
            pressedTile.SetActive(true);    // set to pressed sprite
        }
        // is NOT pressed..
        else
        {
            pressedTile.SetActive(false);   // set to unpressed sprite
        }

    }

    void OnMouseDown()
    {
        TileClicked();
    }


    public void TileClicked()
    {
        // game mode is NOT queens..
        if(gameManager.gameMode != 3)
        {
            // a current tile has already been set
            if (gameManager.currentTile != null)
            {
                // movement is legal
                if (CheckMovement())
                {
                    pressed = true;                             // set as pressed
                    gameManager.currentTile = tileScript;       // set as current tile
                    gameManager.pressedTiles++;                 // increment pressed tiles ctr
                    gameManager.CheckWinCondition();            // check for win or loss
                }
            }
            // nothing has been pressed yet SO IT'S LEGAL
            else
            {
                pressed = true;                         // set as pressed
                gameManager.currentTile = tileScript;   // set as current tile
                gameManager.pressedTiles++;             // increment pressed tiles ctr
                gameManager.CheckWinCondition();        // check for win or loss
            }
        }
        // game mode is queens..
        else
        {
            // this tile is pressed..
            if(pressed)
            {
                pressed = false;                                    // set this tile to no longer pressed
                gameManager.queen[ctrQueenPersonal] = null;         // remove from queens array
                gameManager.placedQueens--;                         // decrement number of placed queens
            }
            // not maxed queens..
            else if(gameManager.placedQueens < gameManager.totalQueens)
            {
                pressed = true;             // set this tile to pressed
                gameManager.placedQueens++; // increment number of placed queens
                
                // find empty place in queens array and add this tile there..
                for(int i = 0; i < gameManager.totalQueens; i++)
                {
                    if (gameManager.queen[i] == null)
                    {
                        gameManager.queen[i] = this.tileScript;
                        ctrQueenPersonal = i;
                        break;
                    }
                }
            }
            gameManager.CheckWinCondition();
        }
    }

    // checks to see if pressing this tile is valid movement
    // returns true if move is valid
    private bool CheckMovement()
    {
        bool found = false; // tile is a valid tile to press with respect to the previous tile
        
        // knight game mode
        if(gameManager.gameMode == 1)
        {
            // attempted placement is x +- 2
            if (transform.position.x == gameManager.currentTile.transform.position.x + 2 ||
                transform.position.x == gameManager.currentTile.transform.position.x - 2)
            {
                // attempted placement is y +- 1
                if (transform.position.y == gameManager.currentTile.transform.position.y + 1 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 1)
                {
                    if(LevelManager.tileGrid[(int)transform.position.x, (int)transform.position.y].pressed == false)
                    {
                        found = true;
                    }
                }
            }
            // attempted placement is x +- 1
            else if (transform.position.x == gameManager.currentTile.transform.position.x + 1 ||
                transform.position.x == gameManager.currentTile.transform.position.x - 1)
            {
                // attempted placement is y +- 2
                if (transform.position.y == gameManager.currentTile.transform.position.y + 2 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 2)
                {
                    if (LevelManager.tileGrid[(int)transform.position.x, (int)transform.position.y].pressed == false)
                    {
                        found = true;
                    }
                }
            }
        }

        // checkers game mode
        if (gameManager.gameMode == 2)
        {
            // attempted placement is x +- 2
            if (transform.position.x == gameManager.currentTile.transform.position.x + 2 ||
                transform.position.x == gameManager.currentTile.transform.position.x - 2)
            {
                // attempted placement is y or y +- 2
                if (transform.position.y == gameManager.currentTile.transform.position.y + 2 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 2 ||
                    transform.position.y == gameManager.currentTile.transform.position.y)
                {
                    if (LevelManager.tileGrid[(int)transform.position.x, (int)transform.position.y].pressed == false)
                    {
                        found = true;
                    }
                }
            }
            // attempted placement is transform.x
            else if (transform.position.x == gameManager.currentTile.transform.position.x)
            {
                // attempted placement is y +- 2
                if (transform.position.y == gameManager.currentTile.transform.position.y + 2 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 2 ||
                    transform.position.y == gameManager.currentTile.transform.position.y + 3 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 3)
                {
                    if (LevelManager.tileGrid[(int)transform.position.x, (int)transform.position.y].pressed == false)
                    {
                        found = true;
                    }
                }
            }
            // attempted placement is x +- 3
            else if (transform.position.x == gameManager.currentTile.transform.position.x + 3 ||
                     transform.position.x == gameManager.currentTile.transform.position.x - 3)
            {
                // attempted placement is y or y +- 3
                if (transform.position.y == gameManager.currentTile.transform.position.y + 3 ||
                    transform.position.y == gameManager.currentTile.transform.position.y - 3 ||
                    transform.position.y == gameManager.currentTile.transform.position.y)
                {
                    if (LevelManager.tileGrid[(int)transform.position.x, (int)transform.position.y].pressed == false)
                    {
                        found = true;
                    }
                }
            }
        }

        if (found)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
