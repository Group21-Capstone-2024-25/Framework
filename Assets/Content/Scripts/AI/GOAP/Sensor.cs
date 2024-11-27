using System;
using UnityEngine;
using UnityUtils;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float timerInterval = 1f;

    SphereCollider detectionRange;
    GameObject target;
    Vector3 lastKnownPosition;
    CountdownTimer timer;

    public event Action OnTargetChanged = delegate { };

    public Vector3 TargetPosition => target ? target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;


    private void Awake()
    {
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
        detectionRange.radius = detectionRadius;
    }

    private void Start()
    {
        timer = new CountdownTimer(timerInterval);
        timer.OnTimerStop += () => {
            UpdateTargetPosition(target.OrNull());
            timer.Start();
        };
        timer.Start();
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    private void UpdateTargetPosition(GameObject target = null)
    {
        this.target = target;
        if (IsTargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero))
        {
            lastKnownPosition = TargetPosition;
            OnTargetChanged.Invoke();
        }
    }

}
