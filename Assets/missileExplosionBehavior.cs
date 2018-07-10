using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileExplosionBehavior : MonoBehaviour {

    private float size;

	// Use this for initialization
	void Start () {
        size = 0.5f;
        Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
        //change scale depending on time not frames
        this.transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
        size += 0.01f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (var enemy in enemies) {
            Vector3 enemyPos = enemy.transform.position;
            float distance = Vector3.Distance(enemyPos, transform.position);
            if (distance < size)
                Destroy(enemy);
        }
	}


}
