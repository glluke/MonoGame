namespace MonoGameContentProcessors.Processors
{
    using System;
    using System.ComponentModel;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content.Pipeline;
    using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
    using Microsoft.Xna.Framework.Content.Pipeline.Processors;

    using MonoGameContentProcessors.Content;

    [ContentProcessor(DisplayName = "ExtensionTextureProcessor")]
    public class ExtensionTextureProcessor : ContentProcessor<ExtensionTextureContent, TextureContent>
    {
        private const string PngExtension = ".png";

        private readonly MGTextureProcessor m_TextureProcessor = new MGTextureProcessor();

        private readonly Tuple<string, Action<MGTextureProcessor, object>>[] m_TextureProcessorParameters = 
        {
            new Tuple<string, Action<MGTextureProcessor, object>>("CompressionMode", (processor, value) => { processor.CompressionMode = (MGCompressionMode)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("ColorKeyColor", (processor, value) => { processor.ColorKeyColor = (Color)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("ColorKeyEnabled", (processor, value) => { processor.ColorKeyEnabled = (bool)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("GenerateMipmaps", (processor, value) => { processor.GenerateMipmaps = (bool)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("PremultiplyAlpha", (processor, value) => { processor.PremultiplyAlpha = (bool)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("ResizeToPowerOfTwo", (processor, value) => { processor.ResizeToPowerOfTwo = (bool)value; }),
            new Tuple<string, Action<MGTextureProcessor, object>>("TextureFormat", (processor, value) => { processor.TextureFormat = (TextureProcessorOutputFormat)value; })
        };

        [DefaultValue(MGCompressionMode.PVRTCFourBitsPerPixel)]
        public MGCompressionMode CompressionMode { get; set; }

        [DefaultValue(typeof(Color), "255, 0, 255, 255")]
        public Color ColorKeyColor { get; set; }

        [DefaultValue(false)]
        public bool ColorKeyEnabled { get; set; }

        [DefaultValue(false)]
        public bool GenerateMipmaps { get; set; }

        [DefaultValue(true)]
        public bool PremultiplyAlpha { get; set; }

        [DefaultValue(false)]
        public bool ResizeToPowerOfTwo { get; set; }

        [DefaultValue(TextureProcessorOutputFormat.Color)]
        public TextureProcessorOutputFormat TextureFormat { get; set; }

        public override TextureContent Process(ExtensionTextureContent input, ContentProcessorContext context)
        {
            context.Logger.LogWarning(null, null, "Platform is {0}", ContentHelper.GetMonoGamePlatform());

            foreach (var parameter in m_TextureProcessorParameters)
            {
                if (context.Parameters.ContainsKey(parameter.Item1))
                {
                    var value = context.Parameters[parameter.Item1];
                    parameter.Item2(m_TextureProcessor, value);
                }
            }

            if (input.TextureFileExtension.ToLowerInvariant() == PngExtension)
            {
                context.Logger.LogWarning(null, null, "Found PNG. Reset texture format parameter.");
                m_TextureProcessor.TextureFormat = TextureProcessorOutputFormat.NoChange;
            }

            return m_TextureProcessor.Process(input.ActualTextureContent, context);
        }
    }
}