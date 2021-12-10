using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject humanPrefab;
    [SerializeField] int maxRightRoadPos;
    [SerializeField] float humanSpawnDelayTime;
    [SerializeField] int minSpawnDistanceToCamera;
    [SerializeField] int maxSpawnDistanceToCamera;

    public bool canSpawnHuman;

    [SerializeField] int spawnedHumanCount = 0;

    private void Start()
    {
        CommonData.Instance.GetDatas();
        MenuLogic.Instance.UpdateUI();
        StartCoroutine(CreateFruit(humanSpawnDelayTime));
    }

    IEnumerator CreateFruit(float nextDelayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(nextDelayTime);
            if (spawnedHumanCount < CommonData.Instance.allHumansCount)
            {
                if (canSpawnHuman)
                {
                    MenuLogic.Instance.levelFill.fillAmount = (float)spawnedHumanCount / CommonData.Instance.allHumansCount;
                    spawnedHumanCount++;
                    int distance = Random.Range(minSpawnDistanceToCamera, maxSpawnDistanceToCamera + 1);
                    int xPos = Random.Range(-maxRightRoadPos, maxRightRoadPos + 1);
                    Vector3 spawnPos = new Vector3(xPos, 0.5f, playerTransform.position.z + distance);
                    GameObject human = Instantiate(humanPrefab, spawnPos, Quaternion.Euler(0, 180, 0));
                    Destroy(human, 7f);
                }
            }
        }
    }
}