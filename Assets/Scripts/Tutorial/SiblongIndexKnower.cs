using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiblongIndexKnower : MonoBehaviour
{
    public int siblingIndex;

    private void Update()
    {
        siblingIndex = gameObject.transform.GetSiblingIndex();
    }
}
