using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] keyObjects;
    // Assign all your key GameObjects here in the Inspector
    // e.g., 5 objects for Hard, or 6 if you want a new Nightmare difficulty

    void Start()
    {
        // e.g. 3 (Easy), 4 (Medium), 5 (Hard), 6 (Nightmare), etc.
        int totalNeeded = DifficultyManager.GetKeysNeeded();

        // How many key objects did we actually assign?
        int maxKeys = keyObjects.Length;

        // If totalNeeded is more than we have, clamp it (or handle error as you prefer)
        if (totalNeeded > maxKeys)
        {
            Debug.LogWarning($"Not enough key objects to satisfy {totalNeeded} keys needed. We only have {maxKeys} assigned. Clamping to {maxKeys}.");
            totalNeeded = maxKeys;
        }

        // If we need exactly as many as assigned, just leave them all active
        if (totalNeeded == maxKeys)
        {
            // But make sure they're all enabled in case you had them disabled by default
            for (int i = 0; i < maxKeys; i++)
            {
                keyObjects[i].SetActive(true);
            }
            return;
        }

        // Otherwise, we need to disable (maxKeys - totalNeeded) random keys
        int toDisable = maxKeys - totalNeeded;

        // 1) Enable all first
        for (int i = 0; i < maxKeys; i++)
        {
            keyObjects[i].SetActive(true);
        }

        // 2) Create an index array [0..maxKeys-1]
        int[] indices = new int[maxKeys];
        for (int i = 0; i < maxKeys; i++)
        {
            indices[i] = i;
        }

        // 3) Shuffle the indices randomly
        ShuffleArray(indices);

        // 4) Disable the first 'toDisable' of them
        for (int i = 0; i < toDisable; i++)
        {
            keyObjects[indices[i]].SetActive(false);
        }
    }

    // Simple Fisher-Yates shuffle
    private void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
