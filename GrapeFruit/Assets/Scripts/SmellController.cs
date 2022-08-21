using UnityEngine;

public class SmellController : MonoBehaviour
{
    private const string kIsSmellingString = "IsSmelling";
    private static int kIsSmellingHash = Animator.StringToHash(kIsSmellingString);

    [SerializeField] private Animator SmellAnimatorController;

    public void StartSmelling()
    {
        SmellAnimatorController.SetBool(kIsSmellingHash, true);
    }

    public void StopSmelling()
    {
        SmellAnimatorController.SetBool(kIsSmellingHash, false);
    }
}
