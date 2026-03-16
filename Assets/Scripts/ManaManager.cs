using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private float mana;


    private void Awake()
    {
        mana = 0;
    }

    private void Start()
    {
        InvokeRepeating(nameof(ManaUpdate), 5.0f, 5.0f);
    }

    private void Update()
    {
        manaText.text = $"Mana: {mana}";
    }


    void ManaUpdate()
    {
        Debug.Log("Charging...");
        mana += 5;
        Debug.Log("Mana Charged!");
    }

}
