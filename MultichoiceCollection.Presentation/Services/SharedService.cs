using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Hangfire;
using Hangfire.Annotations;
using Microsoft.Win32;
using MultichoiceCollection.Common.Entities.Enum;
using MultichoiceCollection.Presentation.Controllers;

namespace MultichoiceCollection.Presentation.Services
{
    public class SharedService
    {
        /// <summary>
        /// Naira symbol
        /// </summary>
        public const string NairaSymbol = "₦";
       
        /// <summary>
        /// Normal messaging key
        /// </summary>
        public const string GetGlobalMessageKey = "notification";

        /// <summary>
        /// 
        /// </summary>
        public const string GetCompanyName = "Multichoice Collection";

        public static string GetCompanyOwner = "Enter owner here";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidateEmail([NotNull] string email)
        {

            return new EmailAddressAttribute().IsValid(email);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool ValidatePhoneNumber([NotNull] string phoneNumber)
        {

            return new PhoneAttribute().IsValid(phoneNumber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            var mimeType = "application/unknown";
            var extension = Path.GetExtension(fileName);
            if (extension != null)
            {
                var ext = extension.ToLower();
                var regKey = Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }
        public static void DownloadContentFromFolder(string path)
        {
            var filename = Path.GetFileName(path);
            var contentType = GetMimeType(filename);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = contentType;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(path));
            HttpContext.Current.Response.End();
        }

        public static string SaveImage(byte[] image, string imageFullDirectory, int offsetX, int offsetY, int imageWidth,
            int imageHeight, int cropWidth, int cropHeight, string saveToType = "jpg", long quality = 75)
        {
            var imageStream = CropImage(image, offsetX, offsetY, imageWidth, imageHeight, cropWidth, cropHeight, quality);
            var ms = new MemoryStream(imageStream);
            var mainStream = Image.FromStream(ms);
            if (!Directory.Exists(imageFullDirectory))
                Directory.CreateDirectory(imageFullDirectory);
            var fileName = Guid.NewGuid().ToString("N") + "." + saveToType;
            mainStream.Save(imageFullDirectory + "/" + fileName);
            return fileName;
        }

        #region ImageUpload
        public static byte[] CropImage(byte[] content, int offsetX, int offsetY, int imageWidth, int imageHeight, int cropWidth, int cropHeight, long quality)
        {
            using (var stream = new MemoryStream(content))
            {
                return CropImage(stream, offsetX, offsetY, imageWidth, imageHeight, cropWidth, cropHeight, quality);
            }
        }

        private static byte[] CropImage(Stream content, int offsetX, int offsetY, int imageWidth, int imageHeight, int cropWidth, int cropHeight, long quality)
        {
            //Parsing stream to bitmap
            using (var sourceBitmap = new Bitmap(content))
            {

                //Get new dimensions
                var sourceWidth = Convert.ToDouble(sourceBitmap.Size.Width);
                var sourceHeight = Convert.ToDouble(sourceBitmap.Size.Height);
                var cropRect = new Rectangle(offsetX, offsetY, imageWidth, imageHeight);


                //Creating new bitmap with valid dimensions
                using (var newBitMap = new Bitmap(cropWidth, cropHeight))
                {
                    using (var g = Graphics.FromImage(newBitMap))
                    {
                        g.Clear(Color.White);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawImage(sourceBitmap, new Rectangle(0, 0, newBitMap.Width, newBitMap.Height), cropRect, GraphicsUnit.Pixel);
                        return GetBitmapBytes(newBitMap, quality);
                    }
                }
            }
        }

        private static byte[] GetBitmapBytes(Bitmap source, long quality)
        {
            //Settings to increase quality of the image
            var codec = GetEncoderInfo("image/jpeg");
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //Temporary stream to save the bitmap
            using (var tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                //Get image bytes from temporary stream
                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);
                return result;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            var encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        #endregion
        /// <summary>
        /// Used for deleting a file in a given directory
        /// </summary>
        /// <param name="filepath">The relative name of the file. e.g ~/images/example.png</param>
        public static void DeleteFileInDirectory(string filepath)
        {
            try
            {
                var fullPath = HttpContext.Current.Server.MapPath(filepath);
                 if (File.Exists(fullPath))
                    File.Delete(fullPath);
               }
            catch (Exception)
            {
                //ignore
            }

        }
        

       /// <summary>
        /// This function is used to format currency. e.g 2,463
        /// </summary>
        /// <param name="money">Decimal to format</param>
        /// <returns></returns>
        public static string FormatCurrency(decimal money)
        {
           return NairaSymbol + money.ToString("N");
        }

        public static string GetCronForSelectedRecurringBillingInterval(RecurringBillingInterval paymentIntervals)
        {
            /*
# ┌───────────── min (0 - 59)
# │ ┌────────────── hour (0 - 23)
# │ │ ┌─────────────── day of month (1 - 31)
# │ │ │ ┌──────────────── month (1 - 12)
# │ │ │ │ ┌───────────────── day of week (0 - 6) (0 to 6 are Sunday to
# │ │ │ │ │                  Saturday, or use names; 7 is also Sunday)
# │ │ │ │ │
# │ │ │ │ │
# * * * * *  command to execute

@yearly (or @annually)	Run once a year at midnight of 1 January	0 0 1 1 *
@monthly	Run once a month at midnight of the first day of the month	0 0 1 * *
@weekly	Run once a week at midnight on Sunday morning	0 0 * * 0
@daily	Run once a day at midnight	0 0 * * *
@hourly	Run once an hour at the beginning of the hour	0 * * * *
@reboot	Run at startup	N/A
                */
           return Cron.Minutely();
           /* switch (paymentIntervals)
            {
                case RecurringBillingInterval.Daily:
                    return Cron.Daily();
                case RecurringBillingInterval.Weekly:
                    return Cron.Weekly();
                case RecurringBillingInterval.Monthly:
                    return Cron.Monthly();
               case RecurringBillingInterval.Yearly:
                    return Cron.Yearly();
                default: return "* * * * *"; //Not specified

            }*/
        }
    }

}