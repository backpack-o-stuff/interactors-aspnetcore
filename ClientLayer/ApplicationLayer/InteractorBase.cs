using System;

namespace BOS.ClientLayer.ApplicationLayer
{
    public class InteractorBase
    {
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
    }
}