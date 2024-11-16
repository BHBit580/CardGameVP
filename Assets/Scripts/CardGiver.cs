using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGiver : MonoBehaviour
{
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
    }
}