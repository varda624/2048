using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceLaunchController : MonoBehaviour
{
    public GameObject _previousDiceInstance => _previousDice;
    public float PushForce = 30f;
    public GameObject DicePrefab;
    public Transform PointA;
    public Transform PointB;
    public LayerMask LayerMask;

    private GameObject _newDice;
    private GameObject _previousDice;
    private float _cooldown = 1f;
    private float _lastLaunchTime;

    private void Launch()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (IsTouchOnLaunchZone(touch) && _newDice == null && !GameManager.IsGameOver)
                {
                    if (Time.time < _lastLaunchTime + _cooldown) return;
                    CreateDice();
                    _lastLaunchTime = Time.time;
                }
                break;

            case TouchPhase.Moved:
                Move(touch);
                break;

            case TouchPhase.Canceled:
            case TouchPhase.Ended:
                if (_newDice != null) Push();
                break;
        }
    }

    private bool IsTouchOnLaunchZone(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask))
        {
            return true;
        }
        return false;
    }

    private void Move(Touch touch)
    {
        if (_newDice != null)
        {
            float screenWidth = touch.position.x / Screen.width;
            _newDice.transform.position = Vector3.Lerp(PointA.position, PointB.position, screenWidth);
        }
    }


    private void Update()
    {
        if (GameManager.IsGameOver != true)
        {
            Launch();
        }
    }

    public void CreateDice()
    {
        _newDice = Instantiate(DicePrefab);
        _newDice.GetComponent<NumberedDice>().AssignNumber(GetRandomDiceNumber());
        Rigidbody diceRBPos = _newDice.GetComponent<Rigidbody>();
        Rigidbody diceRBRot = _newDice.GetComponent<Rigidbody>();
        diceRBPos.constraints = RigidbodyConstraints.FreezePositionY; /// start here next lesson and figure out why it doesnt work 
        diceRBRot.constraints = RigidbodyConstraints.FreezeRotation;
        _newDice.tag = "NewDice";
        Vector3 center = (PointA.position + PointB.position) / 2;
        _newDice.transform.position = center;
    }

    private void Push()
    {
        Rigidbody diceRB = _newDice.GetComponent<Rigidbody>();
        diceRB.constraints = RigidbodyConstraints.None;
        diceRB.AddForce(Vector3.forward * PushForce, ForceMode.VelocityChange);
        StartCoroutine(AssignTag());
        _previousDice = _newDice;
        _newDice = null;

        IEnumerator AssignTag()
        {
            yield return new WaitForSeconds(0.1f);
            _previousDice.tag = "Dice";
        }
    }

    private int GetRandomDiceNumber()
    {
        int number = 2;
        int iterations = 0;
        for (int i = 2; i < GameManager.HighestNumberOnDice; i += i)
        {
            iterations++;
        }
        int iterationStorage = Random.Range(0, iterations);
        for (int i = 0; i < iterationStorage; i++)
        {
            number += number;
        }

        return number;
    }

}
