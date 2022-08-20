using System.Collections;
using UnityEngine;

public class Paw : MonoBehaviour
{
    public GameObject Hand;
    public GameObject CenterPivot;
    public float ShrinkSize;
    public Vector3 AnchorPos;

    public PawState State;

    private Vector3 initialPosition;
    private Vector3 gotoPosition;
    private Vector3 idleOffset;
    private bool isSmol;

    private const float lerpValue = .1f;
    private const float lerpValueIdle = .005f;

    public enum PawState
    {
        IDLE,
        FOLLOWING,
    }

    private void Start()
    {
        initialPosition = transform.position;
        gotoPosition = initialPosition;

        StartCoroutine(RandomizeIdleOffset());
    }

    private void Update()
    {
        Vector3 endPosition = gotoPosition;
        if (State == PawState.IDLE)
        {
            endPosition += idleOffset;
        }

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, endPosition.x, State == PawState.IDLE ? lerpValueIdle : lerpValue),
            Mathf.Lerp(transform.position.y, endPosition.y, State == PawState.IDLE ? lerpValueIdle : lerpValue),
            0);

        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(AnchorPos.x - transform.position.x, transform.position.y - AnchorPos.y) * Mathf.Rad2Deg);

        // Wiggle anim on hover
        if (State == PawState.FOLLOWING)
        {
            var hits = Physics2D.RaycastAll(CenterPivot.transform.position, Vector3.forward, 1000);
            var foundHit = false;

            if (!Input.GetMouseButton(0))
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.GetComponent<PawInteractible>() != null)
                    {
                        float v = 1 + .02f * Mathf.Sin(Time.timeSinceLevelLoad * 20);
                        Hand.transform.localScale = new Vector3(v, v, v);
                        foundHit = true;
                    }
                }
            }

            if (!foundHit)
            {
                var v = isSmol ? ShrinkSize : 1;
                Hand.transform.localScale = new Vector3(v, v, v);
            }
        }
    }

    public void SetState(PawState newState)
    {
        if (State == newState)
        {
            return;
        }

        State = newState;

        switch (State)
        {
            case PawState.IDLE:
                gotoPosition = initialPosition;
                break;
            case PawState.FOLLOWING:
                break;
            default:
                break;
        }
    }

    public void Follow(Vector3 viewportPos)
    {
        var cameraPos = Camera.main.ViewportToWorldPoint(viewportPos);
        gotoPosition = cameraPos;
        gotoPosition.z = 0;
    }

    public void Shrink(bool small)
    {
        isSmol = small;
    }

    private IEnumerator RandomizeIdleOffset()
    {
        while (true)
        {
            idleOffset.x = Random.Range(-1f, 1f);
            idleOffset.y = Random.Range(-1f, 1f);

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }
}