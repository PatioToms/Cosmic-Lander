using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatObject : MonoBehaviour {

    [SerializeField] bool mustSpawnEggs;
    [SerializeField] float red_value = 0.3686f;
    [SerializeField] float heat_resistance = 0.05f;
    [SerializeField] float explode_delay = 0.4f;
    [SerializeField] SpriteRenderer heatingSpr;
    [SerializeField] Animator animator;
    [SerializeField] GameObject particlesPrefab;
    
    bool is_heating_up = false;
    bool has_blown_up = false;
    Transform oldTransform;

    private void Start()
    {
        oldTransform = transform;
    }

    public void Stop_Heat()
    {
        animator.SetBool("is_heating_up", false);
    }

    public void Heat_Up()
    {
        animator.SetBool("is_heating_up", true);

        red_value += heat_resistance;
        heatingSpr.color = new Color(red_value, 0, 0);
        if (red_value >= 1 && !has_blown_up)
        {
            has_blown_up = true;
            if (mustSpawnEggs)
                gameObject.GetComponent<SpawnEggs>().GiveLoot();
            else
            {
                StartCoroutine(DestroyInflamableObject());
            }

        }
    }

    IEnumerator DestroyInflamableObject()
    {
        animator.SetTrigger("explode");
        yield return new WaitForSeconds(explode_delay);
        Destroy(gameObject);
        Instantiate(particlesPrefab, oldTransform.position, oldTransform.rotation);
        yield return null;
    }
}
