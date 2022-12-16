using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private FloatValue minimumHeight;
    [SerializeField]
    private IntValue score, floor, bestJump;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private float baseDeathClockSpeed, maxDeathClockSpeed;
    [SerializeField]
    private int maxDeathClockFloor;
    [SerializeField]
    private GameObject endScreen;

    private void Start()
    {
        floor.SetValue(0);
        score.SetValue(0);
        bestJump.SetValue(0);
        floor.OnValueChanged.AddListener(UpdateScore);
    }

    private void Update()
    {
        if(player.controlEnabled && player.GetCurrentPlatformPosition() * 3 < minimumHeight)
        {
            // trigger end of game
            player.controlEnabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            endScreen.SetActive(true);
        }
        minimumHeight.SetValue(minimumHeight + GetDeathClockSpeed() * Time.deltaTime);
    }

    private float GetDeathClockSpeed()
    {
        if (floor < 6)
            return 0;
        return Mathf.Lerp(baseDeathClockSpeed, maxDeathClockSpeed, floor / maxDeathClockFloor);
    }

    private int lastFloor;
    private void UpdateScore(int newFloor)
    {
        var diff = newFloor - lastFloor;
        score.SetValue(score + diff * diff * Mathf.Max(1, newFloor / 10));
        lastFloor = newFloor;
        bestJump.SetValue(Mathf.Max(bestJump, diff));
    }
}
