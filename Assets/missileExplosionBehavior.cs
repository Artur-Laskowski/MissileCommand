using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileExplosionBehavior : MonoBehaviour {

    private float size;

    private float startTime;

    private float duration;
    private float speed;
    private float collapseRatio;

	// Use this for initialization
	void Start () {
        size = 0.5f;
        startTime = Time.time;
        speed = 0.5f;
        duration = 1.0f;
        collapseRatio = 2.0f / 4.0f;
        Destroy(this.gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
        //change scale depending on time not frames
        if (Time.time - startTime < duration * collapseRatio) {
            this.transform.localScale += new Vector3(1.0f, 1.0f, 0.0f) * speed;
            size += speed / 10.0f;
        }
        else {
            this.transform.localScale -= new Vector3(1.0f, 1.0f, 0.0f) * speed;
            size -= speed / 10.0f;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (var enemy in enemies) {
            Vector3 enemyPos = enemy.transform.position;
            float distance = Vector3.Distance(enemyPos, transform.position);
            if (distance < size)
                Destroy(enemy);
        }
	}


}
