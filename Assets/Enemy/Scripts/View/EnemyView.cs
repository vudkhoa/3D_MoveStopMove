using DG.Tweening;
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
        [SerializeField] private GameObject weaponParent;
        [SerializeField] private GameObject weaponPrefab;

        // Movement
        public bool IsRunning;
        public bool IsAttacking;
        public bool IsIdle;

        public int Index;
        private Vector3 targetPos;
        private GameObject _weapon;
        private bool _completedAttacking;
        private int _count;

        // Animation
        public readonly StateMachine<EnemyView> StateMachine = new();

        private void Start()
        {
            this.ChangeToIdle();
            this.targetPos = new Vector3(-1000, -1000, -1000);
            this.StateMachine.Change(this, new EnemyIdleState());
            this._completedAttacking = true;
            this._count = 0;
        }

        private void FixedUpdate()
        {
            StateMachine.Current?.Execute(this);
            EnemyController.Instance.CheckAttackPlayer(this);

            if (this.IsAttacking)
            {
                Rotate(PlayerController.Instance.Player.gameObject.transform.position);
                this.CheckFinishedAnim();
                return;
            }

            if ((this.IsIdle && !this.IsRunning) || (this.IsRunning && this.targetPos == new Vector3(-1000, -1000, -1000)))
            {
                EnemyController.Instance.ContinueMove(this);
            }

            if (Vector3.Distance(this.gameObject.transform.position, this.targetPos) <= 0.2f)
            {
                this.ChangeToIdle();
            }

            Move(this.targetPos);
            Rotate(this.targetPos);
        }

        public void CheckFinishedAnim()
        {
            AnimatorStateInfo info = new AnimatorStateInfo();
            info = this.animator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName("Attack") && (info.normalizedTime - this._count) >= 0.3f && (info.normalizedTime - this._count) < 0.4f)
            {

                if (this._completedAttacking)
                {
                    if (this._weapon == null)
                    {
                        this._weapon = Instantiate(this.weaponParent, this.weaponParent.transform);
                        this._weapon.gameObject.transform.SetParent(this.gameObject.transform);
                    }
                    else
                    {
                        this._weapon.transform.position = this.weaponParent.GetComponentInChildren<Transform>().position;
                        this._weapon.SetActive(true);
                    }
                    this.weaponParent.gameObject.SetActive(false);
                    this.ThrowWeapon(this.CaculateTime(info, (info.normalizedTime - this._count)));
                }
            }
            else if (info.IsName("Attack") && (info.normalizedTime - this._count) >= 0.8f && (info.normalizedTime - this._count) < 1f)
            {
                this.weaponParent.gameObject.SetActive(true);
            }
            else if (info.IsName("Attack") && (info.normalizedTime - this._count) >= 1f)
            {
                this._count++;
            }
        }


        private float CaculateTime(AnimatorStateInfo info, float ratio)
        {
            float time = info.length;
            time = time - ratio * time;
            return time;
        }

        public void ThrowWeapon(float time)
        {
            this._weapon.gameObject.SetActive(true);
            this._completedAttacking = false;
            Vector3 newPos = PlayerController.Instance.Player.transform.position;
            newPos.y = 0.5f;
            this._weapon.transform.DOMove(newPos, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                this._weapon.gameObject.SetActive(false);
                this._completedAttacking = true;
            });
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

        public void ChangeToAttack()
        {
            this.IsAttacking = true;
            this.IsIdle = false;
            this.IsRunning = false;
        }

        public void ChangeToRun()
        {
            this.IsRunning = true;
            this.IsAttacking = false;
            this.IsIdle = false;
        }

        public void ChangeToIdle()
        {
            this.IsIdle = true;
            this.IsAttacking = false;
            this.IsRunning = false;
        }

        public void SetTargetPos(Vector3 newPos, bool isRunning = true)
        {
            if (isRunning)
            {
                this.ChangeToRun();
            }

            if (this.IsRunning)
            {
                this.targetPos = newPos;
            }
            else
            {
                this.targetPos = new Vector3(-1000, -1000, -1000);
            }
        }

        public void ResetView()
        {
            if (this._weapon != null)
            {
                this._weapon.gameObject.SetActive(false);
            }


            if (this.weaponParent != null)
            {
                this.weaponParent.gameObject.SetActive(true);
            }
        }
    }
}