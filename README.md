# SimpleStateMachine
- Install package
```csharp
dotnet add package OmidHosseini.StateMachineHandler --version 1.0.0
```
- Setup your rules

```csharp
SetRule<List<MyModel>>
  (
      myModel.GetMyModelList(), // your context
      (cfg, x) => new[] // your config
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
```

In this case, I use my own states that defined as Enum : 
```csharp
// this is just a sample
 public enum MyStates
  {
      New,
      Visited,
      Ordered,
      Paid,
      Sent,
  }
```

- Then you can check your states:
```csharp
bool canInsertNewStateAfterOrderedState = stateService.CanInsert(MyStates.New, MyStates.Ordered);
```
