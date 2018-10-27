﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //for loading & reloading of scenes

public class Main : MonoBehaviour {
    static public Main S; //a singleton for Main
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f; //number of Enemies/second
    public float enemyDefaultPadding = 1.5f; //padding for position
    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    void Awake() {
        S = this;
        //set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        //invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        // A generic Dictionary with WeaponType as the key
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions) {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy() {
        //pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null) {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //set the initial posotion for the spawned Enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //Invoke SpawnEnemy again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart (float delay) {
        //Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }

    public void Restart() {
        //reload _Scene_0 to restart the game
        SceneManager.LoadScene("_Scene_0");
    }

    /// <summary>
    /// Static function that gets a WeaponDefinition from the WEAP_DICT static
    /// protected field of the Main class.
    /// </summary>
    /// <returns> The WeaponDefinition or, if there is no WeaponDefinition with
    /// the WeaponType passed in, returns a new WeaponDefinition with a
    /// WeaponType of none. </returns>
    /// <param name="wt">The WeaponType of the desired WeaponDefinition</param>
	
	static public WeaponDefinition GetWeaponDefinition (WeaponType wt) {
        // Check to make sure that they key exists in the Dictionary
        // Attempting to retrieve a key that didn't exist, would throw an error,
        // so the following if statement is important.
        if (WEAP_DICT.ContainsKey(wt)) {
            return (WEAP_DICT[wt]);
        }
        // This returns a new WeaponDefinition with a type of WeaponType.none,
        // which means it has failed to find the right WeaponDefinition
        return (new WeaponDefinition());
    }
}
