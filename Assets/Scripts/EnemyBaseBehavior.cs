using UnityEngine;
using System.Collections;

public class EnemyBaseBehavior : MonoBehaviour {

    protected float creationTime;
    protected int scoreValue;
    protected int healthValue;

    // Use this for initialization
    protected void Start() {
        creationTime = Time.time;

        scoreValue = 3;
        healthValue = 2;
    }

    // Update is called once per frame
    protected void Update() {
        if (IsCollidingWithGround()) {
            DecreaseHealth();
            DestroyEnemy();
        }
    }

    protected bool IsCollidingWithGround() {
        return this.transform.position.y < 0;
    }

    public virtual bool IsDestructible() {
        return true;
    }

    protected void DestroyEnemy() {
        Destroy(this.gameObject);
    }

    public virtual void Explode() {
        DestroyEnemy();

        ScoreHandler.Instance.ChangeScore(scoreValue);
    }

    protected void DecreaseHealth() {
        ScoreHandler.Instance.ChangeHealth(-healthValue);
    }
}
