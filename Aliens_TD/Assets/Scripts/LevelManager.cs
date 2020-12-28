using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    //the parent gameobject for the tiles
    [SerializeField]
    private Transform map;


    private Point startPortalSpawn, endPortalSpawn; //blueSpawn,redSpawn

    [SerializeField]
    private GameObject startPortalPrefab, endPortalPrefab; // bluePortal, redPortal

    //takes point and returns the tile in that point
    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        // Calculates how big our tiles are, this is used to place out tiles on the correct positions
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Creates our level
    /// </summary>
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        // will carry all the data for the map that will be used in placing tiles "contains the index of each tile"
        string[] mapData = ReadLevelText();

        //Calculates the x map size
        int mapX = mapData[0].ToCharArray().Length; // we take the first index as all "raws" have the same length
        
        //Calculates the y map size
        int mapY = mapData.Length; // we need to get the length of all the mapData "number of coloumns in the mapData"

        Vector3 maxTile = Vector3.zero;

        //Calculate the world start point, this is the  topLeft corner of the screen (we used the screen.height because the Y axis is "flipped").
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++) //the y positions
        {
            char[] newTiles = mapData[y].ToCharArray(); //Gets all the tiles, that we need to place on the world.
            for (int x = 0; x < mapX; x++) // the x positions
            {
                //Place the tiles in the world
                //newTiles[x] is the first row of mapData which is string ex: {"0110"} and we converted it to char array to be able to access it as index like{"0","1","1","0"}
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        //The maxTile is the last index in the Tiles dictionary "maxX - 1 and maxY - 1"
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        //Sets the camera limits to the max tile position
        cameraMovement.SetLimits(new Vector3(maxTile .x + TileSize, maxTile.y - TileSize));

        //spawn the start and end portals
        SpawnPortals();
    }

    /// <summary>
    /// Places a tile in the gameWorld
    /// </summary>
    /// <param name="tileType">The type of the tile to place for example 0</param>
    /// <param name="x">x position of the tile</param>
    /// <param name="y">y position of the tile</param>
    /// <param name="worldStart">the world start position</param>
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        //Parses the tileType to an int, so that we can use it as an indexer when we create a new tile.
        int tileIndex = int.Parse(tileType);

        //create the new tile and makes a reference to that tile in the newTile variable
        //so this gets the component of this tile script
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        //use the new tile variable to change the position of the tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

    }


    // read the data from the text file and return it as a string array
    private string[] ReadLevelText()
    {
        // read all the text file in bindData
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        // convert all the text into a oneline string by removing all the newlines
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        //return the data as a string by spliting it at the -
        return data.Split('-');
    }


    // this function will spawn the portals in our game
    private void SpawnPortals()
    {
        startPortalSpawn = new Point(0, 0); //Location of the start portal on the grid


        // we used GetComponent<TileScript>().WorldPosition to get the center of the tile correct
        Instantiate(startPortalPrefab, Tiles[startPortalSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity); //create the start portal


        endPortalSpawn = new Point(11, 6); //Location of the end portal

        Instantiate(endPortalPrefab, Tiles[endPortalSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity); //create the end portal
    }
}
