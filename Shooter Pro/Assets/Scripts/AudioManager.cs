using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Serilized Private Field
        [SerializeField]
        AudioSource _explosionAudioSource;
        [SerializeField]
        AudioSource _powerUpAudioSource;
    #endregion

    #region Public Methods
        public void ExplosionSound()
        {
            _explosionAudioSource.Play();
        }

        public void PowerUpPickSound()
        {
            _powerUpAudioSource.Play();
        }
    #endregion
}
