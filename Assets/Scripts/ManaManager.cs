using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public CheatManager cheatManager;
    public static ManaManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private float textPopScale = 1.15f;
    [SerializeField] private float popSpeed = 10f;

    [Header("Mana Settings")]
    public float mana = 0;
    [SerializeField] private float manaCharge = 5f;
    [SerializeField] private float startCharge = 1f;
    [SerializeField] private float repeatCharge = 2f;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaLimit = 30f;
    [SerializeField] private float manaCheats = 9999999f;

    private Vector3 originalTextScale;
    private Vector3 targetTextScale;

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
        if (manaText != null)
        {
            originalTextScale = manaText.rectTransform.localScale;
            targetTextScale = originalTextScale;
        }

        InvokeRepeating(nameof(AddManaOverTime), startCharge, repeatCharge);
        UpdateUI();
        maxMana = manaLimit;
        Debug.Log("maxMana is now manaLimit");
    }

    void Update()
    {
        if (manaText != null)
        {
            manaText.rectTransform.localScale = Vector3.Lerp(
                manaText.rectTransform.localScale,
                targetTextScale,
                Time.deltaTime * popSpeed
            );
        }

        if (cheatManager.CheatsOn == true)
        {
            Debug.Log("CHEATS ENABLED");
            maxMana = manaCheats;
            Debug.Log("maxMana is now INFINTE RAAAAA");

        }
    }

    void UpdateUI()
    {
        if (manaText != null)
            manaText.text = Mathf.FloorToInt(mana).ToString();
    }

    void PopText()
    {
        if (manaText == null)
            return;

        manaText.rectTransform.localScale = originalTextScale * textPopScale;
        targetTextScale = originalTextScale;
    }

    void AddManaOverTime()
    {
        if (mana >= maxMana)
            return;
        mana += manaCharge;
        UpdateUI();
        PopText();

    }

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
        PopText();
        return true;
    }

    public void AddMana(float amount)
    {
        mana += amount;
        UpdateUI();
        PopText();
    }

    public float GetMana()
    {
        return mana;
    }
}