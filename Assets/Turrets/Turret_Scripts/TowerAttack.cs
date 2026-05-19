// using UnityEngine;

// [RequireComponent(typeof(Tower))]
// public class TowerAttack : MonoBehaviour
// {
//     private Tower tower;

//     void Awake()
//     {
//         tower = GetComponent<Tower>();
//     }

//     public void Fire(Transform firePoint, Transform target)
//     {
//         GameObject projectile = Instantiate(
//             tower.Data.projectilePrefab,
//             firePoint.position,
//             firePoint.rotation
//         );

//         Projectile projectileScript = projectile.GetComponent<Projectile>();
//         if (projectileScript != null)
//         {
//             projectileScript.Initialize(
//             target,
//             tower.CurrentDamage,
//             tower.Data.projectileSpeed
// );
//         }
//     }
// }