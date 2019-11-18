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
}
