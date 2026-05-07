using System.Collections;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthtext;
    public float Health = 12;

    [Header("Damage Feedback")]
    [SerializeField] private float bounceScale = 1.35f;
    [SerializeField] private float bounceDuration = 0.25f;
    [SerializeField] private Color damageColor = Color.red;

    private deathscreen _deathscreen;
    private bool _isDead = false;

    private float _lastHealth;
    private Vector3 _baseScale;
    private Color _baseColor;
    private Coroutine _feedbackRoutine;

    void Start()
    {
        _deathscreen = Object.FindFirstObjectByType<deathscreen>();
        _lastHealth = Health;
        _baseScale = healthtext.transform.localScale;
        _baseColor = healthtext.color;
    }

    private void Update()
    {
        healthtext.text = $"{Health}";

        // Detect damage (health dropped since last frame)
        if (Health < _lastHealth)
        {
            if (_feedbackRoutine != null) StopCoroutine(_feedbackRoutine);
            _feedbackRoutine = StartCoroutine(DamageFeedback());
        }
        _lastHealth = Health;

        if (Health <= 0 && !_isDead)
        {
            _isDead = true;
            _deathscreen?.StartDeath();
        }
    }

    private IEnumerator DamageFeedback()
    {
        Vector3 bigScale = _baseScale * bounceScale;

        // Pop + turn red
        float t = 0f;
        float popTime = bounceDuration * 0.4f;
        while (t < popTime)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / popTime);
            healthtext.transform.localScale = Vector3.Lerp(_baseScale, bigScale, k);
            healthtext.color = Color.Lerp(_baseColor, damageColor, k);
            yield return null;
        }

        // Ease back
        t = 0f;
        float easeTime = bounceDuration - popTime;
        while (t < easeTime)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / easeTime);
            healthtext.transform.localScale = Vector3.Lerp(bigScale, _baseScale, k);
            healthtext.color = Color.Lerp(damageColor, _baseColor, k);
            yield return null;
        }

        healthtext.transform.localScale = _baseScale;
        healthtext.color = _baseColor;
        _feedbackRoutine = null;
    }
}
