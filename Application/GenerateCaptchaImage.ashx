using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace Application
{
    public class GenerateCaptchaImage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // Get CAPTCHA code from session
            string captchaCode = context.Session["CaptchaCode"] as string;

            if (string.IsNullOrEmpty(captchaCode))
            {
                context.Response.StatusCode = 400;
                return;
            }

            // Generate CAPTCHA image
            using (Bitmap bmp = new Bitmap(150, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                {
                    using (Brush brush = new SolidBrush(Color.DarkBlue))
                    {
                        g.DrawString(captchaCode, font, brush, new PointF(10, 10));
                    }
                }

                // Add some random noise
                Random random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(bmp.Width);
                    int y = random.Next(bmp.Height);
                    bmp.SetPixel(x, y, Color.Black);
                }

                // Output the image
                context.Response.ContentType = "image/png";
                bmp.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        public bool IsReusable => false;
    }
}