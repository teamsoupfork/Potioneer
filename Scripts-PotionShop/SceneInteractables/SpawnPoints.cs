using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns objects at a certain point
/// </summary>
[System.Serializable]
public class SpawnPoints : MonoBehaviour

{
    public GameObject itemObj;
    GameObject item;
    public ParticleSystem PSHighlight;
    public GameObject SpawnPoint;
    //private GameObject[] Potions;
    void Start()
    {
        ItemSpawn(itemObj, SpawnPoint);
        ParticleSpawn();
    }
    private void Update()
    {
        
    }
    void ItemSpawn(GameObject itemObj, GameObject SpawnPoint)
    {
        if (SpawnPoint != null)
        {
            item = Instantiate(itemObj, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            item.transform.parent = this.transform;
        }      
    }

    void ParticleSpawn()
    {
        ParticleSystem highlight = Instantiate(PSHighlight, item.transform.position + new Vector3(0, 1, 0) , item.transform.rotation);
        highlight.transform.parent = this.transform;
    }
}
