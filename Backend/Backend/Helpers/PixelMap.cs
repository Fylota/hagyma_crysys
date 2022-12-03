using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Backend.Helpers;

using SysColor = Color;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
// Helper class found on the internet to convert images from ppm to a more common image type
public class PixelMap
{
    private PixelMapHeader _header;

    public PixelMap(Stream stream)
    {
        FromStream(stream);
    }

    public Bitmap BitMap { get; private set; } = null!;

    private byte[] ImageData { get; set; } = null!;

    private int BytesPerPixel { get; set; }

    private int Stride { get; set; }

    private PixelFormat PixelFormat { get; set; }


    private Bitmap CreateBitMap()
    {
        var pImageData = Marshal.AllocHGlobal(ImageData.Length);
        Marshal.Copy(ImageData, 0, pImageData, ImageData.Length);
        var bitmap = new Bitmap(_header.Width, _header.Height, Stride, PixelFormat, pImageData);
        return bitmap;
    }

    [SuppressMessage("ReSharper", "TooWideLocalVariableScope")]
    private Bitmap CreateBitmapOffSize()
    {
        var bitmap = new Bitmap(_header.Width, _header.Height, PixelFormat.Format24bppRgb);
        // ReSharper disable once RedundantAssignment
        Color sysColor;
        int red, green, blue;
        int index;

        for (var x = 0; x < _header.Width; x++)
            for (var y = 0; y < _header.Height; y++)
            {
                index = x + y * _header.Width;

                switch (_header.MagicNumber)
                {
                    case "P6":
                        index = 3 * index;
                        blue = ImageData[index];
                        green = ImageData[index + 1];
                        red = ImageData[index + 2];
                        sysColor = SysColor.FromArgb(red, green, blue);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                bitmap.SetPixel(x, y, sysColor);
            }


        return bitmap;
    }

    public static Bitmap ResizeImage(Image image, int maxSize)
    {
        int width, height;
        if (image.Height < image.Width)
        {
            width = maxSize;
            height = (int)Math.Round(maxSize * ((float)image.Height / image.Width));
        }
        else
        {
            height = maxSize;
            width = (int)Math.Round(maxSize * ((float)image.Width / image.Height));
        }

        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using var graphics = Graphics.FromImage(destImage);
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        using var wrapMode = new ImageAttributes();
        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

        return destImage;
    }

    private static int ReadValue(BinaryReader binReader)
    {
        var value = new StringBuilder();
        while (!char.IsWhiteSpace((char)binReader.PeekChar())) value.Append(binReader.ReadChar());
        binReader.ReadByte();
        return int.Parse(value.ToString());
    }

    private void FromStream(Stream stream)
    {
        _header = new PixelMapHeader();
        var headerItemCount = 0;
        var binReader = new BinaryReader(stream);
        try
        {
            while (headerItemCount < 4)
            {
                var nextChar = (char)binReader.PeekChar();
                if (nextChar == '#') // comment
                    while (binReader.ReadChar() != '\n')
                    {
                        //Not doing anything
                    }

                else if (char.IsWhiteSpace(nextChar))
                    binReader.ReadChar();
                else
                    switch (headerItemCount)
                    {
                        case 0: // next item is Magic Number
                            // Read the first 2 characters and determine the type of pixel map.
                            var chars = binReader.ReadChars(2);
                            _header.MagicNumber = chars[0] + chars[1].ToString();
                            headerItemCount++;
                            break;
                        case 1: // next item is the width.
                            _header.Width = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        case 2: // next item is the height.
                            _header.Height = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        case 3: // next item is the depth.
                            _header.Depth = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        default:
                            throw new ArgumentException("Error parsing the file header.");
                    }
            }

            switch (_header.MagicNumber)
            {
                case "P6": // 3 bytes per pixel
                    PixelFormat = PixelFormat.Format24bppRgb;
                    BytesPerPixel = 3;
                    break;
                default:
                    throw new ArgumentException("Unknown Magic Number: " + _header.MagicNumber);
            }

            ImageData = new byte[_header.Width * _header.Height * BytesPerPixel];
            Stride = _header.Width * BytesPerPixel;
            var bytesLeft = (int)(binReader.BaseStream.Length - binReader.BaseStream.Position);
            ImageData = binReader.ReadBytes(bytesLeft);
            ReorderBGRtoRGB();
            BitMap = Stride % 4 == 0 ? CreateBitMap() : CreateBitmapOffSize();
            BitMap.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        catch (EndOfStreamException e)
        {
            Console.WriteLine(e.Message);
            throw new ArgumentException("Error reading the stream! ", e);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new ArgumentException("Error reading the stream! ", ex);
        }
        finally
        {
            binReader.Close();
        }
    }

    // ReSharper disable once InconsistentNaming
    private void ReorderBGRtoRGB()
    {
        var tempData = new byte[ImageData.Length];
        for (var i = 0; i < ImageData.Length; i++) tempData[i] = ImageData[ImageData.Length - 1 - i];
        ImageData = tempData;
    }

    [Serializable]
    public struct PixelMapHeader
    {
        public string MagicNumber { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public int Depth { get; set; }
    }
}
