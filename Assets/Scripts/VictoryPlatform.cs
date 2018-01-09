using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPlatform : MonoBehaviour {

    [SerializeField] float delayToDeployEgg;
    [SerializeField] Transform destination;
    [SerializeField] Animator animUI;

    ShipMovement shipScr;
    ui_manager_script uiScr;
    Animator anim;
    bool transition;
    bool playerLanded;
    float oldDelay;

    private void Start()
    {
        shipScr = GameObject.Find("obj_player_ship").GetComponent<ShipMovement>();
        uiScr = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        anim = GetComponent<Animator>();
        oldDelay = delayToDeployEgg;
    }

    private void Update()
    {
        if (playerLanded)
        {
            delayToDeployEgg -= Time.deltaTime;
            if (delayToDeployEgg <= 0 && shipScr.numMinerals > 0)
            {
                shipScr.numMinerals -= 1;
                uiScr.SetUIEggs(shipScr.numMinerals);
                delayToDeployEgg = oldDelay;
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (shipScr.vuelta && uiScr.score > 0)
            {
                shipScr.enabled = false;
                ship.parent = destination;
                transition = true;
                anim.SetBool("PlatformUp", true);
                StartCoroutine(LoadLevel("Lvl_Playtesting"));
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerLanded = true;
        delayToDeployEgg = oldDelay;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerLanded = false;
    }

    IEnumerator LoadLevel(string name)
    {
        animUI.SetBool("Permiso", true);
        yield return new WaitForSeconds(3);
        Fading fading = GameObject.Find("Game Manager").GetComponent<Fading>();
        fading.BeginFade(1);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(name);
        yield return null;
    }

}
