using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_manager_script : MonoBehaviour {

	[HideInInspector] public float score = 0;
    [HideInInspector] public int mineralCount;

    [SerializeField] public float fuel;
    [SerializeField] RectTransform fuelBar;
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
        anim.SetInteger("numEggs", player_movement_script.numMinerals);
    }

    public void SetUIEggs(int num)
    {
        anim.SetInteger("numEggs", num);
    }
}
