using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputManager : MonoBehaviourSingleton<CardInputManager>, IPointerDownHandler , IPointerUpHandler , IDragHandler 
{
    [SerializeField] private float clickThreshold = 0.2f;
    public bool isOneTimeClick = false;
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
        if (Time.time - _lastClickTime < clickThreshold)               //If current time minus lastclick
        {
            Debug.Log("It's a click");
            isOneTimeClick = true;
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>() != null)
            {
                //Animate when it's a one time click
                CardManager.Instance.AnimateCardOnClick(eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>());
            }
        }
        else
        {
            //If there is already someobject that is UP then after that we will only make up animations
            if(isOneTimeClick) CardManager.Instance.AnimateCardOnClick(CardManager.Instance.selectedCard);
            else CardManager.Instance.ReleaseCard();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //No drag when it's a one time click
        if(isOneTimeClick) return;
        CardManager.Instance.MoveCard(eventData.position.x);
    }
}
