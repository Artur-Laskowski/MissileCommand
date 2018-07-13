using UnityEngine;
using System.Collections;

public class EnemyBaseBehavior : MonoBehaviour {

    protected float creationTime;
    protected int scoreValue;
    protected int healthValue;

    // Use this for initialization
    protected void Start() {
        creationTime = Time.time;
        //TODO
        var meteoriteSprites = Resources.LoadAll<Sprite>("asteroids");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = meteoriteSprites.PickRandom();

        scoreValue = 3;
        healthValue = 2;
    }

    // Update is called once per frame
    protected void Update() {
        if (IsCollidingWithGround()) {
            //TODO
            var index = Random.Range(1, 7);
            if (index > 5)
                index += 3;
            var explosionPrefab = Resources.Load<GameObject>("Explosions/Prefabs/Explosion" + index);

            Vector3 position = this.transform.position + new Vector3(0, -1, 0);
            GameObject o = Instantiate(explosionPrefab, position, Quaternion.identity);
            GameObject.Destroy(o, 2);

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
    //TODO explosion
    public virtual void Explode() {
        DestroyEnemy();

        ScoreHandler.Instance.ChangeScore(scoreValue);
    }

    protected void DecreaseHealth() {
        ScoreHandler.Instance.ChangeHealth(-healthValue);
    }
}
