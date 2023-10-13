using UnityEngine;
using UnityEngine.UI;

public class ImageSize : MonoBehaviour
{
    public GameObject player;
    public Image image;

    private Rigidbody2D _rigidbody2D;
    private float PortalXPos = 65f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) player = GameObject.Find("Player");
    }

    private void Start()
    {
        _rigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float playerXPos = player.transform.position.x;

        // Calculate scale and position based on player movement
        float scaleMultiplier = 1 + playerXPos / (PortalXPos * 10f);
        float yPosChange = -_rigidbody2D.velocity.x * 0.01f;

        // Update image properties
        image.rectTransform.position += new Vector3(0, yPosChange * 0.1f, 0);
        image.rectTransform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
    }
}