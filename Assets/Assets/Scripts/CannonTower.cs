using UnityEngine;

public class CannonTower : MonoBehaviour
{
	public float shootInterval = 0.5f;
	public float range = 4f;
	public GameObject projectilePrefab;
	public Transform shootPoint;

	private float lastShotTime = -0.5f;

	void Update ()
	{
		if (projectilePrefab == null || shootPoint == null)
			return;

		foreach (var monster in FindObjectsOfType<Monster>())
		{
			if (Vector3.Distance (transform.position, monster.transform.position) > range)
				continue;

			if (lastShotTime + shootInterval > Time.time)
				continue;

			// shot
			Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

			lastShotTime = Time.time;
		}

	}
}
