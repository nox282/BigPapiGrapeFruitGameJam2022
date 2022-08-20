using UnityEngine;

/// <summary>
/// Controls the doge
/// </summary>
public class PawsController : MonoBehaviour
{
    public Paw leftPaw;
    public Paw rightPaw;
    public Nose nose;

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
        bool noseActive = false;

        if (viewportMousePos.x >= 0 && viewportMousePos.x <= 1 && viewportMousePos.y >= 0 && viewportMousePos.y <= 1)
        {
            if (!isHoldingObject && !Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                leftPaw.SetState(Paw.PawState.IDLE);
                rightPaw.SetState(Paw.PawState.IDLE);
                nose.SetState(Nose.NoseState.FOLLOWING);
                nose.Follow(viewportMousePos);
                noseActive = true;
            }
            else if (viewportMousePos.x < .5f)
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

            if (!noseActive)
            {
                currentActivePaw.Follow(viewportMousePos);
            }
        }
        else if (isHoldingObject)
        {
            currentActivePaw.Follow(viewportMousePos);
        }
        //else if (!Input.GetMouseButton(0) && Input.GetMouseButton(1))
        //{
        //    nose.SetState(Nose.NoseState.FOLLOWING);
        //    nose.Follow(viewportMousePos);
        //}
        else
        {
            leftPaw.SetState(Paw.PawState.IDLE);
            rightPaw.SetState(Paw.PawState.IDLE);
            nose.SetState(Nose.NoseState.IDLE);
        }

        if (currentActivePaw != null)
        {
            currentActivePaw.Shrink(Input.GetMouseButton(0));
        }

        if (!noseActive)
        {
            nose.SetState(Nose.NoseState.IDLE);
        }
    }

    public void SetHoldingObject(bool isHolding)
    {
        isHoldingObject = isHolding;
    }
}