using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float zOffset;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + zOffset);
    }
}
