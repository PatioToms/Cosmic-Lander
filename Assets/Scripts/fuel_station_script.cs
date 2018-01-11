using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel_station_script : MonoBehaviour {

    [SerializeField]
    float total_fuel;
    [SerializeField]
    Transform gauge_transform;
    [SerializeField]
    ParticleSystem smoke_particles;

    float current_fuel;
    Animator fuel_station_animator;
    ui_manager_script uiScr;

    void Start()
    {
        current_fuel = total_fuel;
        fuel_station_animator = this.GetComponent<Animator>();
        uiScr = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        UpdateGauge();
    }

    private void Update()
    {
        if (current_fuel <= 0)
            smoke_particles.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && current_fuel >= 0)
        {
            fuel_station_animator.SetBool("is_active", true);
            smoke_particles.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && current_fuel >= 0 && uiScr.fuel < 1)
        {
            current_fuel -= .001f;
            uiScr.modify_fuel(.001f);
            UpdateGauge();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && current_fuel >= 0)
        {
            fuel_station_animator.SetBool("is_active", false);
            smoke_particles.Stop();
        }
    }

    void UpdateGauge()
    { //función utilizada para actualizar el marcador de gasolina

        float new_angle = Mathf.Lerp(60, -60, (current_fuel / total_fuel));
        gauge_transform.eulerAngles = new Vector3(0f, 0f, new_angle);

    }
}
