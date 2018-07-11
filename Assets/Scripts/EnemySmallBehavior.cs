using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallBehavior : EnemyBehavior {

	// Use this for initialization
	void Start () {
        scoreHandler = ScoreHandlerBehavior.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsCollidingWithGround()) {
            DecreaseHealth();
            DestroyEnemy();
        }
    }

    public override void ExplodeEnemy() {
        DestroyEnemy();
        scoreHandler.ChangeScore(+1);
    }
}
