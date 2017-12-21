using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    public Transform target;
    
	[SerializeField] float offsetY = 0f;
	[SerializeField] float followSpeed = 3f;

    Rigidbody2D playerBody;
    Camera cam;

    private string currentTarget;

    private void Start()
    {
        currentTarget = target.gameObject.name;
        playerBody = target.GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
    }

    [SerializeField]
    float speedAdjustment;
    Vector3 currentSpeedAdjustment;


    [SerializeField]
    float maxSize;
    [SerializeField]
    float minSize;
    [SerializeField]
    float maximumZoomSpeed;

    float wantedOrtographicSize;
    [SerializeField]
    float cameraShake = 0.25f;

    void LateUpdate()
    {
        currentSpeedAdjustment = Vector3.Lerp(currentSpeedAdjustment, speedAdjustment * playerBody.velocity, Time.deltaTime * followSpeed*2);
        wantedOrtographicSize = Mathf.Lerp(minSize, maxSize, currentSpeedAdjustment.magnitude / (speedAdjustment * maximumZoomSpeed));


        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, wantedOrtographicSize, Time.deltaTime * followSpeed/2);

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + offsetY, transform.position.z) + currentSpeedAdjustment, Time.deltaTime * followSpeed);


        /*if (Vector3.Project(currentSpeedAdjustment/ speedAdjustment, target.up).magnitude >= maximumZoomSpeed * 0.99f && Vector3.Angle(currentSpeedAdjustment, target.up) < 90)
            transform.position += (Vector3)Random.insideUnitCircle * cameraShake;
        /*else
        {
            Debug.LogError("Null target");
        }*/
    }
}
