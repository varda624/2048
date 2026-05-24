using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RestrictedZone : MonoBehaviour
{
    public static UnityEvent OnGameOver = new UnityEvent();

    public static bool DiceOutside;

    private const string FORBIDDEN_TAG = "Dice";
    private float _timeBetweenCheck = 0.5f;
    private BoxCollider _boxCollider;


    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(SwitchTimer());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(FORBIDDEN_TAG))
        {
            OnGameOver?.Invoke();
            GameManager.IsGameOver = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NewDice"))
        {
            DiceOutside = true;
        }
    }

    private void ToggleActivity()
    {
        if (_boxCollider.enabled == true) _boxCollider.enabled = false;
        else if (_boxCollider.enabled == false) _boxCollider.enabled = true;
    }

    private IEnumerator SwitchTimer()
    {
        while (!GameManager.IsGameOver)
        {
            yield return new WaitForSeconds(_timeBetweenCheck);
            ToggleActivity();
            yield return new WaitForSeconds(_timeBetweenCheck);
            ToggleActivity();
        }
    }
}
