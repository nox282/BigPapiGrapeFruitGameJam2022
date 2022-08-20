using UnityEngine;

public class PawsController : MonoBehaviour
{
    public Paw leftPaw;
    public Paw rightPaw;

    private Paw currentActivePaw;
    private bool isHoldingObject;

    public static PawsController Instance;

    public Paw CurrentActivePaw => currentActivePaw;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var viewportMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (viewportMousePos.x >= 0 && viewportMousePos.x <= 1 && viewportMousePos.y >= 0 && viewportMousePos.y <= 1)
        {
            if (viewportMousePos.x < .5f)
            {
                if (leftPaw.State == Paw.PawState.IDLE)
                {
                    if (!isHoldingObject)
                    {
                        leftPaw.SetState(Paw.PawState.FOLLOWING);
                        rightPaw.SetState(Paw.PawState.IDLE);
                        currentActivePaw = leftPaw;
                    }
                }
            }
            else if (viewportMousePos.x >= .5f)
            {
                if (rightPaw.State == Paw.PawState.IDLE)
                {
                    if (!isHoldingObject)
                    {
                        leftPaw.SetState(Paw.PawState.IDLE);
                        rightPaw.SetState(Paw.PawState.FOLLOWING);
                        currentActivePaw = rightPaw;
                    }
                }
            }

            currentActivePaw.Follow(viewportMousePos);
        }
        else if (isHoldingObject)
        {
            currentActivePaw.Follow(viewportMousePos);
        }
        else
        {
            leftPaw.SetState(Paw.PawState.IDLE);
            rightPaw.SetState(Paw.PawState.IDLE);
        }

        if (currentActivePaw != null)
        {
            currentActivePaw.Shrink(Input.GetMouseButton(0));
        }
    }

    public void SetHoldingObject(bool isHolding)
    {
        isHoldingObject = isHolding;
    }
}