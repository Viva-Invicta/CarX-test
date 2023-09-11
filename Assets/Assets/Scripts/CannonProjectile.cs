using UnityEngine;

public class CannonProjectile : MonoBehaviour 
{
	public float speed = 0.2f;
	public int damage = 10;

	void Update ()
	{
		var translation = transform.forward * speed;
		transform.Translate(translation);
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
