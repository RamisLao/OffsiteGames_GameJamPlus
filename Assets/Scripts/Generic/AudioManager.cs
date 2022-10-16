using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Title("Audio Settings")]
    [SerializeField] private AudioSource _masterAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;

    [Title("Overworld Clips")]
    [SerializeField] private AudioClip _overworldClip;
    [SerializeField] private AudioClip _battleClip;

    [Title("Battle Clips")]

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombatActivated;
    public VoidEventChannelSO _eventOnCombatDeactivated;

    private void Start()
    {
        _eventOnCombatActivated.OnEventRaised += BattleMusic;
        _eventOnCombatDeactivated.OnEventRaised += OverworldMusic;
    }

    private void OverworldMusic()
    {
        _masterAudioSource.clip = _overworldClip;
        _masterAudioSource.Play();
    }

    private void BattleMusic()
    {
        _masterAudioSource.clip = _battleClip;
        _masterAudioSource.Play();
    }
}
