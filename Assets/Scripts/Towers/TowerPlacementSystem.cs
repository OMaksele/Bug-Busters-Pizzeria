using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.Image;

public class TowerPlacementSystem : MonoBehaviour
{
    public Transform buyMenu;
    public Transform menuContent;
    public Transform placableGrid;
    public Transform placableGridHighlight;
    public Transform newGrid;

    private bool placeMode = false;
    private Transform placeObject;

    public Transform sleeperPrefab;
    public Transform throwerPrefab;
    public Transform walkerPrefab;


    public List<Vector2> occupiedPositions = new List<Vector2>();

    public UpgradeMechanics upgradeManager;

    public AudioClip uiButtonSound;
    public AudioClip uiClickSound;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && placeMode)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach(RaycastHit2D target in hit)
            {
                if (target.collider.gameObject.transform == placableGrid)
                {
                    TowerInstantiate();
                }
            }
        }   
    }

    public void BuyMenuToggle()
    {
        AudioManager.Play(uiButtonSound, 1f);
        buyMenu.gameObject.active = !buyMenu.gameObject.active;
    }

    public void PlaceSleeper()
    {
        PlaceModeToggle();
        placeObject = sleeperPrefab;
    }

    public void PlaceThrower()
    {
        PlaceModeToggle();
        placeObject = throwerPrefab;

    }

    public void PlaceWalker()
    {
        PlaceModeToggle();
        placeObject = walkerPrefab;
    }

    public void PlaceModeToggle()
    {
        AudioManager.Play(uiClickSound, 1f);
        placeMode = !placeMode;
        buyMenu.gameObject.active = !buyMenu.gameObject.active;
        placableGridHighlight.gameObject.active = !placableGridHighlight.gameObject.active; 
    }

    public void TowerInstantiate()
    {
        bool isLegal = true;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int cellPosition = newGrid.GetComponent<Tilemap>().WorldToCell(worldPosition);

        foreach(Vector2 pos in occupiedPositions)
        {
            print(Vector2.Distance(pos, (Vector2Int)cellPosition));
            if(Vector2.Distance(pos, (Vector2Int)cellPosition) < 4.5f)
            {
                isLegal = false;
            }
        }

        if (!isLegal) return;

        var cat = Instantiate(placeObject, cellPosition, Quaternion.identity);

        switch (placeObject)
        {
            case var value when value == sleeperPrefab:
                cat.GetComponent<SleepyCatMechanics>().upgradeManager = upgradeManager;
                break;
            case var value when value == throwerPrefab:
                cat.GetComponent<ThrowingCatMechanics>().upgradeManager = upgradeManager;
                break;
            case var value when value == walkerPrefab:
                cat.GetComponent<WalkingCatMechanics>().upgradeManager = upgradeManager;
                break;
        }


        occupiedPositions.Add((Vector2Int)cellPosition);
        placeObject = null;

        PlaceModeToggle();
    }

}
