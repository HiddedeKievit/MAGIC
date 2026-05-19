// using UnityEngine;

// [RequireComponent(typeof(TowerTargeting))]
// [RequireComponent(typeof(TowerSelection))]
// public class TowerRangeVisualizer : MonoBehaviour
// {
//     [SerializeField] private SpriteRenderer rangeSprite;

//     private TowerTargeting targeting;
//     private TowerSelection selection;

//     private float spriteOriginalWidth;

//     public void RefreshRange()
// {
//     UpdateRangeVisual();
// }

//     void Awake()
//     {
//         targeting = GetComponent<TowerTargeting>();
//         selection = GetComponent<TowerSelection>();

//         if (rangeSprite == null)
//         {
//             Debug.LogError("Range sprite not assigned on " + name);
//             enabled = false;
//             return;
//         }

//         rangeSprite.transform.localPosition = Vector3.zero;
//         rangeSprite.enabled = false;

//         spriteOriginalWidth = rangeSprite.bounds.size.x;

//         if (spriteOriginalWidth <= 0f)
//         {
//             Debug.LogError("Range sprite width is zero!");
//             enabled = false;
//         }
//     }

//     void Start()
//     {
//         UpdateRangeVisual();
//     }

//     void Update()
//     {
//         rangeSprite.enabled = selection.IsSelected;
//     }

//     void UpdateRangeVisual()
//     {
//         float diameter = targeting.Range * 2f;
//         float scale = diameter / spriteOriginalWidth;
//         rangeSprite.transform.localScale = new Vector3(scale, scale, 1f);
//     }

//     void LateUpdate()
//     {
//         rangeSprite.transform.rotation = Quaternion.identity;
//     }
// }