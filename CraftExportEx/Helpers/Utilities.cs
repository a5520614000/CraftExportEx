using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using CraftExportEx;

namespace CraftExport.Helpers
{
    static class Utilities
    {
        /// <summary>
        /// Counts the number of controls inside a specific object.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            // Get all sub items from the passed control object.
            var controls = control.Controls.Cast<Control>();

            // Return the controls of the specified type inside the passed control object.
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        /// <summary>
        /// Checks the passed ClassJobType and returns the baseclass.
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static ClassJobType ReturnBaseClass(ClassJobType classType)
        {
            switch (classType)
            {
                case ClassJobType.Carpenter:
                    return ClassJobType.Carpenter;
                case ClassJobType.Blacksmith:
                    return ClassJobType.Blacksmith;
                case ClassJobType.Armorer:
                    return ClassJobType.Armorer;
                case ClassJobType.Goldsmith:
                    return ClassJobType.Goldsmith;
                case ClassJobType.Leatherworker:
                    return ClassJobType.Leatherworker;
                case ClassJobType.Weaver:
                    return ClassJobType.Weaver;
                case ClassJobType.Alchemist:
                    return ClassJobType.Alchemist;
                case ClassJobType.Culinarian:
                    return ClassJobType.Culinarian;
                default:
                    return ClassJobType.Carpenter;
            }
        }

        /// <summary>
        /// Checks if the passed classtype is a crafting class.
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static bool IsCraftingClass(ClassJobType classType)
        {
            switch (classType)
            {
                case ClassJobType.Carpenter:
                case ClassJobType.Blacksmith:
                case ClassJobType.Armorer:
                case ClassJobType.Goldsmith:
                case ClassJobType.Leatherworker:
                case ClassJobType.Weaver:
                case ClassJobType.Alchemist:
                case ClassJobType.Culinarian:
                    return true;
                default:
                    return false;
            }
        }

        public static Bitmap ChangeOpacity(Image image, float opacityValue)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            ColorMatrix colorMatrix = new ColorMatrix{ Matrix33 = opacityValue };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();

            return bitmap;
        }
    }
}
