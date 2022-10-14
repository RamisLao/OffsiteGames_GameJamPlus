
[System.Flags]
public enum RaiseFlag
{
    Awake = 1 << 1,
    OnEnable = 1 << 2,
    Start = 1 << 3,
    FixedUpdate = 1 << 4,
    Update = 1 << 5,
    LateUpdate = 1 << 6,
    OnDisable = 1 << 7,
    OnDestroy = 1 << 8,
    None = 1 << 9
}
