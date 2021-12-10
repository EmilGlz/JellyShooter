using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    
    [SerializeField] Transform slimeSpawnPos;
    [SerializeField] GameObject slimePrefab;
    [SerializeField] Transform mainCam;
    [SerializeField] VariableJoystick joystick;
    [SerializeField] ParticleSystem shootVFX;
    [SerializeField] Flaregun flaregun;
    [SerializeField] Material slimeMat;
    [SerializeField] float xRotMax;
    [SerializeField] float xRotMin;
    [SerializeField] float yRotMax;
    [SerializeField] float yRotMin;
    [SerializeField] float slimeForce;
    [SerializeField] float camTurnSensitivity;
    [SerializeField] private Material skyboxMat;
    public Color32[] skyboxColors;
    public Color32[] fogColors;
    [SerializeField] GameObject crossHair;
    [SerializeField] float moveSpeed;
    //[SerializeField] float shootDelay;
    [HideInInspector] public int randColorIndex;
    private float curHor, lastHor = 0, lastVer = 0;
    private float curVer;
    private float deltaHor, deltaVer;
    private System.Random random;
    public bool canShoot = true;
    //float lastShootTime = 0f;
    private void Start()
    {
        crossHair.SetActive(false);
        random = new System.Random();
        randColorIndex = random.Next(skyboxColors.Length);
        skyboxMat.SetColor("_Tint", skyboxColors[randColorIndex]);
        RenderSettings.fogColor = fogColors[randColorIndex];
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.back * moveSpeed * Time.fixedDeltaTime;
        //if (Time.time - lastShootTime >= shootDelay)
        //{
        //    canShoot = true;
        //    lastShootTime = Time.time;
        //}
    }

    public void OnJoystickPointerDown()
    { 
        crossHair.SetActive(true);
    }

    public void OnJoystickDrag()
    {
        curHor = joystick.Horizontal;
        curVer = joystick.Vertical;
        deltaHor = curHor - lastHor;
        deltaVer = curVer - lastVer;
        //Debug.Log("transform.rotation.X: " + transform.rotation.eulerAngles.x);
        float targetYRot = transform.rotation.eulerAngles.y + deltaHor * camTurnSensitivity;
        float targetXRot = mainCam.rotation.eulerAngles.y + deltaVer * -1f * camTurnSensitivity;
        //Debug.Log("X: " + targetXRot);
        transform.Rotate(0f, deltaHor * camTurnSensitivity, 0f);
        //transform.Rotate(0f, deltaHor * camTurnSensitivity, 0f);
        if (targetYRot <= yRotMax && targetYRot >= yRotMin)
        {
        }
        if (targetXRot <= xRotMax && targetXRot >= xRotMin)
        {
        }
        mainCam.transform.Rotate(deltaVer * -1f * camTurnSensitivity, 0f, 0f);
        lastHor = curHor;
        lastVer = curVer;
    }

    public void OnJoystickPointerUp()
    {
        crossHair.SetActive(false);
        lastHor = 0;
        lastVer = 0;
        if (canShoot)
        {
            GameObject slime = Instantiate(slimePrefab, slimeSpawnPos.position, slimeSpawnPos.rotation);
            Color slimeColor = fogColors[randColorIndex];
            slimeColor.a = 0.7f;
            slimeMat.color = slimeColor;
            shootVFX.Play();
            flaregun.Shoot();
            StartCoroutine(ShootBullet(slime));
            canShoot = false;
            MenuLogic.Instance.MakeReloadUIDefault();
            MenuLogic.Instance.StartReloadAnimUI();
        }
    }

    IEnumerator ShootBullet(GameObject slime)
    {
        yield return null;
        slime.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().AddForce(mainCam.forward * slimeForce);
        Destroy(slime, 5f);
    }

}
