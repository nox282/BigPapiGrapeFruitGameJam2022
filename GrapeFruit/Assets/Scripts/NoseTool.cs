using UnityEngine;

public class NoseTool : Tool
{
    [SerializeField] private Nose Nose;

    private Nose.NoseState? PreviousState;

    private void Update()
    {
        if (Nose.State == Nose.NoseState.IDLE && PreviousState.HasValue && PreviousState.Value == Nose.NoseState.FOLLOWING)
        {
            HandleOnEndDragRecipients();
        }

        PreviousState = Nose.State;

        if (Nose.State != Nose.NoseState.FOLLOWING)
        {
            return;
        }

        HandleOnDragRecipients();
    }
}
