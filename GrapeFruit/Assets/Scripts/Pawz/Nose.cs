using System;
using System.Collections;
using UnityEngine;

public class Nose : MonoBehaviour
{
    private const string kIsWiggleString = "IsWiggle";
    private static int kIsWiggleHash = Animator.StringToHash(kIsWiggleString);

    [SerializeField] private Animator AnimatorController;

    public NoseState State;
    public Vector3 AnchorPos;

    private Vector3 initialPosition;
    private Vector3 gotoPosition;
    private float shakeTimer;

    private const float lerpValue = .1f;
    private const float lerpValueIdle = .05f;
    private const float shakeTotalTime = .5f;

    public event Action<NoseState> OnStateChanged;

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
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere;
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
        OnStateChanged?.Invoke(State);

        switch (State)
        {
            case NoseState.IDLE:
                gotoPosition = initialPosition;
                AnimatorController.SetBool(kIsWiggleHash, false);
                break;
            case NoseState.FOLLOWING:
                AnimatorController.SetBool(kIsWiggleHash, true);
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

            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 7f));
        }
    }
}