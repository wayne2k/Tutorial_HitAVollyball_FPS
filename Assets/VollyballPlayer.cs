using UnityEngine;
using System.Collections;

public class VollyballPlayer : MonoBehaviour
{
	public GameObject vollyball;

	public GameObject effectsPrefab;

	void Start ()
	{
//		Cursor.visible = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TryHitBall();
		}
	}

	void TryHitBall ()
	{
		float power = GetPower();
		if (power > 0f)
		{
			Rigidbody rb = vollyball.GetComponent<Rigidbody>();
			rb.velocity = GetReflected() * power;

			if (effectsPrefab != null)
			{
				GameObject fireball = Instantiate(effectsPrefab) as GameObject;
				fireball.transform.position = vollyball.transform.position;
				Destroy(fireball,2f);
			}
		}
	}

	Vector3 GetReflected ()
	{
		Vector3 vollyballVector = transform.position - vollyball.transform.position;
		Vector3 planeTangent = Vector3.Cross(vollyballVector, Camera.main.transform.forward);
		Vector3 planeNormal = Vector3.Cross(planeTangent, vollyballVector);
		Vector3 reflected = Vector3.Reflect(Camera.main.transform.forward, planeNormal);


		Debug.DrawRay (vollyball.transform.position, vollyballVector, Color.red, 60f);
		Debug.DrawRay (vollyball.transform.position, planeTangent, Color.yellow, 60f);
		Debug.DrawRay (vollyball.transform.position, planeNormal, Color.green, 60f);
		Debug.DrawRay (vollyball.transform.position, reflected, Color.blue, 60f);

		return reflected.normalized;
	}

	float GetPower ()
	{
		float idealDistance = 3f;
		float maxPower = 10;

		float x = Vector3.Distance(vollyball.transform.position, transform.position);
		float y = -Mathf.Abs(x - idealDistance)/3 + 1;

		float power = y * maxPower;
		power = Mathf.Clamp(power, 0f, maxPower);

		return power;
	}
}
