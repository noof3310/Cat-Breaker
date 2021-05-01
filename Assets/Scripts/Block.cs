using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip[] breakSounds;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    Level level;

    [SerializeField] int timesHit;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable") level.CountBreakableBlocks();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable") HandleHit();
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) DestroyBlock();
        else ShowNextHitSprite();
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else Debug.LogError("Block Sprites is missing from array");
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        level.BlockDestroyed();
        FindObjectOfType<GameSession>().AddToScore();
        TriggerSparkleVFX();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX()
    {
        AudioClip clip = breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)];
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
