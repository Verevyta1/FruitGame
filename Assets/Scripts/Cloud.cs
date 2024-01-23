using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject spriteToMove; // Assign your sprite GameObject here in the editor
    public Camera mainCamera;

    public float minXPos = -5f;
    public float maxXPos = 5f;

    private void Update()
    {
        // Convert mouse position to world position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);

        // Clamp the x position to ensure the sprite stays within the specified range
        float clampedX = Mathf.Clamp(worldPosition.x, minXPos, maxXPos);

        // Set the sprite's position, maintaining its current y and z positions
        spriteToMove.transform.position = new Vector3(clampedX, spriteToMove.transform.position.y, spriteToMove.transform.position.z);

    }
}

