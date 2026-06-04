// using UnityEngine;

// public class GhostTowerVisualizer : MonoBehaviour
// {
//     [Header("References")]
//     [SerializeField] private SpriteRenderer rangeSprite; // blue
//     [SerializeField] private SpriteRenderer gridSprite;  // red

//     private float rangeSpriteWorldWidth;
//     private Vector2 gridSize;

//     void Awake()
//     {
//         if (rangeSprite == null || gridSprite == null)
//         {
//             Debug.LogError("GhostTowerVisualizer references missing!");
//             enabled = false;
//             return;
//         }

//         // Calculate sprite width in world units
//         rangeSpriteWorldWidth =
//             rangeSprite.sprite.rect.width /
//             rangeSprite.sprite.pixelsPerUnit;

//         gridSprite.enabled = false; // start hidden
//     }

//     public void Initialize(TurretData data)
//     {
//         gridSize = data.towerGrid;

//         SetRange(data.range);
//     }

//     public void SetRange(float range)
//     {
//         float diameter = range * 2f;
//         float scale = diameter / rangeSpriteWorldWidth;

//         Vector3 scaleVec = new Vector3(scale, scale, 1f);

//         rangeSprite.transform.localScale = scaleVec;
//         gridSprite.transform.localScale = scaleVec;
//     }

//     // THIS is what PlacementManager calls
//     public void SetPlacementValid(bool valid)
//     {
//         rangeSprite.enabled = valid;
//         gridSprite.enabled = !valid;
//     }

//     void OnDrawGizmos()
//     {
//         Gizmos.color = Color.green;

//         Gizmos.DrawWireCube(
//             transform.position,
//             gridSize
//         );
//     }
// }