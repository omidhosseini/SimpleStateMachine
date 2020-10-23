namespace StateMashine.Handler
{
    public class StateMachineModel<TEnum>
    {
        public TEnum CurrentState { get; set; }
        public TEnum[] CanBeChangedTo { get; set; }
        public bool CanBeChanged { get; set; } 
    }
}