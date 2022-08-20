using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Camera MainCamera;
    private Vector3 OriginalPosition;
    private Paw Paw;

    private float OriginalZ;

    private void Awake()
    {
        MainCamera = Camera.main;
        OriginalPosition = transform.position;
        OriginalZ = OriginalPosition.z;
    }

    public void OnBeginDrag()
    {
        Debug.Log($"{gameObject.name}.OnBeginDrag");

        if (PawsController.Instance == null || Paw != null)
        {
            return;
        }

        PawsController.Instance.SetHoldingObject(true);
        Paw = PawsController.Instance.CurrentActivePaw;
    }

    public void OnDrag()
    {
        if (Paw != null)
        {
            var targetPosition = Paw.transform.position;
            targetPosition.z = OriginalZ;
            transform.position = targetPosition;
        }
    }

    public void OnEndDrag()
    {
        Paw = null;
        transform.position = OriginalPosition;

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
