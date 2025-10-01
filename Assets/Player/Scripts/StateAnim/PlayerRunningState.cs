using Player.View;
using Utils.DesignPattern.State;

namespace Player.StateAnim
{
    public class PlayerRunningState : IState<PlayerView>
    {
        public void Enter(PlayerView player)
        {
            player.animator.SetBool("IsRunning", true);
        }

        public void Execute(PlayerView player)
        {
            if (!player.IsRunning())
            {
                player.StateMachine.Change(player, new PlayerIdleState());
            }
        }

        public void Exit(PlayerView player)
        {
            player.animator.SetBool("IsRunning", false);
        }
    }
}