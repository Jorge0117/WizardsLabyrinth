using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip fireBallSFX;
    public AudioClip iceSFX;
    public AudioClip airDashSFX;

    public AudioClip chawaAttack1,
        chawaAttack2,
        chawaJump1,
        chawaJump2,
        chawaDamage;

    public AudioClip typing;

    public AudioClip eAttack1, eAttack2, eAttack3, eAttack4;

    public AudioClip eDie1, eDie2;

    public AudioClip eTakeDamage1, eTakeDamage2;

    public void PlayFireBallSFX(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(fireBallSFX, position, 0.8f);
    }
    
    public void PlayIceSFX(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(iceSFX, position, 0.8f);
    }
    
    public void PlayAirDashSFX(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(airDashSFX, position, 0.8f);
    }

    public void PlayChawaAttack(Vector3 position)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            AudioSource.PlayClipAtPoint(chawaAttack1, position, 1.5f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(chawaAttack2, position, 1.5f);
        }
    }
    
    public void PlayChawaJump(Vector3 position)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            AudioSource.PlayClipAtPoint(chawaJump1, position, 1.5f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(chawaJump2, position, 1.5f);
        }
    }

    public void PlayChawaDamage(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(chawaDamage, position, 1.5f);
    }

    public void PlayTyping(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(typing, position, 1.5f);
    }

    public void PlayEnemyAttack(Vector3 position)
    {
        int randomNumber = Random.Range(0, 4);
        if (randomNumber == 0)
        {
            AudioSource.PlayClipAtPoint(eAttack1, position, 1.5f);
        }
        else if (randomNumber == 1)
        {
            AudioSource.PlayClipAtPoint(eAttack2, position, 1.5f);
        }
        else if (randomNumber == 2)
        {
            AudioSource.PlayClipAtPoint(eAttack3, position, 1.5f);
        }
        else if (randomNumber == 3)
        {
            AudioSource.PlayClipAtPoint(eAttack4, position, 1.5f);
        }
    }

    public void PlayEnemyTakeDamage(Vector3 position)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            AudioSource.PlayClipAtPoint(eTakeDamage1, position, 1.5f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(eTakeDamage2, position, 1.5f);
        }
    }

    public void PlayEnemyDie(Vector3 position)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            AudioSource.PlayClipAtPoint(eDie1, position, 1.5f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(eDie2, position, 1.5f);
        }
    }
}
