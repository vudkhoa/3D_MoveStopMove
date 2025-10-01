using Enemy.View;
using UnityEngine;
using Utils.DesignPattern.State;

namespace Enemy.StateAnim
{
    public class EnemyIdleState : IState<EnemyView>
    {
        public void Enter(EnemyView enemy) { }

        public void Execute(EnemyView enemy)
        {
            if (enemy.IsRunning)
            {
                enemy.StateMachine.Change(enemy, new EnemyRunningState());
            }
        }

        public void Exit(EnemyView enemy) { }
    }
}