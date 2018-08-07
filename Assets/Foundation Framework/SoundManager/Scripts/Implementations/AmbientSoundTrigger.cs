
using UnityEngine;

namespace FoundationFramework.Audio
{
    public class AmbientSoundTrigger : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart;

        [SerializeField] private SoundConfig _sound;


        private int _soundId = -1;

        private void Start()
        {
            if(_playOnStart) TriggerSound();
        }

        public void TriggerSound()
        {
            _soundId = AmbientScenePlayer.PlayAmbientSound(_sound.name);
        }

        public void StopAmbientSound()
        {
            if(_soundId != -1)
            AmbientScenePlayer.StopAmbientSound(_soundId); 
        }

    }
}
