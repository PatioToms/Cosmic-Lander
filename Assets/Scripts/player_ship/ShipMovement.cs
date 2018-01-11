using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour {

    [SerializeField] float magToApplyDamage;
    [SerializeField] GameObject dummy_ship;
	[SerializeField] AudioClip[] impact_sounds;
    [SerializeField] Transform magnetPivot;
    public CamShakeManager.Properties shakeProperties;
    [SerializeField] float fuelCost;

    [Header("Physics propierties")]
    public float reactorForce;
    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] float rotationSpeedUnderThrust = 150f;
    [SerializeField] float friction;
    [SerializeField] float normalGravity;
    [SerializeField] float gravityUnderThrust;
    [SerializeField] float maximumForwardSpeed;
    [SerializeField] AnimationCurve thrustByForwardSpeed;
    [SerializeField] AnimationCurve frictionByAngle;

    [HideInInspector] public bool is_dead = false;
    [HideInInspector] public bool can_fly = true;
    [HideInInspector] public int numMinerals;
    public bool vuelta;
    AudioSource audio_emitter;
	Rigidbody2D rb;
    ui_manager_script ui_manager;
    float mag = 0;

    public static bool isShipEnabled;

    private void Start ()
    {
		audio_emitter = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D>();
        ui_manager = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        isShipEnabled = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (isShipEnabled)
        {
            float forwardSpeed = Vector3.Project((Vector3)rb.velocity, transform.up).magnitude;
            float angleOfVelocity = Vector3.Angle((Vector3)rb.velocity, transform.up);
            if (angleOfVelocity > 90)
                forwardSpeed = 0;
            float possibleThrust = reactorForce * thrustByForwardSpeed.Evaluate(Mathf.Min(1, forwardSpeed / maximumForwardSpeed));

            if (Input.GetButton("ReactorInput") && can_fly)
            {
                ui_manager.modify_fuel(-fuelCost);
                rb.velocity += possibleThrust * (Vector2)transform.up * Time.deltaTime;
                rb.velocity += -Vector2.up * gravityUnderThrust * Time.deltaTime;
                if (Input.GetKey(KeyCode.A))
                    rb.rotation += rotationSpeedUnderThrust * Time.deltaTime;
                if (Input.GetKey(KeyCode.D))
                    rb.rotation -= rotationSpeedUnderThrust * Time.deltaTime;
            }
            else
            {
                rb.velocity += -Vector2.up * normalGravity * Time.deltaTime;
                if (Input.GetKey(KeyCode.A))
                {
                    rb.rotation += rotationSpeed * Time.deltaTime;
                    rb.freezeRotation = true;
                }
                if (Input.GetKeyUp(KeyCode.A))
                    rb.freezeRotation = false;
                if (Input.GetKey(KeyCode.D))
                {
                    rb.rotation -= rotationSpeed * Time.deltaTime;
                    rb.freezeRotation = true;
                }
                if (Input.GetKeyUp(KeyCode.D))
                    rb.freezeRotation = false;
            }
            float vel = rb.velocity.magnitude;
            vel = Mathf.Lerp(vel, 0, frictionByAngle.Evaluate(angleOfVelocity / 180) * friction * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * vel;
            mag = rb.velocity.magnitude;
        }
    }
		
	private void OnCollisionEnter2D (Collision2D col)
    {
        if ((col.gameObject.tag != "mineral") || (col.gameObject.layer != 9))
            {
            if ((mag >= magToApplyDamage))
            {
                ui_manager.modify_fuel(-.01f * mag/10);
                GameObject.Find("Camera").GetComponent<CamShakeManager>().StartShake(shakeProperties);
                audio_emitter.clip = impact_sounds[Random.Range(0, impact_sounds.Length - 1)];
                audio_emitter.Play();
            }
        }
        if (ui_manager.fuel <= 0)
        {
            DestroyShip();
            StartCoroutine(LoadLevel());
        }
	}

    void DestroyShip()
    {
		GetComponentInChildren<ship_reactor_script> ().SeparateParticles ();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = true;
        //GetComponent<CircleCollider2D>().enabled = true;
        GameObject dummy = Instantiate(dummy_ship, transform.position, transform.rotation);
        dummy.transform.parent = transform;
    }

    IEnumerator LoadLevel()
    {
        DestroyShip();
        Fading fading = GameObject.Find("Game Manager").GetComponent<Fading>();
		yield return new WaitForSeconds(2);
		float fadeTime = fading.BeginFade(1);
		yield return new WaitForSeconds(fadeTime + 0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
}
