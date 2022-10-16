using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectResolver : MonoBehaviour
{
    [Title("Listening on")]
    [SerializeField] private ApplyCardEffectEventChannelSO _eventApplyCardEffect;
    [SerializeField] private ApplyAbsorbEffectEventChannelSO _eventAbsorbEffect;

    private void Awake()
    {
        _eventApplyCardEffect.OnEventRaised += ApplyEffect;
        _eventAbsorbEffect.OnEventRaised += ApplyAbsorb;
    }

    private void ApplyAbsorb(CardData cardData, Agent agentFrom, Agent agentTo)
    {
        if (agentFrom.TryGetComponent(out Health healthFrom))
        {
            healthFrom.AddHealth(cardData.AbsorbAmount);
        }
        if (agentTo.TryGetComponent(out Health healthTo))
        {
            healthTo.SubtractHealth(cardData.AbsorbAmount);
        }
    }

    private void ApplyEffect(CardData cardData, Agent agent)
    {
        if (cardData.AppliesBlock) AddBlockPoints(cardData, agent);
        if (cardData.AppliesSappling) AddSapplings(cardData, agent);
        if (cardData.AppliesStun) ApplyStun(cardData, agent);
        if (cardData.AppliesExposed) ApplyExposed(cardData, agent);
        if (cardData.AppliesProtected) ApplyProtected(cardData, agent);
        if (cardData.AppliesDamage) ApplyDamage(cardData, agent);
        if (cardData.AppliesHeal) ApplyHeal(cardData, agent);
    }

    private void AddBlockPoints(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.AddBlockPoints(cardData.BlockAmount);
        }
    }

    private void AddSapplings(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.AddSapplings(cardData.SapplingAmount);
        }
    }

    private void ApplyStun(CardData cardData, Agent agent)
    {
        agent.ApplyStun();
    }

    private void ApplyExposed(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.AddExposedPoints(cardData.ExposedAmount);
        }
    }

    private void ApplyProtected(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.AddProtectedPoints(cardData.ProtectedAmount);
        }
    }

    private void ApplyDamage(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.SubtractHealth(cardData.DamageAmount);
        }
    }

    private void ApplyHeal(CardData cardData, Agent agent)
    {
        if (agent.TryGetComponent(out Health health))
        {
            health.AddHealth(cardData.HealAmount);
        }
    }
}
