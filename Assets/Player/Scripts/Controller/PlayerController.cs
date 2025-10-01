using Player.View;
using UnityEngine;
using Utils.DesignPattern.Singleton;
using CameraFollow;
using Enemy.Controller;

namespace Player.Controller
{ 
    public class PlayerController : SingletonMono<PlayerController>
    {
        [Header("Player Controller Setting")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] public Vector3 PositionInit;
        [SerializeField] public CameraFollow.CameraFollow mainCamera;
        [SerializeField] private Joystick joystick;
        [SerializeField] public float R;

        [Header(" Game Playing ")]
        public PlayerView Player;

        private void Start()
        {
            this.Init();
        }

        private void Init()
        {
            this.SpawnPlayer();
            this.mainCamera.SetupTarget(this.Player);
            EnemyController.Instance.Init();
        }

        private void SpawnPlayer()
        {
            this.Player = Instantiate(this.playerPrefab, PositionInit, Quaternion.identity, this.transform);
            this.Player.SetupJoystick(this.joystick);
        }
    }
} 