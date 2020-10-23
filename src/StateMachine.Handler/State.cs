using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using StateMachine.Handler;

namespace StateMashine.Handler
{
    public class State<TEnum> : IState<TEnum>
        where TEnum : Enum
    {
        public StateMachineModel<TEnum>[] StateMachineModel { get; set; }
        

        private readonly ConcurrentDictionary<TEnum, (TEnum[] Dest, bool CanInsertAfter)> _rules =
            new ConcurrentDictionary<TEnum, (TEnum[], bool)>();


        public void SetRule<TContext>
        (
            TContext context,
            Func<StateMachineModel<TEnum>[],TContext, StateMachineModel<TEnum>[]> config 
        )
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));


            var rules = config(StateMachineModel,context);


            foreach (var rule in rules)
            {
                _rules.TryAdd(rule.CurrentState, (rule.CanBeChangedTo, rule.CanBeChanged));
            }
        }

        public bool CanInsert(TEnum state, TEnum currentState)
        {
            var res = _rules.TryGetValue(state, out var found);

            if (!res)
                return false;

            var dest = found.Dest.FirstOrDefault(x => x.Equals(currentState));

            if (dest == null)
                return false;

            return found.CanInsertAfter;
        }
    }
}