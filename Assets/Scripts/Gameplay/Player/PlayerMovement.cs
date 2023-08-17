using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform testCube;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float pointDistanceThreshold;

    Camera mainCamera;
    Plane intersectPlane;
    bool gameStarted;
    bool canMove;

    Touch touchInput;
    Vector3 swipeDirection;
    Vector2 startPos;
    Vector2 swipeScreenDir;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        intersectPlane = new Plane(Vector3.up, transform.position.y);
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            return;
        }

        //if(Input.GetMouseButton(0))
        //{
        //    UpdatePlayerLook();
        //}

        //if(Input.GetMouseButtonDown(0))
        //{
        //    playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, true);
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, false);
        //}

        if (Input.touchCount > 0)
        {
            touchInput = Input.GetTouch(0);
            if (touchInput.phase == TouchPhase.Moved)
            {
                playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, true);
                swipeScreenDir = touchInput.position - startPos;
                swipeDirection.x = swipeScreenDir.x;
                swipeDirection.z = swipeScreenDir.y;
                canMove = true;
            }
            else if (touchInput.phase == TouchPhase.Began)
            {
                startPos = touchInput.position;
            }

            UpdatePlayerLook();
        }
        else
        {
            canMove = false;
            playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, false);
        }
    }

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_GAME_START, HandleGameStarted);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_GAME_START, HandleGameStarted);
    }

    #endregion

    #region Private
    private void UpdatePlayerLook()
    {
        //Vector3 lookDir;
        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        ////Initialise the enter variable
        //float enter = 0.0f;

        //if (intersectPlane.Raycast(ray, out enter))
        //{
        //    lookDir = ray.GetPoint(enter);
        //    lookDir.y = playerTransform.position.y;
        //    //testCube.transform.position = lookDir;
        //    transform.LookAt(lookDir);

        //    if (Vector3.SqrMagnitude(transform.position - lookDir) > pointDistanceThreshold)
        //    {
        //        MovePlayer();
        //        playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, true);
        //    }
        //    else
        //    {
        //        playerAnimator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, false);
        //    }
        //}
        if (canMove)
        {
            transform.LookAt(swipeDirection);
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        transform.position += (playerMoveSpeed * Time.deltaTime) * playerTransform.forward;
    }
    #endregion

    #region Callbacks

    private void HandleGameStarted(object arg)
    {
        gameStarted = true;
    }    

    #endregion
}
