using UnityEngine;

public class DayMusicPlayer : MonoBehaviour
{
    [SerializeField] private DayManager DayManager;
    [SerializeField] private AudioSource AudioSource;

    private bool IsPlaying = false;

    public void StartMusic()
    {
        var dayData = DayManager.GetDayData();
        if (dayData == null || dayData.Music == null)
        {
            return;
        }

        AudioSource.clip = dayData.Music;
        AudioSource.Play();
        IsPlaying = true;
    }

    private void Update()
    {
        if (!IsPlaying)
        {
            return;
        }

        var currentRatio = DayManager.GetTimeLeftRatio();

        var dayData = DayManager.GetDayData();
        if (dayData == null)
        {
            return;
        }

        if (currentRatio >= .66f)
        {
            if (AudioSource.clip != dayData.MusicFastest)
            {
                AudioSource.clip = dayData.MusicFastest;
                AudioSource.Play();
            }

            return;
        }

        if (currentRatio >= .33f)
        {
            if (AudioSource.clip != dayData.MusicFast)
            {
                AudioSource.clip = dayData.MusicFast;
                AudioSource.Play();
            }

            return;
        }
    }
}
