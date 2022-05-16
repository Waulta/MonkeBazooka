using ComputerInterface.Interfaces;
using Zenject;

namespace MonkeBazooka.ComputerInterface
{
    class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            base.Container.Bind<IComputerModEntry>().To<MonkeBazookaEntry>().AsSingle();
        }
    }
}