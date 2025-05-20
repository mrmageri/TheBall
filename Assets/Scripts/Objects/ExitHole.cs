using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects
{
    public class ExitHole : MonoBehaviour
    {
        [SerializeField] private ParticleSystem winningParticle;
        [SerializeField] private float winningDelayDuration;
        private bool _won;

        private void Update()
        {
            if (Input.anyKeyDown && _won)
            {
                StopAllCoroutines();
                LoadNextScene();
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                winningParticle.Play();
                _won = true;
                StartCoroutine(WinningDelay());
            }
        }

        private IEnumerator WinningDelay()
        {
            yield return new WaitForSeconds(winningDelayDuration);
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curSceneIndex == (SceneManager.sceneCountInBuildSettings - 1) ? 0 : curSceneIndex + 1);
        }
    }
}
