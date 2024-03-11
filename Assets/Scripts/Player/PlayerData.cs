using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player Data", order = 0)]//Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    [HideInInspector] public float gravityStrength;         //Downwards Force ( gravity ) need the junpHeight and jumpTimeToApex.
    [HideInInspector] public float gravityScale;            //Strength of the player's gravity as a multiplier of gravity.

    [Space(20)]

    public float fallGravityMult;                           //Multiplier to the player's gravity when falling.
    public float maxFallSpeed;                              //Maximun fall speed ( terminal velocity ) of the player when falling.
    [Space(5)]
    //Variables to make the player fall faster is downwards input is press ( can be ignore ).
    public float fastFallGravityMult;                       //Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.
    public float maxFastFallSpeed;                          //Maximun fall speed(terminal velocity) of the player when performing a faster fall.

    [Space(20)]

    [Header("Run")]
    public float runMaxSpeed;                               //Target speed the player can reach.
    public float runAcceleration;                           //Speed at which our player accelerates to max speed, can be set to runMaxSpeed (instant acceleration) to 0 (none at all).
    [HideInInspector] public float runAccelAmount;          //The actual force (multiplied with speedDiff) applied to the player.
    public float runDecceleration;                          //The speed at wich the player decelerates from their current speed, can be set to runMaxSpeed (instant deceleration) to 0 (none at all).
    [HideInInspector] public float runDeccelAmount;         //Actual force (multiplied with speedDiff) applied to the player.
    [Space(5)]
    [Range(0f, 1)] public float accelInAir;                 //Multiplier applied to acceleration rate when airborne.
    [Range(0f, 1)] public float deccelInAir;
    [Space(5)]
    public bool doDoncerveMomentum = true;

    [Space(20)]

    [Header("Jump")]
    public int airJumps;                                    //Jumps that can be performed in the air.
    public float jumpHeight;                                //Height of the player's jump.
    public float jumpTimeToApex;                            //Time between applying the jump force and reaching the desired jump height (Also control the player's gravity and jump force).
    [HideInInspector] public float jumpForce;               //The actual force applied (upwards) to the player when they jump.

    [Header("Both Jumps")]
    public float jumpCutGravityMult;                        //Multiplier to increse gravity if the player releases the jump buttom wile still jumping.
    [Range(0f, 1)] public float jumpHangGravityMult;        //Reduces gravity while close to the apex (desired max height) of jump.
    public float jumpHangTimeThreshold;                     //Speed (close to 0) where the player will experience extra "jump hang". Player's velocity.y is closet to 0 at the jump's apex (think of the gradient of a parabola or quadratic function).
    [Space(0.5f)]
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Header("Wall Jump")]
    public Vector2 wallJumpForce;                           //The actual force (set by me) applied to the player when wall jumping.
    [Space(5)]
    [Range(0f, 1)] public float wallJumpRunLerp;            //Reduce the effect of player's movment while wall jumping.
    [Range(0f, 1.5f)] public float wallJumpTime;            //Time after wall jumping the player's movement is slowed for.
    public bool doTurnOnWallJump;                           //Player will rotate to face wall jumping direction.

    [Space(20)]

    [Header("Dash")]
    public int dashAmount;                                  //Times that a dash can be performed.
    public float dashSpeed;                                 //Target speed of the dash.
    public float dashSleepTime;                             //Duration for wich the game freezes when we press dash but befero we read directional input and aplly a force.
    [Space(5)]
    public float dashAttackTime;
    [Space(5)]
    public float dashEndTime;                               //Time after the player finish the inital drag phase, smoothing the transition back to idle (or any standard state).
    public Vector2 dashEndSpeed;                            //Slow down player, makes dash feel more responsive
    [Range(0f, 1f)] public float dashEndRunLerp;            //Slows the affect of player movement while dashing
    [Space(5)]
    public float dashRefillTime;

    [Space(20)]

    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;           //Grace period after falling off a platform, where you can still jump.
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;  //Grace period after pressing jump where a jump will be automatically performed once the requirement (eg. being grounded) are met.
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;

    //Unity Callback, callent when the inspector updates.
    private void OnValidate()
    {
        //Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2)
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        //Calculate the rigidbody's gravity scale (ie: gravity strenght realtive to unity's gravity value, see project settings/Physics2D).
        gravityScale = gravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & deceleration force using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed.
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        //Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex).
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        #region Variable Range
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
        #endregion
    }
}
