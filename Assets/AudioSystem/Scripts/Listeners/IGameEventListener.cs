namespace TocaAssignment
{
    public interface IGameEventListener
    {
        public void OnEventRaised();
        public void RegisterEvent();
        public void UnRegisterEvent();
        public GameEvent gameEvent { get; set; }
    }
}