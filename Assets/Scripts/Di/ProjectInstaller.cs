using ClickerTest.Controllers;
using ClickerTest.Services;
using Zenject;

namespace ClickerTest.Di
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickerController>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrencyService>().FromNew().AsSingle();
        }
    }
}