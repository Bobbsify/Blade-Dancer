using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        TryGetComponent(out anim);
    }

    public void StartShake() 
    {
        anim.SetTrigger("shake");
    }
}





/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Camera Shake")]

    [SerializeField]
    [Range(0f, 3f)]
    private float xMovement = 1;

    [SerializeField]
    [Range(0f, 3f)]
    private float yMovement = 0;

    [SerializeField]
    [Range(0f, 3f)]
    private float zMovement = 1;

    [SerializeField]
    private float shakeDuration = 2.0f;

    private Vector3 currentMovement = Vector3.zero;
    private Vector3 initialPosition;

    private float frameFrequency = 10f;
    private float shakeFrame;

    private void Awake()
    {
        shakeFrame = shakeDuration / frameFrequency / shakeDuration;
    }

    public void StartShake()
    {
        shakeDuration /= 4;
        initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        StartCoroutine(ShakeRoutine());
    }
    private IEnumerator ShakeRoutine()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < shakeDuration) { 
            float x = Mathf.Lerp(initialPosition.x, initialPosition.x + xMovement, timeElapsed / shakeDuration);
            float y = Mathf.Lerp(initialPosition.y, initialPosition.y + yMovement, timeElapsed / shakeDuration);
            float z = Mathf.Lerp(initialPosition.z, initialPosition.z - zMovement, timeElapsed / shakeDuration);

            this.transform.position = new Vector3(x,y,z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ResetToCenter());
    }

    private IEnumerator ResetToCenter(bool mirror = false)
    {
        float timeElapsed = 0.0f;
        Vector3 currentPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        while (timeElapsed < shakeDuration)
        {
            float x = Mathf.Lerp(currentPos.x, initialPosition.x, timeElapsed / shakeDuration);
            float y = Mathf.Lerp(currentPos.y, initialPosition.y, timeElapsed / shakeDuration);
            float z = Mathf.Lerp(currentPos.z, initialPosition.z, timeElapsed / shakeDuration);

            this.transform.position = new Vector3(x, y, z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!mirror)
        {
            StartCoroutine(ShakeRoutineMirror());
        }
        else 
        {
            shakeDuration *= 4;
        }
    }

    private IEnumerator ShakeRoutineMirror()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < shakeDuration)
        {
            float x = Mathf.Lerp(initialPosition.x, initialPosition.x - xMovement, timeElapsed / shakeDuration);
            float y = Mathf.Lerp(initialPosition.y, initialPosition.y + yMovement, timeElapsed / shakeDuration);
            float z = Mathf.Lerp(initialPosition.z, initialPosition.z - zMovement, timeElapsed / shakeDuration);

            this.transform.position = new Vector3(x, y, z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ResetToCenter(true));
    }

}
*/