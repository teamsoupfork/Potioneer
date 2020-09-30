using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles a dont destroy on load function for the Pause Menu
/// Script cant be modified
/// </summary>
public class pause : MonoBehaviour
{

    
    public static pause Instance;
    

    void Awake()
    {
       
        if(Instance == null)
        {
            Instance = this;
        }
        else
            DestroyImmediate(this);

       
        DontDestroyOnLoad(Instance);
    }

    public GameObject[] GetAllRootsOfDontDestroyOnLoad()
    {
        return gameObject.scene.GetRootGameObjects();
    }
}

// Example to access the dontdestroy-objects from anywhere
public class FindDontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] rootsFromDontDestroyOnLoad;
    void Start()
    {
       
        rootsFromDontDestroyOnLoad = pause.Instance.GetAllRootsOfDontDestroyOnLoad();
    }
}
