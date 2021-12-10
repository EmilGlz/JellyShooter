using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HMN"))
        {
            if (other.gameObject.GetComponent<HumanPart>()!= null)
            {
                if (other.gameObject.GetComponent<HumanPart>().human.isAlive)
                {
                    Destroy(other.gameObject.GetComponent<HumanPart>().human.gameObject);
                }
            }
        }
    }
}
