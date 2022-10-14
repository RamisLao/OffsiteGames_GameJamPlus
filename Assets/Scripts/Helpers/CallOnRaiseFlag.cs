using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CallOnRaiseFlag : MonoBehaviour
{
    [SerializeField] private RaiseFlag _raiseEvent = RaiseFlag.Awake;

    private void Awake()
    {
        if (HasEventFlag(RaiseFlag.Awake)) OnRaiseFlag();
    }

    private void OnEnable()
    {
        if (HasEventFlag(RaiseFlag.OnEnable)) OnRaiseFlag();
    }

    private void Start()
    {
        if (HasEventFlag(RaiseFlag.Start)) OnRaiseFlag();
    }

    private void FixedUpdate()
    {
        if (HasEventFlag(RaiseFlag.FixedUpdate)) OnRaiseFlag();
    }

    private void Update()
    {
        if (HasEventFlag(RaiseFlag.Update)) OnRaiseFlag();
    }

    private void LateUpdate()
    {
        if (HasEventFlag(RaiseFlag.LateUpdate)) OnRaiseFlag();
    }

    private void OnDisable()
    {
        if (HasEventFlag(RaiseFlag.OnDisable)) OnRaiseFlag();
    }

    private void OnDestroy()
    {
        if (HasEventFlag(RaiseFlag.OnDestroy)) OnRaiseFlag();
    }

    private bool HasEventFlag(RaiseFlag flag)
    {
        return ((_raiseEvent & flag) == flag);
    }

    protected abstract void OnRaiseFlag();
}
