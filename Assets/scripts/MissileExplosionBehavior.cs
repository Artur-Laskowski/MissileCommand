using UnityEngine;

public class MissileExplosionBehavior : MonoBehaviour {

    private float maxSize;
    private float startTime;
    private float duration;
    private float collapseRatio;
    private float destructionSize;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        duration = Settings.Instance.DefaultExplosionDuration;
        collapseRatio = Settings.Instance.DefaultExplosionCollapseRate;
        this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        //SpawnExplosion();

        Destroy(this.gameObject, duration);
    }
	
	// Update is called once per frame
	void Update () {
        var size = GetCurrentExplosionSize();
        SetExplosionVisualSize(size);
        SetExplosionHitboxSize(size);

        DetectAndExplodeEnemies();
	}

    public void Initialize(float explosionSize) {
        maxSize = explosionSize;
    }

    void SpawnExplosion() {
        this.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        var explosionPrefab = Resources.Load<GameObject>("Explosions/Prefabs/Explosion" + Random.Range(6, 8));

        Vector3 position = this.transform.position + new Vector3(0, -1, 0);
        GameObject o = Instantiate(explosionPrefab, position, Quaternion.identity);
        GameObject.Destroy(o, duration);
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
    //TODO extract enemy listing function, cache them for performance
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
        if (enemy == null)
            return;
        var eb = enemy.GetComponent<EnemyBaseBehavior>();
        eb.Explode();
    }

    void IncreaseScore() {
        var scoreHandler = ScoreHandler.Instance;
        scoreHandler.ChangeScore(1);
    }
}
