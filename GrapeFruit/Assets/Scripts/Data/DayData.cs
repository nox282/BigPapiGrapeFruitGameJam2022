using UnityEngine;

[CreateAssetMenu(fileName = "DayData", menuName = "Data/DayData", order = 0)]
public class DayData : ScriptableObject
{
    [field: SerializeField]
    public IllnessData[] Illnesses { get; private set; }

    [field: SerializeField]
    public float MaxTime { get; private set; } = 60f;

    [field: SerializeField]
    public bool IsTutorial { get; private set; }

    [field: SerializeField]
    public AudioClip Music { get; private set; }

    [field: SerializeField]
    public AudioClip MusicFast { get; private set; }

    [field: SerializeField]
    public AudioClip MusicFastest { get; private set; }
}
