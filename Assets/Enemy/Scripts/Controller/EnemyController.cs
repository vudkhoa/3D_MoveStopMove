using Enemy.StateAnim;
using Enemy.View;
using Map.Controller;
using Player.Controller;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPattern.ObjectPooling;
using Utils.DesignPattern.Singleton;

namespace Enemy.Controller
{
    public class EnemyController : SingletonMono<EnemyController>
    {
        [Header("Enemy Controller Setting")]
        [SerializeField] private EnemyView enemyPrefab;
        [SerializeField] private int count;
        [SerializeField] public float speed;
        [SerializeField] private ObjectPool objectPool;

        private List<EnemyView> _enemies;
        public List<float> EnemySpeed;

        public void Init() 
        {  
            this._enemies = new List<EnemyView>();
            MapController.Instance.MaxTryCount = this.count * MapController.Instance.maxTryRatio;
            this.SpawnEnemys(count);
        }

        private void SpawnEnemys(int count)
        {
            for (int i = 0; i < count; i++)
            {
                MapController.Instance.MaxTryCount = this.count * MapController.Instance.maxTryRatio;
                Vector3 spawnPos = MapController.Instance.GetEmptyPosFollowR(PlayerController.Instance.PositionInit, 10f);

                if (MapController.Instance.MaxTryCount <= 0)
                {
                    return;
                }

                if (spawnPos != new Vector3(-1000f, -1000f, -1000f))
                {
                    spawnPos.y = 0f;
                    
                    EnemyView enemy = this.objectPool.GetPooledObject().GetComponent<EnemyView>();
                    enemy.transform.position = spawnPos;
                    enemy.gameObject.SetActive(true);
                    enemy.name = "Enemy " + i;
                    enemy.Index = i;
                    this.ContinueMove(enemy);
                    
                    
                    this.EnemySpeed.Add(this.speed);
                    this._enemies.Add(enemy);
                }
            }
            
        }

        public void ContinueMove(EnemyView e)
        {
            if (e.IsAttacking) return;

            MapController.Instance.MaxTryCount = this.count * MapController.Instance.maxTryRatio;
            Vector3 newPos = MapController.Instance.GetEmptyPosFollowR(e.transform.position, 10f);
            //Debug.Log(newPos);
            if (newPos != new Vector3(-1000f, -1000f, -1000f))
            {
                newPos.y = 0f;
                e.SetTargetPos(newPos);
            }
            else
            {
                e.SetTargetPos(newPos, false);
            }
        }

        public bool CheckAttackPlayer(EnemyView e)
        {
            bool isCollide = false;

            if (Vector3.Distance(e.gameObject.transform.position, PlayerController.Instance.Player.gameObject.transform.position) 
                <= PlayerController.Instance.R)
            {
                e.ChangeToAttack();
                e.SetTargetPos(new Vector3(-1000, -1000, -1000), false);
                this.EnemySpeed[e.Index] *= 5f;
            }
            else
            {
                if (e.IsAttacking)
                {
                    e.ChangeToIdle();
                }
                this.EnemySpeed[e.Index] = this.speed;
                e.ResetView();
            }

            return isCollide;
        }
    }
}