using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Tile blankTile;

    // block placement
    [SerializeField] private int totalTileCount = 1;    // all blocks total number. Must = number of manually PLACED blocks
    public static int ctrTileCount = 0;                 // ctr for blocks
    public static bool blocksPlaced = false;            // all blocks placed
    public static bool isInitializedTiles = false;      // empty tiles initialzed

    // arrays
    private int xCtr = 0;               // array ctr x
    private int yCtr = 0;               // array ctr y
    public static int xSizeValue = 6;  // array size x / equals screen length -1
    public static int ySizeValue = 6;  // array size y / equals screen length -1
    public static Tile[,] tileGrid = new Tile[xSizeValue, ySizeValue];


    void Start()
    {

    }


    void Update()
    {
        // blocks are all placed..
        if (ctrTileCount >= totalTileCount)
        {
            blocksPlaced = true;
        }
        // blocks are not all placed yet..
        if (blocksPlaced && !isInitializedTiles)
        {
            FillEmptySpace();               // fill empty tile spaces
            isInitializedTiles = true;      // all tiles are ready for gameplay
        }
    }

    private void FillEmptySpace()
    {
        // y values
        while (yCtr < ySizeValue)
        {
            // x values
            if (xCtr < xSizeValue)
            {
                // tile space is empty..
                if (tileGrid[xCtr, yCtr] == null)
                {
                    // place empty tile
                    tileGrid[xCtr, yCtr] = Instantiate(blankTile, new Vector3(xCtr, yCtr, 0), transform.rotation) as Tile;
                    // put under Level Manager GO for organization
                    tileGrid[xCtr, yCtr].transform.parent = transform;
                    // print position of blank placement
                    Debug.Log("created at X:" + xCtr + " Y:" + yCtr);
                }
                
                // increment x placement ctr
                xCtr++;
                // x value is maxed on screen..
                if (xCtr >= xSizeValue)
                {
                    xCtr = 0;
                    // increment y placement ctr
                    yCtr++;
                }
            }
        }
    }
}