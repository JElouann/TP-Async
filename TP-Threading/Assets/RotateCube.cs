using System.Collections;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private Coroutine _coroutine;

    // merci la flèche (ne marche pas sans stocker la coroutine)
    public void StartRotate() => _coroutine = StartCoroutine(RotateCoroutine());

    public void StopRotate() => StopCoroutine(_coroutine);

    private IEnumerator RotateCoroutine()
    {
        for(int i = 0; i < _time * 100; i++)
        {
            this.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
