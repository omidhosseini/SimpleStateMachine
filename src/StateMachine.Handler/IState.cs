using System; 

namespace StateMachine.Handler
{
    public interface IState<TEnum>
    {
        StateMachineModel<TEnum>[] StateMachineModel { get; set; }
        
        
        /// <summary>
        /// Setup your rules
        /// </summary> 
        void SetRule<TContext>
        (
            TContext context,
            Func< StateMachineModel<TEnum>[],TContext, StateMachineModel<TEnum>[]> config
            
        );

        /// <summary>
        /// Use this method for checking the state that can be Insert after the current state
        /// this working according to the rules that already defined.
        /// </summary> 
        bool CanInsert(TEnum state, TEnum currentState);
        
    }
}