using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour , IPointerDownHandler , IPointerUpHandler , IDragHandler 
{
    [SerializeField] private float clickThreshold = 0.2f;
    
    private float _lastClickTime;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _lastClickTime = Time.time;
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>() != null)
            {
                CardManager.Instance.SetSelectedCard(eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>());
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.Instance.ReleaseCard();
        
        /*if (Time.time - _lastClickTime < clickThreshold)               //If current time minus lastclick
        {
            Debug.Log("It's a click");
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>() != null)
            {
                CardManager.Instance.AnimateCardOnClick(eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>());
            }
            CardManager.Instance.ReleaseCard();
        }
        else
        {
            Debug.Log("It's a drag");
            
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardManager.Instance.MoveCard(eventData.position.x);
    }
}
