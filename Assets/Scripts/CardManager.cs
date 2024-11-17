using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviourSingleton<CardManager>
{
    [SerializeField] private float finalLocalYPos;
    [SerializeField] private float speed = 4f;
    
    [SerializeField] private List<Sprite> cardSprites;
    [SerializeField] private List<GameObject> newGroupList;
    [SerializeField] private GameObject cardHolder, cardPrefab, canvas , makeGroupButton;

    public List<GameObject> cardsList;
    public CardView selectedCard;

    
    private int _currentGlobalMovingIndex;
    
    
    public void SetSelectedCard(CardView card)
    {
        selectedCard = card;
        selectedCard.transform.SetParent(canvas.transform);
        _currentGlobalMovingIndex = selectedCard.globalIndex;
    }
    
    private void UpdateCurrentMovingIndex()
    {
        //This method finds the closest card near our selected card 
        if (selectedCard == null || cardsList.Count == 0) return;

        float closestDistance = float.MaxValue;
        int closestIndex = _currentGlobalMovingIndex;

        for (int i = 0; i < cardsList.Count; i++)
        {
            if (cardsList[i] == selectedCard.gameObject) continue;

            float distance = Vector2.Distance(selectedCard.transform.position, cardsList[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        _currentGlobalMovingIndex = closestIndex;
        Debug.Log(_currentGlobalMovingIndex);
    }

    
    public void ReleaseCard()
    {
        GameObject card = cardsList[_currentGlobalMovingIndex];
        int siblingIndex = card.transform.GetSiblingIndex();

        if (siblingIndex != card.transform.parent.childCount-1)
        {
            //If it's not a last index child
            selectedCard.transform.SetSiblingIndex(siblingIndex);
        }
       
        selectedCard.transform.SetParent(card.transform.parent);
        UpdateListGlobalIndex();
        selectedCard = null;
    }

    public void MoveCard(float xPos)
    {
        if(selectedCard != null) selectedCard.transform.position = new Vector2(xPos, selectedCard.transform.position.y);
        if(selectedCard !=null) UpdateCurrentMovingIndex();
    }
    
    private void UpdateListGlobalIndex()
    {
        cardsList.Remove(selectedCard.gameObject);
        cardsList.Insert(_currentGlobalMovingIndex , selectedCard.gameObject);
       
        for (int i = 0; i < cardsList.Count; i++)
        {
            cardsList[i].GetComponent<CardView>().globalIndex = i;
        }
    }
    
    /*
    private void Start()
    {
        int _k;
        for (int i = 0; i < 8; i++)
        {
            SpawnCard(_k);
            _k++;
        }
    }

    private void SpawnCard(int name)
    {
        GameObject card = Instantiate(cardPrefab);
        card.name = "Card" + name;
        card.transform.SetParent(cardHolder.transform);
        card.GetComponent<CardView>().SetImage(cardSprites[Random.Range(0,cardSprites.Count)]);
        card.GetComponent<CardView>().globalIndex = _k;
        cardsList.Add(card);
    }*/
    
    public void AnimateCardOnClick(CardView card)
    {
        CardInputManager.Instance.isOneTimeClick = true;
        card.gameObject.GetComponent<RectTransform>().DOLocalMoveY(finalLocalYPos, 1 / speed);
        if(!newGroupList.Contains(card.gameObject)) newGroupList.Add(card.gameObject);
        if(newGroupList.Count >1) makeGroupButton.SetActive(true);
        cardsList.Remove(card.gameObject);
    }

    public void MakeAnotherGroup()
    {
        GameObject group = new GameObject("Grp");
        HorizontalLayoutGroup horizontalLayoutGroup = group.AddComponent<HorizontalLayoutGroup>();
        horizontalLayoutGroup.spacing = -100f;
        horizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        horizontalLayoutGroup.childControlHeight = true;
        horizontalLayoutGroup.childControlWidth = true;
        horizontalLayoutGroup.childForceExpandHeight = false;
        horizontalLayoutGroup.childForceExpandWidth = false;
        group.transform.SetParent(transform);
        foreach (var obj in newGroupList)
        {
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -95);
            obj.transform.SetParent(group.transform);
        }
        
        
        cardsList = cardsList.Concat(newGroupList).ToList();
        newGroupList.Clear();
        group.AddComponent<Group>();
        CardInputManager.Instance.isOneTimeClick = false;
        makeGroupButton.SetActive(false);
    }
}
