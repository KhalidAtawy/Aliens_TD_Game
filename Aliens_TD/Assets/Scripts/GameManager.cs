using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    // will store a reference to the tower button we clicked
    public TowerBtn ClickedBtn { get; set; }

    private GameObject tempTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    /// <summary>
    /// called when we click on the tower button, and get a reference to the button that's clicked
    /// and enables the hover icon by calling the activate function and pass the icon for the tower from the towerBtn.sprite
    /// </summary>
    /// <param name="towerBtn"></param>
    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }


    /// <summary>
    /// if we didn't do this every time we press on the tile a tower will be built
    /// </summary>
    public void BuyTower()
    {
        Hover.Instance.Deactivate();
    }

    private void HandleEscape()
    {
#if UNITY_EDITOR_WIN
        //for PC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
#endif
    }



}
