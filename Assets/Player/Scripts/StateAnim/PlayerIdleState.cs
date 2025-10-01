using Player.View;
using Utils.DesignPattern.State;

namespace Player.StateAnim
{
    public class PlayerIdleState : IState<PlayerView>
    {
        public void Enter(PlayerView player) { }

        public void Execute(PlayerView player)
        {
            //Debug.Log("Player is Idle");
            if (player.IsRunning())
            {
                player.StateMachine.Change(player, new PlayerRunningState());
            }
        }

        public void Exit(PlayerView player) { }
    }
}