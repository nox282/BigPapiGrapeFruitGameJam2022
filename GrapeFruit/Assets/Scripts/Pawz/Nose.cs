using System.Collections;
using UnityEngine;

public class Nose : MonoBehaviour
{
    public NoseState State;
    public Vector3 AnchorPos;

    private Vector3 initialPosition;
    private Vector3 gotoPosition;
    private float shakeTimer;

    private const float lerpValue = .1f;
    private const float lerpValueIdle = .05f;
    private const float shakeTotalTime = .5f;

    public enum NoseState
    {
        IDLE,
        FOLLOWING,
    }

    private void Start()
    {
        initialPosition = transform.position;
        gotoPosition = initialPosition;

        StartCoroutine(RandomizeIdleShake());
    }

    private void Update()
    {
        Vector3 endPosition = gotoPosition;

        if (shakeTimer > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere;
            endPosition.x += randomOffset.x * .5f;
            endPosition.y += randomOffset.y * .5f;
            shakeTimer -= Time.deltaTime;
        }

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, endPosition.x, State == NoseState.IDLE ? lerpValueIdle : lerpValue),
            Mathf.Lerp(transform.position.y, endPosition.y, State == NoseState.IDLE ? lerpValueIdle : lerpValue),
            0);

        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(AnchorPos.x - transform.position.x, transform.position.y - AnchorPos.y) * Mathf.Rad2Deg);
    }

    public void Follow(Vector3 viewportPos)
    {
        var cameraPos = Camera.main.ViewportToWorldPoint(viewportPos);
        gotoPosition = cameraPos;
        gotoPosition.z = 0;
    }

    private void Shake()
    {
        shakeTimer = shakeTotalTime;
    }

    public void SetState(NoseState newState)
    {
        if (State == newState)
        {
            return;
        }

        State = newState;

        switch (State)
        {
            case NoseState.IDLE:
                gotoPosition = initialPosition;
                break;
            case NoseState.FOLLOWING:
                break;
            default:
                break;
        }
    }

    private IEnumerator RandomizeIdleShake()
    {
        while (true)
        {
            Shake();

            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}