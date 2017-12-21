using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPlatform : MonoBehaviour {

    [SerializeField] Transform ship;
    [SerializeField] Transform destination;
    [SerializeField] Animator animUI;

    ShipMovement shipScr;
    ui_manager_script uiScr;
    Animator anim;
    bool transition;

    private void Start()
    {
        shipScr = GameObject.Find("obj_player_ship").GetComponent<ShipMovement>();
        uiScr = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transition)
        {
            Vector2 direction = destination.position - ship.position;
            ship.Translate(direction * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
