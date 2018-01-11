using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMagnet : MonoBehaviour {

	[SerializeField] ParticleSystem magnet_particle;
    [SerializeField] Transform pivot;
    [SerializeField] float fuelCost;
    
	private ui_manager_script ui_manager;
    private bool inputDown;
	private AudioSource audio_emitter;

    private void Start()
	{
        ui_manager = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
		audio_emitter = this.GetComponent<AudioSource> ();
    }

	private void Update()
	{
		if (Input.GetButtonDown("MagnetInput") && (ui_manager.fuel > 0))
        {
            magnet_particle.Play();
			audio_emitter.Play ();
            inputDown = true;
        }

		/*if (Input.GetButton ("MagnetInput") && (ui_manager.fuel > 0)) {
			
			ui_manager.modify_fuel (-fuelCost);
		}*/

		if (Input.GetButtonUp("MagnetInput") || (ui_manager.fuel <= 0))
        {
            magnet_particle.Stop();
			audio_emitter.Stop ();
            inputDown = false;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 && inputDown)
        {
            collision.gameObject.GetComponent<MineralAttraction>().EnableAttraction(pivot);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 && !inputDown)
        {
            collision.gameObject.GetComponent<MineralAttraction>().DisableAttraction();
        }
    }
}
