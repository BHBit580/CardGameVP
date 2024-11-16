using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroupLogicManager : MonoBehaviour
{
    public GameObject[] cards;
    public List<GameObject> currentNumberOfCardsSelected;

    public void OnClickGroupButton()
    {
        GameObject newGroup = new GameObject("Group12");
        newGroup.transform.SetParent(transform);
        foreach (var g in currentNumberOfCardsSelected)
        {
            g.transform.SetParent(newGroup.transform);
        }
    }
}
