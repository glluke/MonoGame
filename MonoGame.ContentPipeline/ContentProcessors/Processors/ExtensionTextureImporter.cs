namespace MonoGameContentProcessors.Processors
{
    using System.IO;

    using Microsoft.Xna.Framework.Content.Pipeline;

    using MonoGameContentProcessors.Content;

    [ContentImporter(".png,*.bmp,*.tga", DisplayName = "Extension Texture Importer", DefaultProcessor = "ExtensionTextureProcessor")]
    public class ExtensionTextureImporter : ContentImporter<ExtensionTextureContent>
    {
        private readonly TextureImporter m_TextureImporter = new TextureImporter();

        public override ExtensionTextureContent Import(string filename, ContentImporterContext context)
        {
            return new ExtensionTextureContent(m_TextureImporter.Import(filename, context), Path.GetExtension(filename));
        }
    }
}
