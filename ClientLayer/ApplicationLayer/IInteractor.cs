namespace BOS.ClientLayer.ApplicationLayer
{
    public interface IInteractor<TResult, TRequest>
        where TResult : class
    {
        InteractorResult<TResult> Call(TRequest request);
    }
}