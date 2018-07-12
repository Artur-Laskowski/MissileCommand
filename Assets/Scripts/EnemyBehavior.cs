using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private float startTime;
    private float speed;
    private float journeyLength;
    private Vector3 startPos;
    private Vector3 endPos;

    protected ScoreHandlerBehavior scoreHandler;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        speed = 5;
        startPos = this.transform.position;
        endPos  = new Vector3(Random.Range(-20.0f, 20.0f), -1, 0);
        journeyLength = Vector3.Distance(startPos, endPos);

        scoreHandler = ScoreHandlerBehavior.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        Move();

        if (IsCollidingWithGround()) {
            DecreaseHealth();
            DestroyEnemy();
        }
    }

    void Move() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
    }

    protected bool IsCollidingWithGround() {
        return this.transform.position.y < 0;
    }

    virtual public bool IsDestructible() {
        return true;
    }

    protected void DestroyEnemy() {
        Destroy(this.gameObject);
    }

    virtual public void ExplodeEnemy() {
        for (int i = 0; i < 5; i++)
            SpawnSmallEnemy(this.transform.position);

        DestroyEnemy();
        scoreHandler.ChangeScore(+1);
    }

    protected void DecreaseHealth() {
        scoreHandler.ChangeHealth(-1);
    }


    void SpawnSmallEnemy(Vector3 pos) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/enemyShipSmall");
        GameObject o = Instantiate(prefab, pos, Quaternion.identity);
        o.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100,100), Random.Range(100,300)));
    }
}
