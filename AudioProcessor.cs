﻿using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework.Plugin;
using System;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// This object is a dummy AudioProcessor only to be able to output Midi during the Audio processing cycle.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessor
    {
        // TODO: set some defaults
        private const int AudioInputCount = 2;
        private const int AudioOutputCount = 2;
        private const int InitialTailSize = 0;

        private readonly MidiProcessor _midiProcessor;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioProcessor(MidiProcessor midiProcessor)
            : base(AudioInputCount, AudioOutputCount, InitialTailSize, noSoundInStop: true)
        {
            _midiProcessor = midiProcessor ?? throw new ArgumentNullException(nameof(midiProcessor));
        }

        /// <summary>
        /// Called by the host to allow the plugin to process audio samples.
        /// </summary>
        /// <param name="inChannels">Never null.</param>
        /// <param name="outChannels">Never null.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            // calling the base class transfers input samples to the output channels unchanged (bypass).
            base.Process(inChannels, outChannels);

            // check to see if we need to output midi here
            if (_midiProcessor.SyncWithAudioProcessor)
            {
                _midiProcessor.ProcessCurrentEvents();
            }
        }
    }
}
