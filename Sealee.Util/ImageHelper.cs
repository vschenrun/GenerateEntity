using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sealee.Util
{
    /// <summary>
    /// ImageHelper.IsSameImg(item.memberHeadimg, tkCustomer.headUrl)
    /// </summary>
    public class ImageHelper
    {
        public static Image UrlToImage(string url)
        {
            WebClient mywebclient = new WebClient();
            byte[] Bytes = mywebclient.DownloadData(url);
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                Image outputImg = Image.FromStream(ms);
                return outputImg;
            }
        }

        /// <summary>
        /// Image 转成 base64
        /// </summary>
        /// <param name="fileFullName"></param>
        public static string ImageToBase64(Image img)
        {
            try
            {
                Bitmap bmp = new Bitmap(img);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ImageToBase64(string url)
        {
            return ImageToBase64(UrlToImage(url));
        }

        #region 判断图片是否一致

        /// <summary>
        /// 判断图片是否一致
        /// </summary>
        /// <param name="img">图片一</param>
        /// <param name="bmp">图片二</param>
        /// <returns>是否一致</returns>
        public static bool IsSameImg(string imgUrl, string bmpUrl)
        {
            try
            {
                Bitmap img = new Bitmap(UrlToImage(imgUrl));

                Bitmap bmp = new Bitmap(UrlToImage(bmpUrl));

                if (!(img.Width == bmp.Width && img.Height == bmp.Height))
                {
                    img = KiResizeImage(img, bmp.Width, bmp.Height);
                }

                //大小一致
                if (img.Width == bmp.Width && img.Height == bmp.Height)
                {
                    //将图片一锁定到内存
                    BitmapData imgData_i = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                    IntPtr ipr_i = imgData_i.Scan0;
                    int length_i = imgData_i.Width * imgData_i.Height * 3;
                    byte[] imgValue_i = new byte[length_i];
                    Marshal.Copy(ipr_i, imgValue_i, 0, length_i);
                    img.UnlockBits(imgData_i);
                    //将图片二锁定到内存
                    BitmapData imgData_b = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                    IntPtr ipr_b = imgData_b.Scan0;
                    int length_b = imgData_b.Width * imgData_b.Height * 3;
                    byte[] imgValue_b = new byte[length_b];
                    Marshal.Copy(ipr_b, imgValue_b, 0, length_b);
                    img.UnlockBits(imgData_b);
                    //长度不相同
                    if (length_i != length_b)
                    {
                        return false;
                    }
                    else
                    {
                        //循环判断值
                        for (int i = 0; i < length_i; i++)
                        {
                            //不一致
                            if (imgValue_i[i] != imgValue_b[i])
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion 判断图片是否一致

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name= "bmp "> 原始Bitmap </param>
        /// <param name= "newW "> 新的宽度 </param>
        /// <param name= "newH "> 新的高度 </param>
        /// <returns> 处理以后的Bitmap </returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
