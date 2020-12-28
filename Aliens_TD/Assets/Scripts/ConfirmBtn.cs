using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject CancelImage;


    private void OnMouseDown()
    {
        CancelImage.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
