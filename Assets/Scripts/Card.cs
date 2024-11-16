using System;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private float yLocalPos = 0.5f;
    [SerializeField] private float speed = 1f;
    public string cardName = "C4";

    private CardGroupLogicManager _cardGroupLogicManager;

    private void Awake()      
    {
        _cardGroupLogicManager = FindObjectOfType<CardGroupLogicManager>();
    }

    private void OnMouseDown()
    {
        _cardGroupLogicManager.currentNumberOfCardsSelected.Add(gameObject);
        transform.DOLocalMoveY(yLocalPos, 1 / speed);
    }
}
