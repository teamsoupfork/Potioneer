using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// Prevents the duplication of gameobjects
    /// </summary>
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<DoNotDestroyOnLoad>().Length;
        if (numMusicPlayers != 1)
        {
            Destroy(gameObject);
        }
        // if more then one music player is in the scene
        //destroy itself
        else
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }
}
