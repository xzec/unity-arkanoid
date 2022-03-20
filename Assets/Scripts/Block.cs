using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparkleVFX;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private int maxHits;
    private Level _level;
    private int _timesHit;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        _level = FindObjectOfType<Level>();
        if (CompareTag("Breakable_1") || CompareTag("Breakable_3"))
            _level.CountBlocks();
    }

    private void OnCollisionEnter2D()
    {
        if (CompareTag("Breakable_1") || CompareTag("Breakable_3"))
            HandleHit();
    }

    private void HandleHit()
    {
        _timesHit++;
        if (_timesHit >= maxHits) DestroyBlock();
        else ShowNextHitSprite();
    }

    private void DestroyBlock()
    {
        if (Camera.main != null) AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        _level.BlockDestroyed();
        FindObjectOfType<GameStatus>().AddToScore();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        var t = transform;
        var sparkles = Instantiate(blockSparkleVFX, t.position, t.rotation);
        Destroy(sparkles, 0.25f);
    }

    private void ShowNextHitSprite()
    {
        var spriteIndex = _timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }
}