using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundHandler : MonoBehaviour
{
    [SerializeField] AudioClip chaseSaund;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip deathSound;

    AudioSource enemyAudio;
    EnemyHealth health;
    EnemyAI movement;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyAI>();
        enemyAudio = GetComponent<AudioSource>();
    }

    public void DeathSoundQue()
    {
        enemyAudio.Stop();
        if (health.IsDead())
        {
            enemyAudio.PlayOneShot(deathSound, 2);
        }
    }

    public void ChaseSoundQue()
    {
        if (movement.isProvoked && !enemyAudio.isPlaying)
        {
            enemyAudio.PlayOneShot(chaseSaund);
        }
    }

    public void AttackSoundQue()
    {
        if (movement.isAttacking)
        {
            enemyAudio.PlayOneShot(attackSound);
        }
    }
}
