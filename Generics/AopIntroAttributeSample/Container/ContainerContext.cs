using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AopIntroAttributeSample.Container
{
    public class ContainerContext
    {
        private static object objLock = new object();
        private static IUnityContainer instance;

        private ContainerContext()
        {

        }

        public static IUnityContainer Instance
        {
            get
            {
                lock (objLock)
                {
                    if (instance == null)
                        instance = new UnityContainer();
                }

                return instance;
            }
            set { instance = value; }
        }

        public static void Load()
        {
            Instance.LoadConfiguration();
        }

        public static TSource Resolve<TSource>()
        {
            return Instance.Resolve<TSource>();
        }
         
    }
}
