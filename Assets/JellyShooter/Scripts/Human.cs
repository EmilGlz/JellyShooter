using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    bool canMove;
    public bool isAlive = true;
    [SerializeField] float humanMoveSpeed;
    [SerializeField] Animator anim;

    public Collider[] characterPartColliders;
    public Rigidbody[] characterPartsRigidbodies;

    public Rigidbody humanHips;

    private void Start()
    {
        canMove = true;
        characterPartColliders = GetComponentsInChildren<Collider>();
        characterPartsRigidbodies = GetComponentsInChildren<Rigidbody>();
        anim.Play("Fast Run");
    }


    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.position += transform.forward * humanMoveSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6) // collision with slime
        {
            if (isAlive)
            {
                canMove = false;
                isAlive = false;
                int r = Random.Range(0, 4);
                AudioManager.Instance.Play(r);
                CommonData.Instance.scoreWrittenInAnimCoin++;
                if (MenuLogic.Instance.animatingCoindImage.gameObject.activeInHierarchy)
                {
                    LeanTween.scale(MenuLogic.Instance.animatingCoindImage, Vector3.one * 1.5f, 0.6f).setEasePunch();
                }
                MenuLogic.Instance.animCoinText.text = "+ " + CommonData.Instance.scoreWrittenInAnimCoin.ToString();
                MenuLogic.Instance.StartCoinAnim(transform);
                SetRagdoll(true);
            }
        }
    }

    public void SetRagdoll(bool isRagdoll)
    {
        anim.enabled = false;
        for (int i = 0; i < characterPartColliders.Length; i++) // 14 colliders and 14 rigidbodies
        {
            if (characterPartColliders[i].gameObject != this.gameObject) // if colliders are only child's colliders
            {
                characterPartColliders[i].isTrigger = !isRagdoll;
            }
            else // if it is our collider, capsule collider
            {
                characterPartColliders[i].isTrigger = isRagdoll;
            }
        }
        for (int i = 0; i < characterPartsRigidbodies.Length; i++) // 14 colliders and 14 rigidbodies
        {
            if (characterPartsRigidbodies[i].gameObject != this.gameObject) // if children
            {
                characterPartsRigidbodies[i].useGravity = isRagdoll;
            }
            else // if it is parent
            {
                characterPartsRigidbodies[i].useGravity = !isRagdoll;
            }
        }
    }

    private void OnDestroy()
    {
        CommonData.Instance.currentKilledHumanCount++;
        if (CommonData.Instance.currentKilledHumanCount == CommonData.Instance.allHumansCount) // this is the last human
        {
            MenuLogic.Instance.GameWin();
        }
    }
}
