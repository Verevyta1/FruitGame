using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance { get; private set; }

    public GameObject[] fruitPrefabs; // Array of fruit prefabs

    private void Awake()
    {
        // Ensure there's only one instance of FruitManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetFruitPrefab(FruitType type)
    {
        int index = (int)type;
        if (index >= 0 && index < fruitPrefabs.Length)
        {
            return fruitPrefabs[index];
        }
        else
        {
            Debug.LogError("Invalid FruitType: " + type);
            return null;
        }
    }
}