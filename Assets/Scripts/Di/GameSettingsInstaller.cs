using UnityEngine;
using Zenject;

namespace ClickerTest.Di
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(_gameSettings).AsSingle();
        }
    }
}