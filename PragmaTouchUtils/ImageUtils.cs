// Author: Ali Özgür (www.pragmatouch.com) 
// Contact: aliozgur79@gmail.com
// 
// Copyright (c) 2010 Ali Özgür
// 
//  PragmaTouch Licence
//  Version 1.0, September 2007  
// 
//  This licence, PragmaTouch Licence, applies to all PragmaTouch products published for
//  purchase or demonstration purposes over any media.
// 
//  This computer program is protected by copyright law and international
//  treaties.Unauthorized reproduction or distribution of this program, or any portion
//  of it, may result in severe civil and criminal penalties, and will be prosecuted 
//  to the maximum extent possible under the law.  

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;



namespace PragmaTouchUtils
{
  /// <summary>
  /// Helper class mainly proposed for byte[]->Image and Image->byte[]
  /// conversion
  /// </summary>
  public sealed class ImageUtils
  {

    /// <summary>
    /// Converts System.Drawing.Image instance to byte array
    /// </summary>
    /// <param name="SourceImg">Source image to be converted</param>
    /// <param name="ImgFormat">Format of the image</param>
    /// <returns>Byte array representation of the SourceImg</returns>
    public static byte[] ImageToByteArray(Image sourceImg)
    {
      byte[] returnBuffer;
      using ( MemoryStream ms = new MemoryStream() )
      {
        sourceImg.Save(ms, sourceImg.RawFormat);
        returnBuffer = ms.ToArray();
        return returnBuffer;
      }
    }

    /// <summary>
    /// Converts System.Drawing.Image instance to byte array
    /// </summary>
    /// <param name="SourceImg">Source image to be converted</param>
    /// <param name="ImgFormat">Format of the image</param>
    /// <returns>Byte array representation of the SourceImg</returns>
    public static byte[] ImageToByteArray(Image sourceImg, ImageFormat imageFormat)
    {
      byte[] returnBuffer;
      using ( MemoryStream ms = new MemoryStream() )
      {
        sourceImg.Save(ms, imageFormat);
        returnBuffer = ms.ToArray();
        return returnBuffer;
      }
    }

    /// <summary>
    /// Converts byte array to System.Drawing.Image 
    /// </summary>
    /// <param name="SourceArray">Source byte array to be converted</param>
    /// <returns>Returns image</returns>
    public static Image ByteArrayToImage(byte[] SourceArray)
    {
      using ( MemoryStream ms = new MemoryStream(SourceArray, 0, SourceArray.Length) )
      {
        ms.Write(SourceArray, 0, SourceArray.Length);
        return Image.FromStream(ms, true);
      }
    }

    public static Image ResizeImage(Image imgToResize, Size size)
    {
      int sourceWidth = imgToResize.Width;
      int sourceHeight = imgToResize.Height;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)size.Width / (float)sourceWidth);
      nPercentH = ((float)size.Height / (float)sourceHeight);

      if (nPercentH < nPercentW)
        nPercent = nPercentH;
      else
        nPercent = nPercentW;

      int destWidth = (int)(sourceWidth * nPercent);
      int destHeight = (int)(sourceHeight * nPercent);

      using ( Bitmap b = new Bitmap(destWidth, destHeight) )
      {
        Graphics g = Graphics.FromImage( (Image) b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return ( Image ) b;
      }
    }

    public static Image ResizeImageAsSmall(Image imgToResize)
    {
      if (imgToResize.Width > SmallImageSize.Width || imgToResize.Height > SmallImageSize.Height)
        return ResizeImage(imgToResize, SmallImageSize);
      else
        return imgToResize;
    }

    public static Image ResizeImageAsBig(Image imgToResize)
    {
      if (imgToResize.Width > BigImageSize.Width || imgToResize.Height > BigImageSize.Height)
        return ResizeImage(imgToResize, BigImageSize);
      else
        return imgToResize;
    }


    public static Size SmallImageSize = new Size();
    public static Size BigImageSize = new Size();

 
  
  }
}
