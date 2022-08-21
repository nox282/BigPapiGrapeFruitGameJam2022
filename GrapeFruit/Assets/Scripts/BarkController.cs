using System.Collections.Generic;
using UnityEngine;

public class BarkController : MonoBehaviour
{
    public List<GameObject> Barks = new List<GameObject>();

    private GameObject activeBark;
    private float timer;

    private const float totalTimer = .5f;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                activeBark.SetActive(false);
                activeBark = null;
            }
        }
    }

    public void StartBark()
    {
        if (activeBark == null)
        {
            activeBark = Barks[Random.Range(0, Barks.Count)];
            activeBark.SetActive(true);

            timer = totalTimer;
        }
    }
}