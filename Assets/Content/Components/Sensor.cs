using System;
using UnityEngine;
using UnityUtils;

/// <summary>
/// Sensor
/// </summary>

[RequireComponent (typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float timerInterval = 1f;

    SphereCollider detectionRange;
    
    public event Action OnTargetChanged = delegate { };

    public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;

    GameObject target;
    Vector3 lastknownPosition;
    CountdownTimer timer;

    private void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Awake()
    {
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
        detectionRange.radius = detectionRadius;
    }

    private void Start()
    {
        timer = new CountdownTimer(timerInterval);
        timer.OnTimerStop += () =>
        {
            UpdateTargetPosition(target.OrNull());
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PLayer")) return;
        UpdateTargetPosition(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        UpdateTargetPosition();
    }

    private void UpdateTargetPosition(GameObject target = null)
    {
        this.target = target;
        if (IsTargetInRange && (lastknownPosition != TargetPosition || lastknownPosition != Vector3.zero))
        {
            lastknownPosition = TargetPosition;
            OnTargetChanged.Invoke();
        }
    }
}
