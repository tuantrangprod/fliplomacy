using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private const int mMessageWidth  = 200;
    private const int mMessageHeight = 64;

    private readonly Vector2 mXAxis = new Vector2(1, 0);
    private readonly Vector2 mYAxis = new Vector2(0, 1);

    private int mMessageIndex = 0;

    private const float mAngleRange = 30;

    private const float mMinSwipeDist = 20.0f;

    private const float mMinVelocity  = 1000.0f;

    private Vector2 mStartPosition;
    private float mSwipeStartTime;

    public TextMeshProUGUI ShowText;
    FloppyControll floppyControll;
    public GameManager gameManager;

    void Start ()
    {
        floppyControll = gameObject.GetComponent<FloppyControll>();
    }
    bool canSwipe = false;
    bool canRescale = false;
    void Update () {

        if (floppyControll.canswipe)
        {

            if (Input.GetMouseButtonDown(0))
            {
                mStartPosition = new Vector2(Input.mousePosition.x,
                                             Input.mousePosition.y);
                mSwipeStartTime = Time.time;
                floppyControll.StartJumpAnim();
                canSwipe = true;
            }
            Vector2 endPosition = new Vector2(Input.mousePosition.x,
                                                   Input.mousePosition.y);
            Vector2 swipeVector = endPosition - mStartPosition;
            if(swipeVector.magnitude > mMinSwipeDist)
            {
                float deltaTime = Time.time - mSwipeStartTime;

                //Vector2 endPosition = new Vector2(Input.mousePosition.x,
                //                                   Input.mousePosition.y);
                //Vector2 swipeVector = endPosition - mStartPosition;

                float velocity = swipeVector.magnitude / deltaTime;

                if (velocity > mMinVelocity && canSwipe)
                {
                    canSwipe = false;
                    swipeVector.Normalize();

                    float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                    if (angleOfSwipe < mAngleRange)
                    {
                        OnSwipeRight();
                    }
                    else if ((180.0f - angleOfSwipe) < mAngleRange)
                    {
                        OnSwipeLeft();
                    }
                    else
                    {
                        angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < mAngleRange)
                        {
                            OnSwipeTop();
                        }
                        else if ((180.0f - angleOfSwipe) < mAngleRange)
                        {
                            OnSwipeBottom();
                        }
                        else
                        {
                            mMessageIndex = 0;
                        }
                    }
                }
                else
                {
                    canRescale = true;
                }
            }
            else
            {
                canRescale = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (canRescale)
                {
                    floppyControll.floppyOnStartReScale();
                    canRescale = false;
                }
               
                //else
                //{
                //   
                //    Debug.Log(111);
                //}
            }
        }
    }

    private void OnSwipeLeft() {
        ShowText.text = "Left";
        Debug.Log("Left");
        if (floppyControll.canswipe)
        {
            gameManager.OnSwipeLeft();
            floppyControll.JumpAnim();
        }

    }

    private void OnSwipeRight() {
        ShowText.text = "Right";
        Debug.Log("Right");
        if (floppyControll.canswipe)
        {
            gameManager.OnSwipeRight();
            floppyControll.JumpAnim();
        }

    }

    private void OnSwipeTop() {
        ShowText.text = "Top";
        Debug.Log("Top");
        if (floppyControll.canswipe)
        {
            gameManager.OnSwipeTop();
            floppyControll.JumpAnim();
        }

    }

    private void OnSwipeBottom() {
        ShowText.text = "Botton";
        Debug.Log("Down");
        if (floppyControll.canswipe)
        {
            gameManager.OnSwipeBottom();
            floppyControll.JumpAnim();
        }

    }
}
