using System;
using UnityEngine;

public class CustomHorizontalLayout : MonoBehaviour
{
    public float spacing = 10f;

    private void Update()
    {
        LayoutChildren();
    }

    private void LayoutChildren()
    {
        float totalWidth = 0f;
        int totalChildren = transform.childCount;

        // Calculate total width of all child elements
        for (int i = 0; i < totalChildren; i++)
        {
            totalWidth += transform.GetChild(i).localScale.x;
        }

        // Add spacing between child elements
        totalWidth += (totalChildren - 1) * spacing;

        // Position child elements horizontally
        float x = -totalWidth / 2f;
        for (int i = 0; i < totalChildren; i++)
        {
            var child = transform.GetChild(i);
            child.localPosition = new Vector3(x, child.localPosition.y, child.localPosition.z);
            x += child.localScale.x + spacing;
        }
    }
}