using UnityEngine;

public class Archer : EnemyController
{

    [Header("Archer Settings")]
    [Tooltip("Prefab of the arrow to be instantiated when shooting.")]
    //public float arrowSpeed = 10f;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float projectileMaxHeight = 10f;
    public AnimationCurve trajectoryAnimationCurve;
    public AnimationCurve axisCorrectionAnimationCurve;
    public AnimationCurve projectileSpeedAnimationCurve;
    public void Shoot()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }

}
