using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallBehavior : EnemyBehavior {

    private float creationTime;
    private float destructibleAfter;

	// Use this for initialization
	void Start () {
        scoreHandler = ScoreHandler.Instance;
        creationTime = Time.time;
        destructibleAfter = Settings.Instance.EnemySmallDestructibleAfter;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsCollidingWithGround()) {
            DecreaseHealth();
            DestroyEnemy();
        }
    }

    public override void ExplodeEnemy() {
        if (scoreHandler == null)
            return;

        if (IsDestructible()) {
            DestroyEnemy();
            scoreHandler.ChangeScore(+1);
        }
    }

    public override bool IsDestructible() {
        return Time.time > creationTime + destructibleAfter;
    }
}
