using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class RotateAsync : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    [SerializeField] private Button _cancelButton;

    private CancellationTokenSource _cancellationTokenSource;

    public async void Rotate()
    { 
        _cancellationTokenSource = new CancellationTokenSource();
        
        _cancelButton.onClick.AddListener(() => _cancellationTokenSource.Cancel());

        for (int i = 0; i < _time * 100; i++)
        {
            this.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
            await UniTask.Delay(10, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
        }
    }
}
