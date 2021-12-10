using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] GameObject[] humanCorps;
    [SerializeField] int humanCounter = 0;
    Vector3 humanCorpStartScale;
    [SerializeField] Transform increasingTransform;

    private void Start()
    {
        humanCorpStartScale = humanCorps[0].transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FRUIT") || other.CompareTag("HMN"))
        {
            if (humanCounter >= humanCorps.Length)
            {
                humanCounter = 0;
            }
            humanCorps[humanCounter].SetActive(true);
            humanCounter++;
            increasingTransform.localScale += Vector3.one * 30f;
            for (int i = 0; i < humanCorps.Length; i++)
            {
                humanCorps[i].transform.localScale = humanCorpStartScale;
            }
            Destroy(other.gameObject, 0.2f);
        }
    }
}
