using UnityEngine;
using System.Collections;

public class Vollyball : MonoBehaviour 
{
	public AudioClip teleportSound;
	public bool teleport = false;
	
	void OnCollisionEnter (Collision coll)
	{
		if (teleport == false)
			return;

		transform.position = Camera.main.transform.position + new Vector3(0f,10f,0f);
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		if (teleportSound != null)
		{
			AudioSource.PlayClipAtPoint(teleportSound, Camera.main.transform.position);
		}
	}
}
