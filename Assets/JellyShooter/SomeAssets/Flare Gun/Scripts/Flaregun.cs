using UnityEngine;
using System.Collections;

public class Flaregun : MonoBehaviour {
	
	public Rigidbody flareBullet;
	public Transform barrelEnd;
	//public GameObject muzzleParticles;
	public AudioClip flareShotSound;
	public AudioClip noAmmoSound;	
	public AudioClip reloadSound;	
	public int bulletSpeed = 2000;
	public int maxSpareRounds = 5;
	public int spareRounds = 3;
	public int currentRound = 0;

	public void Shoot()
	{
		currentRound--;
		if (currentRound <= 0)
		{
			currentRound = 0;
		}

		GetComponent<Animation>().Stop( "Shoot");
		GetComponent<Animation>().Play( "Shoot", PlayMode.StopSameLayer);
		//GetComponent<AudioSource>().PlayOneShot(flareShotSound);
	}
	
}
