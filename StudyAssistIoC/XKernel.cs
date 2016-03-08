using Ninject;
using StudyAssistInterfaces;
using StudyAssistModel;

namespace StudyAssistIoC
{
    public class XKernel : StandardKernel
    {
        private static XKernel _instance;

        static XKernel()
        {
            _instance = new XKernel();            
        }

        public static XKernel Instance
        {
            get { return _instance; }            
        }

        private XKernel()
        {
            Bind<IModel>().To<XModel>().InSingletonScope();
            Bind<ICategory>().To<XCategory>();
            Bind<ITheme>().To<XTheme>();
            Bind<IProblem>().To<XProblem>();

        }
    }
}
