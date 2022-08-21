using UnityEngine;

public class SmellController : MonoBehaviour
{
    private const string kIsSmellingString = "IsSmelling";
    private static int kIsSmellingHash = Animator.StringToHash(kIsSmellingString);

    [SerializeField] private Animator SmellAnimatorController;
    [SerializeField] private TMPro.TMP_Text SmellText;

    public void StartSmelling(string smellDescription)
    {
        SmellText.text = smellDescription;
        SmellAnimatorController.SetBool(kIsSmellingHash, true);
    }

    public void StopSmelling()
    {
        SmellAnimatorController.SetBool(kIsSmellingHash, false);
    }
}
