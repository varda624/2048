using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NumberedDice : MonoBehaviour
{
    public int NumberOnDice;
    public UnityEvent OnMerging;
    public float ForceOnMerging;
    public TMP_Text[] TextOnFaces;
    public Color[] DiceColors;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        UpdateTextFaces();
        DefineColor();
    }

    public void AssignNumber(int value)
    {
        NumberOnDice = value;
        UpdateTextFaces();
        DefineColor();
    }

    public void UpdateTextFaces()
    {
        for (int i = 0; i < TextOnFaces.Length; i++)
        {
            TextOnFaces[i].text = $"{NumberOnDice}";
        }
    }

    private void Merge(NumberedDice collidedDice)
    {
        Vector3 thisDicePosition = transform.position;
        Vector3 collidedDicePosition = collidedDice.transform.position;
        Vector3 center = (thisDicePosition + collidedDicePosition) / 2;
        Destroy(collidedDice.gameObject);
        transform.position = center;
        _rigidbody.AddForce(Vector3.up * ForceOnMerging, ForceMode.VelocityChange);
        AssignNumber(NumberOnDice + NumberOnDice);
        OnMerging?.Invoke();
        GameManager.CheckHighestNumberOnDice(this);
        GameManager.MakeChangesToGameScore(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        NumberedDice collidedDice = collision.gameObject.GetComponent<NumberedDice>();
        if (collidedDice != null)
        {
            if (NumberOnDice == collidedDice.NumberOnDice)
            {
                Merge(collidedDice);
            }
        }
    }
    
    public void DefineColor()
    {
        if (_renderer != null)
        {
            Color diceColor = Color.white;
            int colorIndex = 0;
            for (int i = 2; i <= NumberOnDice; i+= i)
            {
                if (colorIndex < DiceColors.Length)
                {
                    diceColor = DiceColors[colorIndex];
                }
                else
                {
                    diceColor = Color.white;
                }
                colorIndex++;
            }
            _renderer.material.color = diceColor;
        }
    }
}
