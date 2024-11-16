using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    private Image _image;
    public int globalIndex = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
