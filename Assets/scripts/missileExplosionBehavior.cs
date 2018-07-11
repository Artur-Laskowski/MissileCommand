using UnityEngine;

public class MissileExplosionBehavior : MonoBehaviour {

    private float maxSize;
    private float startTime;
    private float duration;
    private float collapseRatio;
    private float destructionSize;

	// Use this for initialization
	void Start () {
        maxSize = Settings.Instance.DefaultMaxExplosionSize;
        startTime = Time.time;
        duration = Settings.Instance.DefaultExplosionDuration;
        collapseRatio = Settings.Instance.DefaultExplosionCollapseRate;
        this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        Destroy(this.gameObject, duration);
    }
	
	// Update is called once per frame
	void Update () {
        var size = GetCurrentExplosionSize();
        SetExplosionVisualSize(size);
        SetExplosionHitboxSize(size);

        DetectAndExplodeEnemies();
	}

    float GetCurrentExplosionSize() {
        var maxSizeTime = duration * collapseRatio;
        var elapsedTime = Time.time - startTime;
        float currentMaxSizeRatio;
        float currentSize;

        if (elapsedTime < maxSizeTime) {
            currentMaxSizeRatio = elapsedTime / maxSizeTime;
        }
        else {
            currentMaxSizeRatio = (duration - elapsedTime) / (duration - maxSizeTime);
        }
        currentSize = currentMaxSizeRatio * maxSize;

        return currentSize;
    }

    void SetExplosionVisualSize(float size) {
        this.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f) * size;
    }

    void SetExplosionHitboxSize(float size) {
        destructionSize = size / 10.0f;
    }

    void DetectAndExplodeEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (var enemy in enemies) {
            if (IsInRangeOfExplosion(enemy)) {
                Explode(enemy);
            }
        }
    }

    bool IsInRangeOfExplosion(GameObject enemy) {
        Vector3 enemyPos = enemy.transform.position;
        float distance = Vector3.Distance(enemyPos, transform.position);
        return distance < destructionSize;
    }

    void Explode(GameObject enemy) {
        //Destroy(enemy);
        //IncreaseScore();

        if (enemy == null)
            return;
        var eb = enemy.GetComponent<EnemyBehavior>();
        eb.ExplodeEnemy();
    }

    void IncreaseScore() {
        var scoreHandler = ScoreHandlerBehavior.Instance;
        scoreHandler.ChangeScore(1);
    }
}
