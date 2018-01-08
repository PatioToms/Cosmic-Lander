using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ToxicCloud : MonoBehaviour {

    [SerializeField] float vignetteSpeed;
    [SerializeField] float delayDamage;
    [SerializeField] float damage;
    [SerializeField] InmunityBubbleController bubbleScr;
    [SerializeField] PostProcessingProfile profile;

    float timer;
    bool playerIn;
    ui_manager_script uiScr;
    float oldDelayDamage;

    private void Start()
    {
        var vignette = profile.vignette.settings;
        vignette.intensity = 0;
        profile.vignette.settings = vignette;
        uiScr = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        oldDelayDamage = delayDamage;
    }

    private void Update()
    {
        if (playerIn && bubbleScr.playerProtected == false)
        {
            var vignette = profile.vignette.settings;
            vignette.intensity = Mathf.Clamp01(vignette.intensity + Time.deltaTime * vignetteSpeed);
            profile.vignette.settings = vignette;
        }
        if (playerIn == false || bubbleScr.playerProtected == true)
        {
            var vignette = profile.vignette.settings;
            vignette.intensity = Mathf.Clamp01(vignette.intensity - Time.deltaTime * vignetteSpeed);
            profile.vignette.settings = vignette;
        }
        if (profile.vignette.settings.intensity > .5f)
        {
            delayDamage -= Time.deltaTime;
            if(delayDamage <= 0)
            {
                uiScr.ApplyDamage(-damage);
                delayDamage = oldDelayDamage;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIn = false;
    }
}
