using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    public static SmoothFollow Instance { get; private set; }

    [SerializeField]
    private Transform targetTransform = null;
    [SerializeField]
    private float smoothTime = 0.5f;

    private float cameraZOffset = 0f;
    private Vector3 cameraVelocity;

    private Vector3 lastOffsetPosition = Vector3.zero;
    private Coroutine lastShakeCoroutine = null;

    private float topLimit;
    private float bottomLimit;
    private float leftLimit;
    private float rightLimit;

    private Camera myCamera = null;

    private void Awake() {

        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        cameraZOffset = transform.position.z;

        myCamera = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            Shake(0.5f, 3f);
        }
    }

    private void LateUpdate() {
        Vector3 targetPosition = targetTransform.position;
        targetPosition.z = cameraZOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref cameraVelocity,
            smoothTime);
    }


    public void SetTarget(Transform newTargetTransform) {
        targetTransform = newTargetTransform;
    }

    private IEnumerator DoShake(float duration, float range) {

        while (duration > 0) {
            transform.localPosition -= lastOffsetPosition;
            lastOffsetPosition = Random.insideUnitCircle * range;
            lastOffsetPosition.z = 0;
            transform.localPosition += lastOffsetPosition;

            if (duration < 0.5f) {
                range *= 0.90f;
            }

            duration -= Time.deltaTime;
            yield return null;
        }
    }

    public void Shake(float duration, float range) {
        if (lastShakeCoroutine != null) {
            StopCoroutine(lastShakeCoroutine);
        }
        lastShakeCoroutine = StartCoroutine(DoShake(1f, 0.75f));
    }
}
