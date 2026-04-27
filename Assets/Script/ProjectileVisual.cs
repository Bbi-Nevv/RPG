using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    public Transform projectileVisual;
    public Transform projectileShadow;
    public Arrow arrow;

    private Archer archer;
    private Vector3 trajectorStartPosition;
    private Transform playerTransform;
    private float shadowPositionYDidiver = 6f;
    private void Start()
    {
        archer = FindAnyObjectByType<Archer>();
        playerTransform = archer.TakeVisitPlayer();
        trajectorStartPosition = transform.position;
    }
    private void Update()
    {
        UpdateProjectileRotation();
        UpdateProjectileShadow();

        float trajectoryProgressMagnitude = (transform.position - trajectorStartPosition).magnitude;
        float trajectoryMagitude = (playerTransform.position - trajectorStartPosition).magnitude;

        float trajectoryProgressNormalized = trajectoryProgressMagnitude / trajectoryMagitude;

        if (trajectoryProgressNormalized < .7f)
        {
            UpdateProjectileShadowRotation();
        }
    }
    private void UpdateProjectileShadow()
    {
        if (arrow == null) return;

        Vector3 newPosition = transform.position;
        newPosition.y = trajectorStartPosition.y + arrow.GetNextYTrajectoryPosition() / shadowPositionYDidiver + arrow.GetNextPositionYCorrectionAbsolute();
        projectileShadow.position = newPosition;
    }
    private void UpdateProjectileRotation()
    {
        if (arrow == null) return;
        Vector3 projectileMoveDir = arrow.GetProjectileMoveDir();

        float angle = Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg;
        projectileVisual.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void UpdateProjectileShadowRotation()
    {
        if (arrow == null) return;
        Vector3 projectileMoveDir = arrow.GetProjectileMoveDir();

        float angle = Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg;
        projectileShadow.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
