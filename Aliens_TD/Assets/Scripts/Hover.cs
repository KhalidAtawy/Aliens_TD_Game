using UnityEngine;

public class Hover : Singleton<Hover>
{

    private SpriteRenderer spriteRenderer;

    // this variable is used to check on the enable trigger activate/deactivate swipe for touch in cameraMovement
    public SpriteRenderer Sprite_Renderer
    {
        get { return spriteRenderer; }
    }
    // Start is called before the first frame update
    void Start()
    {
        // get a reference to the spriteRenderer for the current tower button
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    // make the tower icon follow the mouse by passing the mouse position to the icon
    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0); // set the Z 0 because the default value was -10
        }
        
    }


    //enables the hover icon when we click the tower button ,called in the gameManager
    public void Activate(Sprite sprite)
    {
        spriteRenderer.enabled = true;

        this.spriteRenderer.sprite = sprite;
    }


    // deactivate the hover icon and clear the clickedBtn so that we can't place a tower after deactivate is called.
    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;
    }
}
