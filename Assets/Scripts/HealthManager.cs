using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthtext;
    public float Health = 12;

    private deathscreen _deathscreen;
    private bool _isDead = false;

    void Start()
    {
        _deathscreen = Object.FindFirstObjectByType<deathscreen>();
    }

    private void Update()
    {
        healthtext.text = $"{Health}";

        if (Health <= 0 && !_isDead)
        {
            _isDead = true;
            _deathscreen?.StartDeath();
        }
    }
}
