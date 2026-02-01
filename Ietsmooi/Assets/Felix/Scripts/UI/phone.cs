using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class phone : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float targetHeight;
    [SerializeField] private Vector2[] heights = new Vector2[2];
    [SerializeField] private bool shouldMove = false;
    [SerializeField] private int framesTraveling = 0;
    public bool isActive = false;
    public int moveSpeed;
    public int passWord = 0;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldMove)
        {
            Vector2 a = rect.anchoredPosition;
            if(rect.anchoredPosition.y > targetHeight)
            {
                rect.anchoredPosition = new Vector2(a.x, a.y - (moveSpeed + framesTraveling / 5));
            }else if(rect.anchoredPosition.y < targetHeight)
            {
                rect.anchoredPosition = new Vector2(a.x, a.y + (moveSpeed + framesTraveling / 5));
            }

            if(Mathf.FloorToInt(Mathf.Abs(a.y - targetHeight)) <= (moveSpeed + framesTraveling / 5))
            {
                shouldMove = false;

                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x,
                    targetHeight
                );

                Debug.Log("good height reached!");
            }

            framesTraveling += 1;
        }
    }

    public void TogglePhone(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        isActive = !isActive;
        targetHeight = heights[Convert.ToInt32(isActive)].y;
        shouldMove = true;
        framesTraveling = 0;

        if (passWord == 0)
        {
            
        }

        Debug.Log("phone visible set to: " + isActive + " new height is: " + targetHeight);
    }
}
