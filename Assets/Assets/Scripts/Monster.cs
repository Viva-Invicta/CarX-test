using UnityEngine;

public class Monster : MonoBehaviour {

	public GameObject moveTarget;

	public float speed = 0.1f;
	public int maxHP = 30;
	const float reachDistance = 0.3f;

	public int hp;

	void Start() 
	{
		hp = maxHP;
	}

	void Update () 
	{
		if (moveTarget == null)
			return;
		
		if (Vector3.Distance (transform.position, moveTarget.transform.position) <= reachDistance)
		{
			Destroy (gameObject);
			return;
		}

		var translation = moveTarget.transform.position - transform.position;
		if (translation.magnitude > speed)
		{
			translation = translation.normalized * speed;
		}
		transform.Translate (translation);
	}
}
