using UnityEngine;
using UnityEngine.UI;

public class DiceLaunchController : MonoBehaviour
{
    public float PushForce = 30f;
    public GameObject DicePrefab;
    public Transform PointA;
    public Transform PointB;
    public Button button;
    public LayerMask LayerMask;

    private GameObject _newDice;
    private GameObject _previousDice;

    private void Launch()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && _newDice == null)
        {
            if (_previousDice != null) _previousDice.tag = "Dice";
            CreateDice();
        }
         if (touch.phase == TouchPhase.Moved && _newDice != null)
        {
            float screenWidth = Screen.width;
            float touchPositionX = Input.GetTouch(0).position.x / screenWidth;
            _newDice.transform.position = Vector3.Lerp(PointA.position, PointB.position, touchPositionX);
        }
         if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled && _newDice != null)
        {
            Push();
        }
    }

    private void Update()
    {
        
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~LayerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("LaunchZone"))
                {
                    Launch();
                }
            }
        
        



        //if (GameManager.IsGameOver != true)
        //{
        //    if (Input.touchCount > 0 && _newDice == null)
        //    {
        //        if (_previousDice != null) _previousDice.tag = "Dice";

        //        CreateDice();
        //    }
        //    else if (_newDice != null)
        //    {
        //        if (Input.touchCount > 0 && _newDice != null)
        //        {
        //            float screenWidth = Screen.width;
        //            float touchPositionX = Input.GetTouch(0).position.x / screenWidth;
        //            _newDice.transform.position = Vector3.Lerp(PointA.position, PointB.position, touchPositionX);
        //        }
        //        else if (Input.touchCount == 0)
        //        {
        //            Push();
        //        }
        //    }
        //}

    }

    public void CreateDice()
    {
        _newDice = Instantiate(DicePrefab);
        _newDice.tag = "NewDice";
        Vector3 center = (PointA.position + PointB.position) / 2;
        _newDice.transform.position = center;
    }

    private void Push()
    {
        Rigidbody diceRB = _newDice.GetComponent<Rigidbody>();
        diceRB.AddForce(Vector3.forward * PushForce, ForceMode.VelocityChange);
        _previousDice = _newDice;
        _newDice = null;
    }

}
