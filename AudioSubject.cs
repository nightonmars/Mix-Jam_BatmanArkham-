using UnityEngine;
using System;

namespace DesignPatterns.Observer
{
    public class AudioSubject : MonoBehaviour
    {
        public event Action<int> audioAnimationEvent;//use the int variable to set different sounds in the audio event (fmod, wwise,etc) rather than tiggereing separate audio events.
        public Action onePunch;
        public Action leapAttackStart;
        public Action leapAttackStop;
        public Action lastHit;

        // invoke the event to broadcast to any listeners/observers
        public void TriggerSwooshAudio(int fmodParam)
        {
            audioAnimationEvent?.Invoke(fmodParam);
        }

        public void TriggerPunch()
        {
            onePunch?.Invoke(); 
        }

        public void TriggerLeapAttackStart()
        {
            leapAttackStart?.Invoke();
        }

        public void TriggerLeapAttackStop()
        {
            leapAttackStop?.Invoke();
        }

        //for the final blow when the cam zooms in
        public void LastSoundhit()
        {
            lastHit.Invoke(); 
        }
    }
}

