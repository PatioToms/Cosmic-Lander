using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour {

    [SerializeField] float countDonw;

    private void Start()
    {
        Destroy(gameObject, countDonw);
    }

}
