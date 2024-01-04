using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio; 

namespace DesignPatterns.Observer
{
    public class AudioObserver : MonoBehaviour
    {
        private EventInstance swooshAudio;
        private EventInstance woosh;
        private EventInstance finalSnapShot;
        private bool isLastHit; 

        // reference to the subject script that we are observing/listening to
        [SerializeField] AudioSubject audioSubject;

        private void Start()
        {
            isLastHit = false; 
        }

        // event handling method: the function signature must match the subject's event
        private void OnTriggerSwooshAudio(int fmodParam) 
        {  
            swooshAudio = RuntimeManager.CreateInstance("event:/SwooshAttacks");
            swooshAudio.setParameterByName("SwooshAttackType", fmodParam); 
            swooshAudio.start();
            swooshAudio.release();
        }

        private void TriggerPunch()
        {
            if(!isLastHit)
            {
                RuntimeManager.PlayOneShot("event:/Punch");
            }
            else
            {
                RuntimeManager.PlayOneShot("event:/LastPunch");
            }
            
        }

        private void LeapAttackStart()
        {
            woosh = RuntimeManager.CreateInstance("event:/Leaps");
            //woosh.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject)); 
            woosh.start();
        }

        private void LeapAttackStop()
        {
            woosh.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            woosh.release(); 
        }

        //for the final blow when the cam zooms in
        private void LastHitSound() 
        {
            finalSnapShot = RuntimeManager.CreateInstance("snapshot:/SS_LastPunch");
            finalSnapShot.start();
            isLastHit = true; //set the bool so the trigger changes to the last punch event. 
        }


        private void Awake()
        {
            // subscribe/register to the subject's event 
            if (audioSubject != null)
            {
                audioSubject.audioAnimationEvent += OnTriggerSwooshAudio;
                audioSubject.onePunch += TriggerPunch;
                audioSubject.leapAttackStart += LeapAttackStart;
                audioSubject.leapAttackStop += LeapAttackStop;
                audioSubject.lastHit += LastHitSound;
            }     
        }

        private void OnDestroy()
        {
            // unsubscribe/unregister if we destroy the object
            if (audioSubject != null)
            {
                audioSubject.audioAnimationEvent -= OnTriggerSwooshAudio;
                audioSubject.onePunch -= TriggerPunch;
                audioSubject.leapAttackStart -= LeapAttackStart;
                audioSubject.leapAttackStop -= LeapAttackStop;
                audioSubject.lastHit -= LastHitSound;
            }
        }
    }
}
