using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_Ship_Trigger : MonoBehaviour {

    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "mineral")
            parent.transform.parent = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "mineral")
            parent.transform.parent = null;
    }
}
