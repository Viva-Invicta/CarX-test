using UnityEngine;

public class Spawner : MonoBehaviour
{
	public float interval = 3;
	public GameObject moveTarget;

	private float lastSpawn = -1;

	void Update () 
	{
		if (Time.time > lastSpawn + interval) 
		{
			var newMonster = GameObject.CreatePrimitive (PrimitiveType.Capsule);
			var r = newMonster.AddComponent<Rigidbody> ();
			r.useGravity = false;
			newMonster.transform.position = transform.position;
			var monsterBeh = newMonster.AddComponent<Monster> ();
			monsterBeh.moveTarget = moveTarget;

			lastSpawn = Time.time;
		}
	}
}
