using UnityEngine;

public class BadumController : MonoBehaviour
{
    private const string kIsBeatingString = "IsBeating";
    private static int kIsBeatingHash = Animator.StringToHash(kIsBeatingString);

    [SerializeField] private Animator BadumAnimatorController;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip BaSoundClip;
    [SerializeField] private AudioClip DumSoundClip;

    public void StartBadum(float speed)
    {
        BadumAnimatorController.SetBool(kIsBeatingHash, true);
        BadumAnimatorController.speed = speed;
    }

    public void StopBadum()
    {
        BadumAnimatorController.SetBool(kIsBeatingHash, false);
        BadumAnimatorController.speed = 1;
    }

    public void OnBa()
    {
        AudioSource.PlayOneShot(BaSoundClip);
    }

    public void OnDum()
    {
        AudioSource.PlayOneShot(DumSoundClip);
    }
}
