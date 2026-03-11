using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    private void Awake()
    {
        if (!ability)
        {
            Debug.Log("No Ability on " + gameObject.name);
            enabled = false;
            return;
        }
    }


    enum AbilityState { ready, active, cooldown }

    AbilityState state = AbilityState.ready;


    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                ability.Activate(transform.position);
                state = AbilityState.active;
                activeTime = ability.activeTime;

                if (ability.effectPrefab)
                    Instantiate(ability.effectPrefab, transform.position, Quaternion.identity);

                break;
            case AbilityState.active:
                if (activeTime > 0)
                { activeTime -= Time.deltaTime; } else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:

                if (cooldownTime > 0)
                { cooldownTime -= Time.deltaTime; } else
                {
                    state = AbilityState.ready;
                }

                break;
        }

    }
}
