using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DataAccess.Context;

namespace GenericUnitOfWorkSample.Context
{
    public class ContextFactory
    {
        public static BaseContext Create()
        {
            //Eski hali
            //return new MessageContext();
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration(); 
            BaseContext context = container.Resolve<BaseContext>();
            return context;
        }
    }
}
