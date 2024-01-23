using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum FruitType
{
    Fruit1,
    Fruit2,
    Fruit3,
    Fruit4,
    Fruit5,
    Fruit6,
    Fruit7,
    Fruit8,
    Fruit9,
    Fruit10,
    Fruit11
}


public class Fruit : MonoBehaviour
{
    public FruitType fruitType;
    private bool isReadyToTransform = false;
    private bool hasInitiatedCollision = false;

    public AudioClip hitSound; // Assign this in the Unity Editor
    private AudioSource audioSource;
    private bool hasCollided = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        // Start the coroutine to enable transformation after a short delay
        StartCoroutine(EnableTransformationAfterDelay(0.01f)); // 0.1 seconds delay
    }
    private IEnumerator EnableTransformationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReadyToTransform = true;
    }
    private void MergeFruits(Fruit otherFruit)
    {
        Vector3 midpoint = (this.transform.position + otherFruit.transform.position) / 2;
        FruitType nextFruitType = this.fruitType + 1;
        if (nextFruitType > FruitType.Fruit11)
        {
            Destroy(this.gameObject);
            Destroy(otherFruit.gameObject);
            return;
        }

        GameObject nextFruitPrefab = FruitManager.Instance.GetFruitPrefab(nextFruitType);
        GameObject newFruit = Instantiate(nextFruitPrefab, midpoint, Quaternion.identity);
        Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = new Vector2(0, -4);
            float forceMagnitude = 6.0f;
            rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode2D.Impulse);
        }

        Destroy(this.gameObject);
        Destroy(otherFruit.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if this is the first collision
        if (!hasCollided)
        {
            // Play the hit sound
            audioSource.PlayOneShot(hitSound);

            // Set flag to true so sound is not played on subsequent collisions
            hasCollided = true;
        }

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
        if (otherFruit != null && this.fruitType == otherFruit.fruitType && isReadyToTransform && otherFruit.isReadyToTransform)
        {
            if (!hasInitiatedCollision && !otherFruit.hasInitiatedCollision)
            {

                MergeFruits(otherFruit);
                hasInitiatedCollision = true;
                otherFruit.hasInitiatedCollision = true;
                CheckAndHandleSecondaryCollision(this.gameObject);
                ScoreManager.Instance.AddScore(50);
            }
        }
    }

    private void CheckAndHandleSecondaryCollision(GameObject newFruit)
    {
        Collider2D newFruitCollider = newFruit.GetComponent<Collider2D>();
        if (newFruitCollider != null)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(newFruitCollider.bounds.center, newFruitCollider.bounds.size, 0f);
            foreach (var hit in hits)
            {
                Fruit hitFruit = hit.GetComponent<Fruit>();
                if (hit != newFruitCollider && hitFruit != null && hitFruit.fruitType == this.fruitType && hitFruit.isReadyToTransform && this.isReadyToTransform)
                {
                    MergeFruits(hitFruit);
                }
            }
        }
    }

}