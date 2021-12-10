using UnityEngine;

public class GroundMover : MonoBehaviour
{
    [SerializeField] Transform nextGround;
    [SerializeField] float distance;

    private void Start()
    {
        distance = Mathf.Abs(transform.parent.position.z - nextGround.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Collision with" + other.gameObject.name);
            nextGround.position = new Vector3(nextGround.position.x, nextGround.position.y, nextGround.position.z - distance * 2);
        }
    }
}
