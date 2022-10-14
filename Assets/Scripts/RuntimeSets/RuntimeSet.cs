using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IClearable : ScriptableObject
{
    public abstract void Clear();
}

public abstract class RuntimeSet<T> : IClearable where T : class
{
    [SerializeField] private RuntimeSet<T> _copyFrom;
    [Multiline(5)]
    [SerializeField] private string _description;
    [SerializeField] private bool _canContainDuplicates = false;
    [Space] [SerializeField] protected List<T> _items = new List<T>();
    public List<T> Items => _items;

    public UnityEvent OnChanged;

    public bool ListIsEmpty => _items.Count == 0;
    public int Count => _items.Count;

    [Button("Clear list")]
    private void ClearList()
    {
        Clear();
    }

    [Button("Copy From")]
    private void CopyFrom()
    {
        Clear();
        foreach(T t in _copyFrom.Items)
        {
            Add(t);
        }
    }

    public void Add(T t)
    {
        if (_canContainDuplicates || !_items.Contains(t))
        {
            _items.Add(t);
            OnChanged.Invoke();
        }
    }

    public void Remove(T t)
    {
        if (_items.Contains(t))
        {
            _items.Remove(t);
            OnChanged.Invoke();
        }
    }

    public bool Contains(T t)
    {
        return _items.Contains(t);
    }

    public T GetRandomItem()
    {
        if (Count > 0) return _items[Random.Range(0, _items.Count)];
        return null;
    }

    public T Get(int i)
    {
        if (i < Count) return _items[i];
        return null;
    }

    public override void Clear()
    {
        _items.Clear();
        OnChanged.Invoke();
    }

    public void Shuffle()
    {
        _items.Shuffle();
    }

    public int ElementCount(T t)
    {
        int counter = 0;
        foreach (T item in _items)
        {
            if (item == t) counter++;
        }

        return counter;
    }
}
