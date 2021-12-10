using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] GameObject[] stainPrefabs;
    [SerializeField] Shooter shooter;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            int r = Random.Range(0, stainPrefabs.Length);
            Color strainColor = shooter.fogColors[shooter.randColorIndex];
            strainColor.a = 1f;
            stainPrefabs[r].GetComponent<SpriteRenderer>().color = strainColor;
            Instantiate(stainPrefabs[r], collision.GetContact(0).point + Vector3.up * 0.1f, Quaternion.Euler(90,Random.Range(0,360),0));
        }
    }


}
