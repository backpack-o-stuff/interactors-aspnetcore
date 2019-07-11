# interactors-aspnetcore

Interactor pattern and helpers in ASP.NET Core.

---

## Setup 

#### Built With

- ASP.NET Core 2.2

---

## Intent

#### Application Services

The interactor is a service object. It constrains the service object to a single business need/responsibilty. The concept here is to pass in a request from say your controller or an interactor coordinator, and then it handles the flow of logic for that unit of logic.

#### Interactor Structure/Style

The interactor service object houses a single business need, therefor they should have a single publicly accessable entry point. Their dependencies needed to perform the logic are passed in and it returns a shared contract that can be reasoned about in the system's layers above.

Interactor Contract
```
public interface IInteractor<TResult, TRequest>
    where TResult : class
{
    InteractorResult<TResult> Call(TRequest request);
}
```

Example Contract
```
public interface ICreateMonsterInteractor 
    : IInteractor<Monster, CreateMonsterRequest> {}
```

Interactor Implementation
```
public InteractorResult<Monster> Call(CreateMonsterRequest request)
{
    return PerformCall(() => 
    {
        var monster = _monsterFactory.For(request);

        var monsterValidation = monster.Validate();
        if(!monsterValidation.IsValid)
            return InteractorResult<Monster>.ForFailure(monsterValidation.Messages);

        var result = _monsterRepository.Add(monster);
        return InteractorResult<Monster>.ForSuccess(result);
    });
}
```

In this system we are stating that the Interactors are safe to call and handle exceptions. See this PerformCall inline delegate caller implementation:
```
protected InteractorResult<T> PerformCall<T>(Func<InteractorResult<T>> performCall)
    where T : class
{
    try
    {
        return performCall();
    }
    catch (Exception e)
    {
        return InteractorResult<T>.ForFailure(e);
    }
}
```

#### Interactor Coordniators

Eventually you will want to chain a series of interactors together to form a larger 'process'. In such cases, an Interactor Coordinator with the same style of use Result Call(Request) would be created to wrap the coordination of and between the interactors it wraps to create a larger business need/case.

```
ex. playerDefeatedByMonsterInteractor

Would call and coordinate the following interactors:
    defeatPlayerInterator
    levelupMonsterInteractor
    monsterScoresPlayerDefeatInteractor
```

