using System.Collections;
using UnityEngine;

public class ComicRoomCamera : MonoBehaviour
{
    public Transform targetPoint;
    public float moveDuration = 2f;

    private void Start()
    {
        StartCoroutine(WaitAndSubscribe());
    }

    IEnumerator WaitAndSubscribe()
    {
        while (ComicManager.comicManager == null)
            yield return null;

        ComicManager.comicManager.onRequestMoveCamera += StartMove;
    }

    public void StartMove()
    {
        StartCoroutine(MoveCameraToTarget());
    }

    IEnumerator MoveCameraToTarget()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 endPos = targetPoint.position;
        Quaternion endRot = targetPoint.rotation;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;

        ComicManager.comicManager.ResumeTypingAfterCameraMove();
    }
}
