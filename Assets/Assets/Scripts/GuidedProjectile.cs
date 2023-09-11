using UnityEngine;

public class GuidedProjectile : MonoBehaviour 
{
	public GameObject target;
	public float speed = 0.2f;
	public int damage = 10;

	void Update () 
	{
		if (target == null) 
		{
			Destroy (gameObject);
			return;
		}

		var translation = target.transform.position - transform.position;
		if (translation.magnitude > speed) 
		{
			translation = translation.normalized * speed;
		}
		transform.Translate (translation);
	}

	void OnTriggerEnter(Collider other)
	{
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

		monster.hp -= damage;
		if (monster.hp <= 0) 
		{
			Destroy (monster.gameObject);
		}
		Destroy (gameObject);
	}
}
