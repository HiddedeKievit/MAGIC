using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public bool CheatsOn;
    public ManaManager manaManager;
    public GameObject Zombie;
    public GameObject Bat;
    public Transform spawnPoint;
    public PathManager Path;

    void Start()
    {
        CheatsOn = false;
    }

    public void CheatEnabler()
    {
        CheatsOn = true;
    }

    void Update()
    {
        if (CheatsOn == true)
        {
            manaManager.mana = 9999999;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Zombie = Instantiate(Zombie, spawnPoint.position, Quaternion.identity);
                Debug.Log("ZOMBIE SPAWNED");

                PathFollower pf = Zombie.AddComponent<PathFollower>();
                pf.Path = Path;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Bat = Instantiate(Bat, spawnPoint.position, Quaternion.identity);
                Debug.Log("BAT SPAWNED");

                PathFollower pf = Bat.AddComponent<PathFollower>();
                pf.Path = Path;
            }

        }

        
    }
}
