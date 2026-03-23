using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public static ManaManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Mana Settings")]
    public float mana = 0;
    [SerializeField] private float manaCharge = 5f;
    [SerializeField] private float startCharge = 1f;
    [SerializeField] private float repeatCharge = 2f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(AddManaOverTime), startCharge, repeatCharge);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (manaText != null)
            manaText.text = $"Mana: {Mathf.FloorToInt(mana)}";
    }

    void AddManaOverTime()
    {
        mana += manaCharge;
        UpdateUI();
    }

    // =========================
    // PUBLIC API (IMPORTANT)
    // =========================

    public bool CanAfford(float cost)
    {
        return mana >= cost;
    }

    public bool SpendMana(float cost)
    {
        if (mana < cost)
            return false;

        mana -= cost;
        UpdateUI();
        return true;
    }

    public void AddMana(float amount)
    {
        mana += amount;
        UpdateUI();
    }

    public float GetMana()
    {
        return mana;
    }
}