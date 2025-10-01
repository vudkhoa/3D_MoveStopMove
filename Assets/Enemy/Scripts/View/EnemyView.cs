using Enemy.Controller;
using Enemy.StateAnim;
using Player.Controller;
using UnityEngine;
using Utils.DesignPattern.State;

namespace Enemy.View
{
    public class EnemyView : MonoBehaviour
    {
        [Header("Enemy View Setting")]
        [SerializeField] public Animator animator;
        [SerializeField] private Rigidbody rb;

        // Movement
        public bool IsRunning;
        public bool IsAttacking;
        public int Index;
        private Vector3 targetPos;

        // Animation
        public readonly StateMachine<EnemyView> StateMachine = new();

        private void Start()
        {
            this.IsRunning = false;
            this.targetPos = new Vector3(-1000, -1000, -1000);
            this.StateMachine.Change(this, new EnemyIdleState());
        }

        private void FixedUpdate()
        {
            StateMachine.Current?.Execute(this);

            if (this.IsAttacking)
            {
                Rotate(PlayerController.Instance.Player.gameObject.transform.position);
                return;
            }


            if (!this.IsRunning && this.targetPos == new Vector3(-1000, -1000, -1000))
            {
                EnemyController.Instance.ContinueMove(this);
                return;
            }

            if (Vector3.Distance(this.targetPos, this.transform.position) <= 0.1f)
            {
                this.SetTargetPos(new Vector3(-1000, -1000, -1000), false);
                return;
            }

            Move(this.targetPos);
            Rotate(this.targetPos);
            EnemyController.Instance.CheckAttackPlayer(this);
        }

        public void Move(Vector3 newPos)
        {
            Vector3 moveDir = newPos - this.transform.position;
            moveDir = moveDir.normalized;

            this.rb.MovePosition(this.transform.position + moveDir * Time.deltaTime * EnemyController.Instance.EnemySpeed[Index]);
        }

        public void Rotate(Vector3 targetPosition)
        {
            Vector3 rotateDir = targetPosition - this.transform.position;
            rotateDir = rotateDir.normalized;
            rotateDir.y = 0;

            if (rotateDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rotateDir);
                this.rb.MoveRotation(Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * EnemyController.Instance.EnemySpeed[Index]));
            }
        }

        public void SetTargetPos(Vector3 newPos, bool isMoving = true)
        {
            this.IsRunning = isMoving;

            if (this.IsRunning)
            {
                this.targetPos = newPos;
            }
            else
            {
                this.targetPos = new Vector3(-1000, -1000, -1000);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.ToString() == "Obstacle")
            {
                this.SetTargetPos(new Vector3(-1000, -1000, -1000), false);
            }
        }
    }
}