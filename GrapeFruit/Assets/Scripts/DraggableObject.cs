using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Camera MainCamera;
    private Vector3 OriginalPosition;
    private float OriginalZ;
    private bool IsBeingDragged = false;

    private void Awake()
    {
        MainCamera = Camera.main;
        OriginalPosition = transform.position;
        OriginalZ = OriginalPosition.z;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (IsBeingDragged)
        {
            var worldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = OriginalZ;
            transform.position = worldPosition;
        }
        else
        {
            transform.position = OriginalPosition;
        }
    }

    public void OnBeginDrag()
    {
        Debug.Log($"{gameObject.name}.OnBeginDrag");
        PawsController.Instance?.SetHoldingObject(true);
        IsBeingDragged = true;
    }

    public void OnDrag()
    {

    }

    public void OnEndDrag()
    {
        IsBeingDragged = false;
        var worldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        var colliders = Physics2D.OverlapPointAll(worldPosition);
        foreach (var collider in colliders)
        {
            var dragRecipent = collider.gameObject.GetComponent<DragObjectRecipient>();
            if (dragRecipent != null)
            {
                dragRecipent.OnDraggedOn(this);
                OnDragged(dragRecipent);
            }
        }

        PawsController.Instance?.SetHoldingObject(false);
        Debug.Log($"{gameObject.name}.OnEndDrag");
    }

    protected virtual void OnDragged(DragObjectRecipient Recipient)
    {
        // TODO @nox extend tool function here.
    }
}
