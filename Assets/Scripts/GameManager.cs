using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private Vector3 tempTouch;

    public int gameMode;
    public bool gameWon;
    public bool gameLost;

    public int totalTiles;       // each tile increments this value on start
    public int pressedTiles;     // incremented on valid tile click
    public int remainingTiles;   // unpressed tiles. updated in checkwincondtion()

    public Tile currentTile;

	// Use this for initialization
	void Start ()
    {
        gameMode = 1;
        gameWon = false;
        gameLost = false;
        pressedTiles = 0;
        //totalTileCtr = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.touchCount > 0)
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

    public void CheckWinCondition()
    {
        // knight game mode..
        if (gameMode == 1)
        {
            remainingTiles = totalTiles - pressedTiles;
            bool tileOpen = false;  // tile open check temp var
            // pressed all tiles..
            if (pressedTiles == totalTiles)
            {
                gameWon = true; // game won
            }
            // game over sequence
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

                // no open tiles..
                if (!tileOpen)
                {
                    gameLost = true;    // game over
                    Debug.Log("game over");
                }
            }
        }
        
    }
}
