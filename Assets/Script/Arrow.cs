using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float maxSpeed = 10f;
    private float lifeTime = 5f;
    private Vector2 movoment;
    private Archer archer;
    private Vector2 direction;
    private Vector3 trajectorRange;

    private Vector3 trajectoryStartPosion;
    private Vector3 projectileMoveDir;

    private Transform playerTransform;
    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;
    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;
    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve projectileSpeedAnimationCurve;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        archer = FindAnyObjectByType<Archer>();

        playerTransform = archer.TakeVisitPlayer();
        trajectoryStartPosion = transform.position;
        trajectoryAnimationCurve = archer.trajectoryAnimationCurve;
        axisCorrectionAnimationCurve = archer.axisCorrectionAnimationCurve;
        projectileSpeedAnimationCurve = archer.projectileSpeedAnimationCurve;

    }
    private void Update()
    {
        UpdateProjcetilePosition();
    }
    private void UpdateProjcetilePosition()
    {
        trajectorRange = playerTransform.position - trajectoryStartPosion;

        if(Mathf.Abs(trajectorRange.normalized.x) < Mathf.Abs(trajectorRange.normalized.y))
        {
            if (trajectorRange.y < 0)
            {
                speed = -speed;
            }
            UpdatePositionWithXCurve();
        }
        else
        {
            if (trajectorRange.x < 0)
            {
                speed = -speed;
            }
            UpdatePositionWithYCurve();
        }
        
    }
    private void UpdatePositionWithYCurve()
    {
        float nextPositionX = transform.position.x + Time.deltaTime * speed;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPosion.x) / trajectorRange.x;

        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalized + archer.projectileMaxHeight;

        float nextCorrectionPositionYNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextCorrectionPositionYNormalized * trajectorRange.y;

        float nextPositionY = trajectoryStartPosion.y + nextPositionYNormalized * archer.projectileMaxHeight + nextPositionYCorrectionAbsolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CaculateNextProjectileSpeed(nextPositionXNormalized);
        projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void UpdatePositionWithXCurve()
    {
        // Move primarily along Y, compute X from curves (mirrors UpdatePositionWithYCurve)
        float nextPositionY = transform.position.y + Time.deltaTime * speed;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPosion.y) / trajectorRange.y;

        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalized + archer.projectileMaxHeight;

        float nextCorrectionPositionXNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextCorrectionPositionXNormalized * trajectorRange.x;

        if (trajectorRange.x > 0 && trajectorRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }
        if (trajectorRange.x < 0 && trajectorRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextPositionX = trajectoryStartPosion.x + nextPositionXNormalized + nextPositionXCorrectionAbsolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0f);

        CaculateNextProjectileSpeed(nextPositionYNormalized);
        projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }
    private void CaculateNextProjectileSpeed(float nextPositionXNormalized)
    {
        float nextMoveSpeedNormalized = archer.projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        speed = nextMoveSpeedNormalized * maxSpeed;

    }
    public Vector3 GetProjectileMoveDir()
    {
        return projectileMoveDir;
    }
    public float GetNextYTrajectoryPosition()
    {
        return nextYTrajectoryPosition;
    }
    public float GetNextPositionYCorrectionAbsolute()
    {
        return nextPositionYCorrectionAbsolute;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventHub.TriggerHealthChange(-archer.damage);
            Destroy(gameObject);
        }

    }
}
