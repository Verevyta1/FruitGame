using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;




public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Array of fruit prefabs
    public Camera mainCamera;
    public Image currentFruitUI; // UI element for current fruit
    public Image nextFruitUI; // UI element for next fruit
    private float cooldownTimer;

    public float minXPosition;
    public float maxXPosition;
    public float minYPosition;
    public float maxYPosition;

    private FruitType currentFruitToSpawn;
    private FruitType nextFruitToSpawn; // Store the next fruit to be spawned

    private void Start()
    {
        SetNextFruitToSpawn();
        UpdateFruitUI(); // Initial UI update
    }


    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) && cooldownTimer <= 0)
        {
            SpawnFruitOnMouseClick();
            SetNextFruitToSpawn(); // Determine the next fruit after spawning
            UpdateFruitUI();
            cooldownTimer = 0.9f;
        }
    }

    private void SetNextFruitToSpawn()
    {
        currentFruitToSpawn = nextFruitToSpawn; // Set the current fruit to the previously determined next fruit
        nextFruitToSpawn = (FruitType)UnityEngine.Random.Range(0, 5);
        Debug.Log("Current fruit to spawn: " + currentFruitToSpawn);
        Debug.Log("Next fruit to spawn: " + nextFruitToSpawn);
    }


    private void SpawnFruitOnMouseClick()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0; // Set the z distance from the camera
        Vector3 objectPos = mainCamera.ScreenToWorldPoint(mousePos);

        objectPos.x = Mathf.Clamp(objectPos.x, minXPosition, maxXPosition);
        objectPos.y = Mathf.Clamp(objectPos.y, minYPosition, maxYPosition);

        Instantiate(fruitPrefabs[(int)currentFruitToSpawn], objectPos, Quaternion.identity);
    }



    private void UpdateFruitUI()
    {
        if (currentFruitUI != null)
        {
            // Assuming your fruitPrefabs have a SpriteRenderer with the fruit image
            currentFruitUI.sprite = fruitPrefabs[(int)currentFruitToSpawn].GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Current fruit UI updated to: " + currentFruitToSpawn);

        }

        if (nextFruitUI != null)
        {
            nextFruitUI.sprite = fruitPrefabs[(int)nextFruitToSpawn].GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Next fruit UI updated to: " + nextFruitToSpawn);

        }
    }



}