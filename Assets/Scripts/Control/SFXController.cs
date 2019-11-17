using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip fireBallSFX;
    public AudioClip iceSFX;
    public AudioClip airDashSFX;

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
}
