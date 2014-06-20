// -----------------------------------------------------------------------
// <copyright file="ExtensionTextureContent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGameContentProcessors.Content
{
    using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

    /// <summary>
    /// A wrapper TextureContent class used to deliver the texture file name extension to the content processor.
    /// </summary>
    public class ExtensionTextureContent 
    {
        public ExtensionTextureContent(TextureContent actualTextureContent, string textureFileExtension)
        {
            ActualTextureContent = actualTextureContent;
            TextureFileExtension = textureFileExtension;
        }

        public TextureContent ActualTextureContent { get; private set; }

        public string TextureFileExtension { get; private set; }
    }
}
