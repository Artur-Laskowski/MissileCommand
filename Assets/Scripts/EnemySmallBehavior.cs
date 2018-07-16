using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallBehavior : EnemyBaseBehavior {

    private float destructibleAfter;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        scoreValue = 1;
        healthValue = 1;

        destructibleAfter = Settings.Instance.EnemySmallDestructibleAfter;
        
        Vector2 force = new Vector2(Random.Range(-100, 100), Random.Range(100, 300));
        var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(force);
        rigidBody.AddTorque(Random.Range(-300, 300));
    }
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
    }

    public override void Explode() {
        if (IsDestructible()) {
            base.Explode();
        }
    }

    public override bool IsDestructible() {
        return Time.time > creationTime + destructibleAfter;
    }
}
