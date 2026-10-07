using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Beyond_Numbers_with_STT.Systems;
using Unity.Entities;
using UnityEngine.Profiling;

namespace Beyond_Numbers_with_STT
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger("Beyond Numbers").SetShowsErrorsInUI(false);
        public static Setting m_Setting;

        private const string ProfilerPrefix = "BeyondNumbers.";

        public void OnLoad(UpdateSystem updateSystem)
        {
            Profiler.BeginSample(ProfilerPrefix + nameof(OnLoad));

            log.Info("Beyond Numbers Mod loaded!");

            m_Setting = new Setting(this);

            m_Setting.RegisterInOptionsUI();

            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            GameManager.instance.localizationManager.AddSource("de-DE", new LocaleDE(m_Setting));

            AssetDatabase.global.LoadSettings(nameof(Beyond_Numbers_with_STT), m_Setting, new Setting(this));

            PushBindings();

            updateSystem.UpdateAt<BeyondNumbersUISystem>(SystemUpdatePhase.UIUpdate);

            Profiler.EndSample();
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

        public static void PushBindings()
        {
            Profiler.BeginSample(ProfilerPrefix + nameof(PushBindings));

            if (m_Setting == null)
            {
                Profiler.EndSample();
                return;
            }

            foreach (var world in World.All)
            {
                var ui = world.GetExistingSystemManaged<BeyondNumbersUISystem>();
                if (ui != null)
                {
                    ui.UpdateBindings();
                    Profiler.EndSample();
                    return;
                }
            }

            Profiler.EndSample();
        }
    }
}
