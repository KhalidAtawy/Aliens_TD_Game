using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;


    private void OnMouseDown()
    {
        tower.SetActive(false);
        Destroy(tower);
    }
}
