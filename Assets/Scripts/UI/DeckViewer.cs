using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckViewer : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private RuntimeSetCardData _playerDeck;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _scrollView;

    private bool _isOpen = false;

    public void Toggle()
    {
        if (_isOpen) CloseDeckViewer();
        else OpenDeckViewer();

        _isOpen = !_isOpen;
    }

    private void OpenDeckViewer()
    {
        foreach (Transform t in _content.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (CardData cd in _playerDeck.Items)
        {
            Card c = Instantiate(_cardPrefab, _content.transform);
            c.IsSelectable = false;
            c.Data = cd;
        }

        _scrollView.SetActive(true);
    }

    private void CloseDeckViewer()
    {
        _scrollView.SetActive(false);
    }
}
