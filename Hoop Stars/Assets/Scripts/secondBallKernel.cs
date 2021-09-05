using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBallKernel : MonoBehaviour
{
    [SerializeField] private FirstBallKernel _firstBallKernel;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger")
        {
            _firstBallKernel.isAlreadyPlayerDetected = false;
        }
        if (other.gameObject.tag == "BotTrigger")
        {
            _firstBallKernel.isAlreadyBotDetected = false;
        }
    }
}
