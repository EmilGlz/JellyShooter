using UnityEngine;

public class SlimeDetector : MonoBehaviour
{
    public Slime slime;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HMN"))
        {
            if (collision.gameObject.GetComponent<Human>() != null)
            {
                if (collision.gameObject.GetComponent<Human>().isAlive)
                {
                    slime.myHumanCount++;
                    if (slime.myHumanCount > CommonData.Instance.maxHumanCountPerBall)
                    {
                        CommonData.Instance.maxHumanCountPerBall = slime.myHumanCount;
                        MenuLogic.Instance.SetCombo(slime.myHumanCount);
                    }
                }
            }
        }
        else if (collision.gameObject.layer == 8) // ground
        {
            if (!slime.touchedTheGround) // if it is fist time touching the ground
            {
                int r = Random.Range(0, 4);
                AudioManager.Instance.Play(r);
                slime.touchedTheGround = true;
            }
        }
    }
}
