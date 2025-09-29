namespace Utils.DesignPattern.State
{
    public class StateMachine <TOwner>
    {
        public IState<TOwner> Current { get; private set; }
        
        public void Change(TOwner owner, IState<TOwner> next)
        {
            Current?.Exit(owner);
            Current = next;
            Current?.Enter(owner);
        }
    }
}