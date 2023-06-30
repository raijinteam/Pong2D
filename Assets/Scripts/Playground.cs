using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sprite_Playground;



    // Start is called before the first frame update
    void Start()
    {
        SetPlaygroundSizeToScreen();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void SetPlaygroundSizeToScreen()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        float spriteHeight = sprite_Playground.sprite.bounds.size.y;
        float spriteWidth = sprite_Playground.sprite.bounds.size.x;

        float scale = Mathf.Max(screenWidth / spriteWidth, screenHeight / spriteHeight);

        // Set the scale of the sprite
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}



