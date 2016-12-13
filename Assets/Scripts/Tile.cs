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

    public void Start()
    {
        //
        gameManager = FindObjectOfType<GameManager>();              // find game manager
        tileScript = GetComponent<Tile>();                          // get gameObject Tile script
        pressedTile = transform.FindChild("pressed").gameObject;    // find child pressed GO
        pressed = false;                                            // set as unpressed

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

   /* void OnMouseDown()
    {
        TileClicked();
    }*/


    public void TileClicked()
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

    private bool CheckMovement()
    {
        bool found = false;
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
        if(found)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
