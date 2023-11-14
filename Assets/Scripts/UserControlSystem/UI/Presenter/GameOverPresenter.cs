using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public class GameOverPresenter : MonoBehaviour
    {
        [SerializeField] 
        private Button _restartButton;
        [SerializeField] 
        private TextMeshProUGUI _text;
        [SerializeField]
        private GameObject _panel;

        [Inject]
        private void Init(IGameState gameState)
        {
            _panel.SetActive(false);
            
            _restartButton.OnClickAsObservable().Subscribe(_ => RestartGame());
            
            gameState.State.ObserveOnMainThread().Subscribe(faction =>
            {
                _panel.SetActive(true);
                _text.text = faction == 0 ? $"Everybody loose" : $"Faction {faction} win";
                Time.timeScale = 0;
            }).AddTo(this);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene("Main");
            Time.timeScale = 1;
        }
    }
}