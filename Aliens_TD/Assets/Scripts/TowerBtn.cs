using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;


    // create property to be able to read it in other classes
    public GameObject TowerPrefab
    {
        get { return towerPrefab; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }
}
