using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{

    [SerializeField] GameObject player;
    List<GameObject> chunks;
    [SerializeField] float loadDistance = 50.0f;
    [SerializeField] float checkInterval = 1.0f;

    void Start()
    {
        chunks = new List<GameObject>();
        foreach (Transform child in transform)
        {
            chunks.Add(child.gameObject);
        }

        StartCoroutine(CheckChunkDistance());
    }

    IEnumerator CheckChunkDistance()
    {
        foreach (GameObject chunk in chunks)
        {
            if (chunk != null)
            {
                float distance = Vector3.Distance(player.transform.position, chunk.transform.position);
                if (distance > loadDistance)
                {
                    chunk.SetActive(false);
                }
                else
                {
                    chunk.SetActive(true);
                }
            }
        }

        yield return new WaitForSeconds(checkInterval);
        StartCoroutine(CheckChunkDistance());}
}
