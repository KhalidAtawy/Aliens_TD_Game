using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// this script is used for all the tiles in the game.
/// </summary>
public class TileScript : MonoBehaviour
{
    /// <summary>
    /// the tiles grid position
    /// </summary>
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255, 118, 118, 225);

    private Color32 emptyColor = new Color32(96, 225, 90, 225);

    private SpriteRenderer spriteRenderer;


    /// <summary>
    ///The tile's center world position.
    ///Calculates and returns the center point of each tile.
    /// </summary>
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="gridPos"> x,y created in our point class</param>
   /// <param name="worldPos"> actual location</param>
   /// <param name="parent"> the parent for this tile</param>
    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        
        // used in path finidng for example to know if it's empty 
        this.GridPosition = gridPos;

        // set the tile position
        transform.position = worldPos;

        transform.SetParent(parent);
        // every single tile will be added in the Tiles dictionary
        // so we can access each tile by it's gridPos (x,y)
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }


    /// <summary>
    /// MouseOver, this is execute when the player mouse over the tile
    /// </summary>
    Touch touch;
    private void OnMouseOver()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("mouse is ....");
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && GameManager.Instance.ClickedBtn != null)
                { TileIsEmptyCheck(); }
            }
        }
#endif

#if UNITY_EDITOR_WIN
        //check if our mouse is not over any UI elements
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            TileIsEmptyCheck();
        }
#endif

    }

    private void OnMouseExit()
    {

        ColorTile(Color.white);
    }


    /// <summary>
    /// places tower on the tile
    /// </summary>
    private void PlaceTower()
    {
        Debug.Log(GridPosition.X + "," + GridPosition.Y);
        // spawn a tower then store it in tht tower gameObject so we can access it later
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

        // to make the tower which is in higher Y always on top 
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        // the parent here will be the current tile we are in, so we simply pass the transform to reference the current tile.
        tower.transform.SetParent(transform);

        IsEmpty = false;
        ColorTile(Color.white);
         
        GameManager.Instance.BuyTower();
    }
/*
    private void PlaceTower()
    {

        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

        // to make the tower which is in lower Y always on top 
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        IsEmpty = false;
        ColorTile(Color.white);

        GameManager.Instance.BuyTower();
    }
*/

    /// <summary>
    /// function used to check if the tile is empty or not
    /// </summary>
    private void TileIsEmptyCheck()
    {
        if (IsEmpty)
        {

            ColorTile(emptyColor);
            // replaced with another else if
            // if (Input.GetMouseButtonDown(0))
            // {
            //     PlaceTower();
            // }
        }

        if (!IsEmpty)
        {
            ColorTile(fullColor);
        }
        // it's the else for the previous if statement "if(!IsEmpty)"
        else if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }
    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
