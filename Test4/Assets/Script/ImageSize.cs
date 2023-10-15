using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class ImageSize : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer sprite;

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

    void FixedUpdate()
    {
        float playerXPos = player.transform.position.x;

        // Calculate scale and position based on player movement
        float scaleMultiplier = 1 + playerXPos / (PortalXPos * 10f);
        float yPosChange = -_rigidbody2D.velocity.x * 0.01f;

        // Update image properties

        //sprite.transform.position = new Vector3(playerXPos, sprite.transform.position.y, sprite.transform.position.z);
        sprite.transform.position += new Vector3(-yPosChange, yPosChange * 0.01f, 0);
        sprite.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
        
    }
}