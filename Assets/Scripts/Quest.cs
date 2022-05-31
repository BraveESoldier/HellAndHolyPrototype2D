using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questNum;
    public int[] items;
    public GameObject[] clouds;
    public GameObject barrier;
    public GameObject key;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player" && other.gameObject.GetComponent<Pickup>().id == items[questNum])
        {
            questNum++;
            Destroy(other.gameObject);
        }
    }
}
