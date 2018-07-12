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
