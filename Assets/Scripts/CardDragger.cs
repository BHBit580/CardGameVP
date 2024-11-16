using UnityEngine;

public class CardDragger : MonoBehaviour
{
    [SerializeField] private float minLocalX, maxLocalX;

    private Vector3 _offset;
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("Hey you forgot to add collider2d to the gameobject");
        }
    }

    private void OnMouseDown()
    {
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
        _offset = transform.localPosition - transform.parent.InverseTransformPoint(mouseWorldPos);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
        Vector3 targetLocalPos = transform.parent.InverseTransformPoint(mouseWorldPos) + _offset;
        
        float clampedX = Mathf.Clamp(targetLocalPos.x, minLocalX, maxLocalX);
        
        transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
    }
}
