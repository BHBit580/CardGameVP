using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private float finalLocalYPos;
    [SerializeField] private float speed = 4f;
    [SerializeField] private List<Sprite> cardSprites;
    [SerializeField] private GameObject cardHolder, cardPrefab, canvas;

    public List<GameObject> cardsList;
    [SerializeField] private List<GameObject> newGroupList;
    
    public CardView selectedCard;

    
    private int _currentGlobalMovingIndex;
    private int _k;
    
    
    public static CardManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void SetSelectedCard(CardView card)
    {
        selectedCard = card;
        selectedCard.transform.SetParent(canvas.transform);
        _currentGlobalMovingIndex = selectedCard.globalIndex;
    }
    
    private void UpdateCurrentMovingIndex()
    {
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
    }

    
    public void ReleaseCard()
    {
        GameObject card = cardsList[_currentGlobalMovingIndex];
        selectedCard.transform.SetParent(card.transform.parent);
        selectedCard.transform.SetSiblingIndex(card.transform.GetSiblingIndex());
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
    
    private void Start()
    {
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
    }
    
    public void AnimateCardOnClick(CardView card)
    {
        card.gameObject.GetComponent<RectTransform>().DOLocalMoveY(finalLocalYPos, 1 / speed);
        if(!newGroupList.Contains(card.gameObject)) newGroupList.Add(card.gameObject);
    }

    public void MakeAnotherGroup()
    {
        GameObject group = new GameObject("Grp");
        HorizontalLayoutGroup horizontalLayoutGroup = group.AddComponent<HorizontalLayoutGroup>();
        horizontalLayoutGroup.spacing = -77.5f;
        horizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        horizontalLayoutGroup.childControlHeight = true;
        horizontalLayoutGroup.childControlWidth = true;
        horizontalLayoutGroup.childForceExpandHeight = false;
        horizontalLayoutGroup.childForceExpandWidth = false;
        group.transform.SetParent(transform);
        foreach (var obj in newGroupList)
        {
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -95);
            obj.transform.SetParent(group.transform);
        }

        cardsList = cardsList.Concat(newGroupList).ToList();
        newGroupList.Clear();
        group.AddComponent<Group>();
    }
}
