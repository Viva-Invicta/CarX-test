using UnityEngine;

public class SimpleTower : MonoBehaviour 
{
	public float shootInterval = 0.5f;
	public float range = 4f;
	public GameObject projectilePrefab;

	private float lastShotTime = -0.5f;
	
	void Update () 
	{
		if (projectilePrefab == null)
			return;

		foreach (var monster in FindObjectsOfType<Monster>()) 
		{
			if (Vector3.Distance (transform.position, monster.transform.position) > range)
				continue;

			if (lastShotTime + shootInterval > Time.time)
				continue;

            // shot
            var projectile = Instantiate(projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            var projectileBeh = projectile.GetComponent<GuidedProjectile>();
			projectileBeh.target = monster.gameObject;

			lastShotTime = Time.time;
		}
	
	}
}
