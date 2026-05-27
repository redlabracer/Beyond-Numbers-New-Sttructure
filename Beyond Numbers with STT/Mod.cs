using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Beyond_Numbers_with_STT.Systems;
using Unity.Entities;

namespace Beyond_Numbers_with_STT
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger(nameof(Mod)).SetShowsErrorsInUI(false);
        public static Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info("Beyond Numbers Mod loaded!");

            m_Setting = new Setting(this);

            AssetDatabase.global.LoadSettings(nameof(Beyond_Numbers_with_STT), m_Setting, new Setting(this));

            m_Setting.RegisterInOptionsUI();

            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            GameManager.instance.localizationManager.AddSource("de-DE", new LocaleDE(m_Setting));

            updateSystem.UpdateAt<BeyondNumbersUISystem>(SystemUpdatePhase.UIUpdate);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }

        /// <summary>
        /// Pushes the current setting values into the UI bindings.
        /// Called from setting setters — never calls ApplyAndSave / Apply
        /// and never injects JavaScript. The UI module reads the bindings
        /// and updates the DOM by itself.
        /// </summary>
        public static void PushBindings()
        {
            if (m_Setting == null) return;

            foreach (var world in World.All)
            {
                var ui = world.GetExistingSystemManaged<BeyondNumbersUISystem>();
                if (ui != null)
                {
                    ui.UpdateBindings();
                    return;
                }
            }
        }
    }
}
