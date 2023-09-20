using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField]
	private TranslationMover mover;

    [SerializeField]
    private HealthPoints healthPoints;

	public TranslationMover Mover => mover;

    private void OnEnable()
    {
        mover.TargetAchieved += OnTargetAchieved;
        healthPoints.HPExpired += OnHealthPointsExpired;
    }

    private void OnDisable()
    {
        healthPoints.HPExpired -= OnHealthPointsExpired;
        mover.TargetAchieved -= OnTargetAchieved;
    }

    private void OnHealthPointsExpired()
    {
        gameObject.SetActive(false);
    }

    private void OnTargetAchieved()
    {
        gameObject.SetActive(false);
    }
}
