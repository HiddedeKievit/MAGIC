using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;

    // public reference so all grids use the same cell size
    public int GridCellSize = 2;

    private void Awake()
    {
        if (Instance)
            Debug.LogWarning("double LevelData instance detected");

        Instance = this;
    }
}

