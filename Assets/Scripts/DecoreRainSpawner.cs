using UnityEngine;
using UnityEngine.UI;

public class DecoreRainSpawner : MonoBehaviour
{
    public Image[] DecorPrefabs;
    public RectTransform DecorContainer;
    public float SpawnDelay;
    public float MaxSpeed;
    public float MinSpeed;

    private float _canvasWidth;
    private float _canvasHeight;

    private void Awake()
    {
        _canvasWidth = DecorContainer.rect.width;
        _canvasHeight = DecorContainer.rect.height;
        InvokeRepeating(nameof(Spawn), 0, SpawnDelay);
    }

    private void Spawn()
    {
        var obj = Instantiate(DecorPrefabs[Random.Range(0, DecorPrefabs.Length)],DecorContainer);
        var objRect = obj.GetComponent<RectTransform>();
        float randomX = Random.Range(-_canvasWidth / 2, _canvasWidth / 2);
        float spawnY = _canvasHeight / 2 + 100;
        objRect.anchoredPosition = new Vector2(randomX, spawnY);
        objRect.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        float speed = Random.Range(MinSpeed, MaxSpeed);
        obj.GetComponent<UISprits>().Speed = speed;
    }

}
