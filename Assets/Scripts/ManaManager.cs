using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI manaText;
    public float mana;
    [SerializeField] private float manaCharge;
    [SerializeField] private float startCharge;
    [SerializeField] private float repeatCharge;


    private void Awake()
    {
        mana = 0;
    }

    private void Start()
    {
        InvokeRepeating(nameof(ManaUpdate), startCharge, repeatCharge);
    }

    private void Update()
    {
        manaText.text = $"Mana: {mana}";
    }


    void ManaUpdate()
    {
        Debug.Log("Charging...");
        mana += manaCharge;
        Debug.Log("Mana Charged!");
    }

}
