using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : EnemyBaseBehavior {


    private float speed;
    private float journeyLength;
    private Vector3 startPos;
    private Vector3 endPos;
    private GameObject smallShipPrefab;

    // Use this for initialization
    protected new void Start() {
        base.Start();

        scoreValue = 3;
        healthValue = 2;

        speed = 5;
        startPos = this.transform.position;
        endPos = new Vector3(Random.Range(-20.0f, 20.0f), -1, 0);
        journeyLength = Vector3.Distance(startPos, endPos);
        smallShipPrefab = Resources.Load<GameObject>("Prefabs/enemyShipSmall");
    }
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
        Move();
    }

    public override void Explode() {
        base.Explode();
        for (int i = 0; i < 5; i++)
            SpawnSmallEnemy(this.transform.position);
    }
    
    void Move() {
        float distCovered = (Time.time - creationTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
    }

    void SpawnSmallEnemy(Vector3 pos) {
        Instantiate(smallShipPrefab, pos, Quaternion.identity);
    }
}
