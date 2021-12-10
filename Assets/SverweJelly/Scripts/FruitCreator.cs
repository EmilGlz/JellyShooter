using System.Collections;
using UnityEngine;

public class FruitCreator : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject[] fruits;
    [SerializeField] float fruitSpawnDelayTime;
    [SerializeField] float fruitDestroyTime = 3f;
    [SerializeField] float fruitSpawnDistance = 20f;
    [SerializeField] float fruitDistanceToPlayer;
    [SerializeField] Movement movement;
    private void Start()
    {
        StartCoroutine(CreateFruit(fruitSpawnDelayTime));
    }

    IEnumerator CreateFruit(float nextDelayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(nextDelayTime);
            int r = Random.Range(0, fruits.Length - 1);
            float fruitHeight = (9.81f * fruitSpawnDistance * fruitSpawnDistance) / (2 * movement.forwardSpeed * movement.forwardSpeed); // formula to find height, so that fruit exactly collides with us
            Vector3 pos = playerTransform.position + new Vector3(0f, fruitHeight, fruitSpawnDistance) + Vector3.forward * fruitDistanceToPlayer;
            GameObject fruit = Instantiate(fruits[r], pos, Quaternion.identity);
            Destroy(fruit, fruitDestroyTime);
        }
    }
}
