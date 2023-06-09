using Jacobi.Vst.Plugin.Framework;

namespace VstNetMidiPlugin.Dmp
{
    internal sealed class GainParameters
    {
        private const string ParameterCategoryName = "Gain";

        public GainParameters(PluginParameters parameters)
        {
            InitializeParameters(parameters);
        }

        private void InitializeParameters(PluginParameters parameters)
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = parameters.ParameterInfos;

            // retrieve the category for all delay parameters.
            VstParameterCategory paramCategory =
                parameters.GetParameterCategory(ParameterCategoryName);

            // delay time parameter
            var paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Index",
                Label = "Db",
                ShortLabel = "Db",
                MinInteger = 0,
                MaxInteger = 16,
                LargeStepFloat = 1f,
                SmallStepFloat = 1.0f,
                StepFloat = 1f,
                DefaultValue = 0.0f
            };

            GainMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);
        }

        public VstParameterManager GainMgr { get; private set; }
    }
}
