using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] protected bool _instantiateNew = false;

    private void Awake()
    {
        Reset();
    }

    protected float Remap(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }

    public virtual void Reset() { }

    public virtual void Play() { }

    public virtual void PlayWithDelay(float delay) { }

    public virtual void Stop() { }

    public virtual void Play(float duration)
    {
        StartCoroutine(PlayCoroutine(duration));
    }

    private IEnumerator PlayCoroutine(float duration)
    {
        Play();
        yield return new WaitForSeconds(duration);
        Stop();
    }

    public virtual void UpdateEffect(float value) { }

    public virtual void UpdateEffect(int value) { }
}
