using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    private int maxFruitsAllowed = 4;
    private HashSet<GameObject> fruitsInBoundary = new HashSet<GameObject>();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruit")) // Make sure your fruits have the tag "Fruit"
        {
            fruitsInBoundary.Add(other.gameObject);

            if (fruitsInBoundary.Count >= maxFruitsAllowed)
            {
                Debug.Log("Game Over! Too many fruits are touching the boundary.");
                Application.Quit();

                // If you're running in the Unity editor, you'd want to use the following line:
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Fruit")) // Ensure your fruits have the tag "Fruit"
        {
            fruitsInBoundary.Remove(other.gameObject);
        }
    }

    
}