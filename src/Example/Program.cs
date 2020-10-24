using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StateMachine.Handler; 

namespace ConsoleApp1
{
    class Program
    {
        public MyStates MyStates { get; set; }

        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped(typeof(IState<>), typeof(State<>));

            var myModel = new MyModel();
            
            using (var sp = services.BuildServiceProvider())
            {
                var stateService = sp.GetService<IState<MyStates>>();


                // Set your rules like this: 
                stateService.SetRule<List<MyModel>>
                (
                    myModel.GetMyModelList(),
                    (cfg, x) => new[]
                    {
                        new StateMachineModel<MyStates>()
                        {
                            CurrentState = MyStates.New,
                            CanBeChangedTo = new[] {MyStates.New, MyStates.Ordered, MyStates.Paid},
                            CanBeChanged = x.Any(s => s.Age < 20)
                        },
                        new StateMachineModel<MyStates>()
                        {
                            CurrentState = MyStates.Ordered,
                            CanBeChangedTo = new[] {MyStates.New, MyStates.Visited},
                            CanBeChanged = true
                        },
                        new StateMachineModel<MyStates>()
                        {
                            CurrentState = MyStates.Sent,
                            CanBeChangedTo = new[] {MyStates.New, MyStates.Ordered, MyStates.Paid},
                            CanBeChanged = false
                        }
                    }
                );

                
                var t1 = stateService.CanInsert(MyStates.New, MyStates.Ordered);
                var t2 = stateService.CanInsert(MyStates.Sent, MyStates.Ordered);
                var t3 = stateService.CanInsert(MyStates.Ordered, MyStates.Visited);
                var t4 = stateService.CanInsert(MyStates.Sent, MyStates.Visited);


                Console.WriteLine("Hello World!");
                Console.WriteLine($" t1: {t1}\r\n t2: {t2}\r\n t3: {t3}\r\n t4: {t4}");
                Console.ReadLine();
            }
        }
    }
}