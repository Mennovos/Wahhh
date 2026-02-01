using System;
using System.Collections.Generic;
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
    [SerializeField] private int[] cursorPos = new int[2];
    public List<PhoneButton> phoneButtons = new List<PhoneButton>();
    public List<PhoneScreen> phoneScreens = new List<PhoneScreen>();
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

        targetHeight = heights[Convert.ToInt32(isActive)].y;
        shouldMove = true;
        framesTraveling = 0;

        if (passWord == 0)
        {
            
        }
        
        isActive = !isActive;
        Debug.Log("phone visible set to: " + isActive + " new height is: " + targetHeight);
    }

    //lowkey gotta rebuild the enitre UI system just because this fuckass camera yaaay
    public void NavigatePhone(InputAction.CallbackContext context)
    {
        if(!isActive) return;

        Vector2 dir = context.ReadValue<Vector2>();
        Debug.Log(dir);

        if(dir.x == 1 && dir.y == 0 && cursorPos[0] < 2)
        {
            cursorPos[0] += 1;
        }else if(dir.x == -1 && dir.y == 0 && cursorPos[0] > 0)
        {
            cursorPos[0] -= 1;
        }else if(dir.y == 1 && dir.x == 0 && cursorPos[1] > 0)
        {
            cursorPos[1] -= 1;
        }else if(dir.y == -1 && dir.x == 0 && cursorPos[1] < 2)
        {
            cursorPos[1] += 1;
        }
    }

}

[System.Serializable]
public class PhoneButton
{
    public GameObject button;
    public int[] buttonPos = new int[2];
    public string name;

}

[System.Serializable]
public class PhoneScreen
{
    public GameObject screen;
    public List<PhoneButton> buttonsOnScreen = new List<PhoneButton>();
    public string name;
    public int index = 999;

    public int[] CalculateHeight()
    {
        int highestX = 0;
        int highestY = 0;

        foreach(PhoneButton b in buttonsOnScreen)
        {
            if(b.buttonPos[0] > highestX)
            {
                highestX = b.buttonPos[0];
            }

            if(b.buttonPos[1] > highestY)
            {
                highestY = b.buttonPos[1];
            }
        }

        return new int[2] {highestX, highestY};
    }
}
