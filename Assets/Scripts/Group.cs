using UnityEngine;
public class Group : MonoBehaviour
{ 
    private void Update()
    {
        if(transform.childCount ==0) Destroy(gameObject);
    }
}