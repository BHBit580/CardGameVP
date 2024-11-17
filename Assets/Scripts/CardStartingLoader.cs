using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStartingLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] cardSprites;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject initialGroup;
    [SerializeField] private TextAsset textJson;
    [SerializeField] private List<string> cardsList;
    
    [System.Serializable]
    public class CardData
    {
        public Data data;
    }

    [System.Serializable]
    public class Data
    {
        public string[] deck;
    }

    void Start()
    {
        CardData cardList = JsonUtility.FromJson<CardData>(textJson.text);
        
        foreach (string card in cardList.data.deck)
        {
            cardsList.Add(card);
        }

        LoadCards();
    }

    private void LoadCards()
    {
        int i = 0;
        foreach (string cardName in cardsList)
        {
            GameObject cardObject = Instantiate(cardPrefab, initialGroup.transform);
            CardManager.Instance.cardsList.Add(cardObject);
            Sprite cardSprite = FindCardSprite(cardName);
            cardObject.GetComponent<CardView>().SetImage(cardSprite);
            cardObject.GetComponent<CardView>().globalIndex = i;
            i++;
        }
    }

    private Sprite FindCardSprite(string cardName)
    {
        foreach (Sprite sprite in cardSprites)
        {
            if (sprite.name == cardName)
            {
                return sprite;
            }
        }
        return null;
    }
}