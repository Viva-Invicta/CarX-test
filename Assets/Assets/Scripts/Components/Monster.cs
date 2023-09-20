using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField]
	private TranslationMover mover;

	public TranslationMover Mover => mover;

    private void OnEnable()
    {
        mover.TargetAchieved += OnTargetAchieved;
    }

    private void OnDisable()
    {
        mover.TargetAchieved -= OnTargetAchieved;
    }

    private void OnTargetAchieved()
    {
        gameObject.SetActive(false);
    }
}
