using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;

    private bool playerIsOverLapping = false;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverLapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If true player moved across the portal
            if (dotProduct < 0f)
            {
                // teleport
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverLapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TELEPORT");
        if (other.tag == "Player")
        {
            playerIsOverLapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("NOT TELEPORT");
        if (other.tag == "Player")
        {
            playerIsOverLapping = false;
        }
    }

}
