using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an enum of the various possible weapon types.
/// It also includes a *shield* type to allow a shield power-up.
/// Items marked [NI] below are Not Implimented in the IGDPD book.
/// </summary>

public enum WeaponType {
    none,       // the default/no weapon
    blaster,    // a simple blaster
    spread,     // two shots simultaneously
    phaser,     // [NI] shots that move in waves
    missile,    // [NI] homing missiles
    laser,      // [NI] damage over time
    shield      // raise shieldLevel
}

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
