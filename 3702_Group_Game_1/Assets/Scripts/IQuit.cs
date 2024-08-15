using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is for when the WingeBaby character nerd rages and quits the game
/// </summary>
public class IQuit : MonoBehaviour
{
    [Tooltip("The effects that will spawn when the character quits the game")]
    public GameObject[] iQuitEffects;

    [MethodButton("I quit Tantrum")]
    public void Invoke()
    {
        // Add sound effect spawn of the character quitting
        // cry cry cry

        // Spawn tantrum effects
        foreach (GameObject effect in iQuitEffects)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
        }

        GetComponent<Die>().Invoke();
    }

}
