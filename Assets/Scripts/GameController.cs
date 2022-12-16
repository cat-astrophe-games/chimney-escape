using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private FloatValue minimumHeight;
    [SerializeField]
    private PlayerController player;

    private bool deathMarch;

    private void Update()
    {
        if(player.GetCurrentPlatformPosition() * 3 < minimumHeight)
        {
            // trigger end of game
            player.controlEnabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        if(!deathMarch && player.GetMaxRoundedPlatformPosition() >= 6)
            deathMarch = true;
        if(deathMarch)
        {
            minimumHeight.SetValue(minimumHeight + 0.75f * Time.deltaTime);
        }
    }
}
