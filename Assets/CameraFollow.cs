﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update () {

        transform.position = target.position + offset;
	
	}
}
