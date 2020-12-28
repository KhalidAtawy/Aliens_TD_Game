using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Recommended value between 2 and 5.")]
    private float cameraSpeed = 0;

    [SerializeField]
    [Tooltip("Recommended value between 0.03 and 0.05.")]
    private float touchCameraSpeed = 0;

    private bool canSwipe;



    private float xMax;
    private float yMin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();

    }

    Touch touch;
    Vector3 startTouchPosition, endTouchPosition;

    //take the player's input and move the camera around
    private void GetInput()
    {
#if UNITY_EDITOR_WIN

        //move the camera upward when we press W
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }
#endif


#if UNITY_ANDROID
        // disable swiping if the player is holding a tower
        if (!Hover.Instance.Sprite_Renderer.enabled)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                transform.Translate(touch.deltaPosition * -touchCameraSpeed);

            }
        }
#endif
        // here we prevent the camera from being able to move by clamping the position on the X and the Y
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
    }




    public void SetLimits(Vector3 maxTile)
    {

        // the right bottom corner of the screen
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        // calculate how much we can move.
        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
    }
}
