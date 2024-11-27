using System.Collections;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _time;

    // merci la flèche (ne marche pas sans stocker la coroutine)
    public void StartRotate() => StartCoroutine(RotateCoroutine());

    public void StopRotate() => StopCoroutine(RotateCoroutine());

    private IEnumerator RotateCoroutine()
    {
        for(int i = 0; i < _time * 100; i++)
        {
            this.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
