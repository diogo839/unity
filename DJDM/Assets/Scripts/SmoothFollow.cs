using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    public static SmoothFollow Instance { get; private set; }

    [SerializeField]
    private Transform targetTransform = null;
    [SerializeField]
    private float smoothTime = 0.5f;
    [SerializeField]
    private Transform topLimitTransform = null;
    [SerializeField]
    private Transform bottomLimitTransform = null;
    [SerializeField]
    private Transform leftLimitTransform = null;
    [SerializeField]
    private Transform rightLimitTransform = null;

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
        SetCameraLimits();
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.L)) {
    //        Shake(0.5f, 3f);
    //    }
    //}

    private void LateUpdate() {
        Vector3 targetPosition = targetTransform.position;
        targetPosition.z = cameraZOffset;


        targetPosition.x = Mathf.Clamp(
            targetPosition.x,
            leftLimit,
            rightLimit
            );

        targetPosition.y = Mathf.Clamp(
            targetPosition.y,
            bottomLimit,
            topLimit
            );
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

    //public void Shake(float duration, float range) {
    //    if (lastShakeCoroutine != null) {
    //        StopCoroutine(lastShakeCoroutine);
    //    }
    //    lastShakeCoroutine = StartCoroutine(DoShake(1f, 0.75f));
    //}

    public void SetCameraLimits() {
        float halfHeight = myCamera.orthographicSize;
        float halfWidth = halfHeight * myCamera.aspect;

        leftLimit = leftLimitTransform.position.x + halfWidth;
        rightLimit = rightLimitTransform.position.x - halfWidth;
        bottomLimit = bottomLimitTransform.position.y + halfHeight;
        topLimit = topLimitTransform.position.y - halfHeight;
    }

    public void SetLeftLimit(Transform left) {
        leftLimitTransform = left;
        SetCameraLimits();
    }

    public void SetRightLimit(Transform right) {
        rightLimitTransform = right;
        SetCameraLimits();
    }

    public void SetTopLimit(Transform top) {
        topLimitTransform = top;
        SetCameraLimits();
    }

    public void SetBottomLimit(Transform bottom) {
        bottomLimitTransform = bottom;
        SetCameraLimits();
    }
}
