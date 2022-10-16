using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(string name)
    {
        _animator?.SetTrigger(name);
    }

    public void SetBoolToTrue(string name)
    {
        _animator?.SetBool(name, true);
    }

    public void SetBoolToFalse(string name)
    {
        _animator?.SetBool(name, false);
    }
}
