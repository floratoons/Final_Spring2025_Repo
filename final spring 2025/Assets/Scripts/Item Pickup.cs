using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    // the prefab that will be instantiated when picked up
    public GameObject gemPrefab;

    // the transformsocket where the gem will be parented
    public Transform gemSocket;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // instantiate and parent directly to gem socket
            GameObject newGem = Instantiate(gemPrefab, gemSocket.position, Quaternion.identity, gemSocket);

            // resetting the position and rotation to make sure it fits in the "socket"
            newGem.transform.localPosition = Vector3.zero;
            newGem.transform.localRotation = Quaternion.identity;

            // ** add gem to the list, and destroy the gem pickup object
            other.GetComponent<ItemManager>().AddGem(newGem);
            Destroy(gameObject);

        }
    }

}
