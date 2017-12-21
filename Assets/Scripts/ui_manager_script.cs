using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_manager_script : MonoBehaviour {

	[HideInInspector] public float score = 0;
    [HideInInspector] public int mineralCount;

    [SerializeField] public float fuel;
    [SerializeField] RectTransform fuelBar;
    [SerializeField] float UIShakeMax;
    [SerializeField] float UIShakeMin;
    [SerializeField] float UIShakeTime;
    [SerializeField] Animator anim;
    [SerializeField] Text scoreWord;

    bool can_fly_proxy = true;
    RectTransform fuelRect;
    ShipMovement player_movement_script;
	ship_reactor_script player_reactor_script;
    Vector3 oldScale;

    void Start ()
    {
		player_movement_script = GameObject.Find ("obj_player_ship").GetComponent<ShipMovement> ();
		player_reactor_script = GameObject.Find ("obj_player_ship").GetComponentInChildren<ship_reactor_script> ();
        fuel = fuelBar.localScale.y;
    }

    private void Update()
    {

    }

    public void modify_score (float quantity){

		score += quantity;
        scoreWord.text = score.ToString ();
		print ("added "+ quantity +" to score");

	}

	public void modify_fuel (float quantity)
    {
	    fuel += quantity;
        if (fuel <= 0)
        {
		    if (can_fly_proxy)
            {
		        can_fly_proxy = false;
                ShipMovement.isShipEnabled = false;
		        player_movement_script.can_fly = can_fly_proxy;
		        player_movement_script.is_dead = !can_fly_proxy;
		        player_reactor_script.can_fly = can_fly_proxy;
		    }
		} 
		else if ((fuel > 0) && (!can_fly_proxy))
        {
		    can_fly_proxy = true;
		    player_movement_script.can_fly = can_fly_proxy;
		    player_movement_script.is_dead = !can_fly_proxy;
		    player_reactor_script.can_fly = can_fly_proxy;
		}
        Mathf.Clamp(fuel, 0, 1);
        fuelBar.localScale = new Vector3(fuelBar.localScale.x, fuel, fuelBar.localScale.z);
    }
	
    public void ApplyDamage (float dmg)
    {
        GameObject.Find("Camera").GetComponent<CamShakeManager>().StartShake(player_movement_script.shakeProperties);
        modify_fuel(dmg);
    }

    public void AddEgg()
    {
        switch (player_movement_script.numMinerals)
        {
            case 0:
                anim.SetBool("Empty", true);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", false);
                break;
            case 1:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", true);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", false);
                break;
            case 2:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", true);
                anim.SetBool("3 eggs", false);
                break;
            case 3:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", true);
                break;
        }
    }

    public void ThrowEgg()
    {
        switch (player_movement_script.numMinerals)
        {
            case 0:
                anim.SetBool("Empty", true);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", false);
                break;
            case 1:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", true);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", false);
                break;
            case 2:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", true);
                anim.SetBool("3 eggs", false);
                break;
            case 3:
                anim.SetBool("Empty", false);
                anim.SetBool("1 egg", false);
                anim.SetBool("2 eggs", false);
                anim.SetBool("3 eggs", true);
                break;
        }
    }
}
