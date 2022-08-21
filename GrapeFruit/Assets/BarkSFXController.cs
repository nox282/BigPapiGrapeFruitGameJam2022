using System.Collections.Generic;
using UnityEngine;

public class BarkSFXController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> BarkSFXs = new List<AudioClip>();
    [SerializeField] private List<AudioClip> WhineSFXs = new List<AudioClip>();
    [SerializeField] private AudioSource AudioSource;

    public void Bark()
    {
        var randomIndex = Random.Range(0, BarkSFXs.Count - 1);
        AudioSource.PlayOneShot(BarkSFXs[randomIndex]);
    }

    public void Whine()
    {
        var randomIndex = Random.Range(0, WhineSFXs.Count - 1);
        AudioSource.PlayOneShot(WhineSFXs[randomIndex]);
    }
}
