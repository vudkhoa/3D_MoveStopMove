using UnityEngine;
using Utils.DesignPattern.State;
using Player.StateAnim;
using Player.Controller;

namespace Player.View 
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Player View Setting")]
        [SerializeField] public Animator animator;
        [SerializeField] private SpriteRenderer circleRange;

        private float _speed = 1.2f;

        // input
        private float _horizontalInput;
        private float _verticalInput;

        // Components
        private Rigidbody _rb;
        private Transform _transform;

        // State Anim
        public readonly StateMachine<PlayerView> StateMachine = new();

        // Joystick
        private Joystick joystick;

        private void Start()
        {
            this._rb = GetComponent<Rigidbody>();
            this._transform = GetComponent<Transform>();
            this.StateMachine.Change(this, new PlayerIdleState());
            this.SetScaleFollowR();
        }

        private void Update()
        {
            // Check State Game
            this.GetInput();
        }

        private void FixedUpdate()
        {
            this.Rotate();
            this.Move();
        }

        private void GetInput()
        {
            // reset input
            this.ResetInput();

            // get input from joystick
            this._horizontalInput = joystick.Horizontal;
            this._verticalInput = joystick.Vertical;

            // Excute State 
            this.StateMachine.Current?.Execute(this);
        }

        private void ResetInput()
        {
            this._horizontalInput = 0;
            this._verticalInput = 0;
        }

        private void Move()
        {
            Vector3 moveDir = new Vector3(this._horizontalInput, 0, this._verticalInput);
            //moveDir = this._transform.right * this._horizontalInput + this._transform.forward * this._verticalInput;
            moveDir = moveDir.normalized;

            this._rb.MovePosition(this._transform.position + moveDir * this._speed * Time.deltaTime);
        }

        private void Rotate()
        {
            Vector3 inputDir = new Vector3(this._horizontalInput, 0, this._verticalInput);
            if (inputDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(inputDir);
                this._rb.MoveRotation(Quaternion.Slerp(this._transform.rotation, targetRotation, Time.deltaTime * 5f));
            }
        }

        public bool IsRunning()
        {
            return this._horizontalInput != 0 || this._verticalInput != 0;
        }

        public void SetupJoystick(Joystick joy)
        {
            this.joystick = joy;
        }
    
        private void SetScaleFollowR()
        {
            // Get Input
            float originR = this.circleRange.bounds.extents.x;
            float ratio = PlayerController.Instance.R / originR;
            Vector3 scale = this.circleRange.transform.localScale;

            // Set Scale
            this.circleRange.transform.localScale = new Vector3(scale.x * ratio, scale.y * ratio, scale.z * ratio);

        }
    }
}