using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private DraggableObject CurrentObject;

    private Camera MainCamera;

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
            Debug.Log("OnMouseButtonDown();");
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
            Debug.Log("OnMouseButtonUp();");
        }

        if (CurrentObject != null)
        {
            CurrentObject.OnDrag();
        }
    }

    private void OnMouseButtonDown()
    {
        if (CurrentObject != null)
        {
            return;
        }

        var mouseWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        var colliders = Physics2D.OverlapPointAll(mouseWorldPosition);
        foreach (var collider in colliders)
        {
            var draggableObject = collider.gameObject.GetComponent<DraggableObject>();
            if (draggableObject != null)
            {
                CurrentObject = draggableObject;
                CurrentObject.OnBeginDrag();
                break;
            }
        }
    }

    private void OnMouseButtonUp()
    {
        if (CurrentObject != null)
        {
            CurrentObject.OnEndDrag();
            CurrentObject = null;
            return;
        }


        var mouseWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        var colliders = Physics2D.OverlapPointAll(mouseWorldPosition);
        foreach (var collider in colliders)
        {
            var clickableObject = collider.gameObject.GetComponent<ClickableObject>();
            if (clickableObject != null)
            {
                clickableObject.OnClicked();
                break;
            }
        }
    }
}
