using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FFImageLoadingTransformationIssue
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {
            InitializeComponent();

            ResizePic.Clicked += ResizePic_Clicked;
            Monochrome.Clicked += Monochrome_Clicked;
        }

        private async void Monochrome_Clicked(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            byte[] imageBytes = webClient.DownloadData("https://tinyurl.com/qph49y4");

            using (var image = await Plugin.ImageEdit.CrossImageEdit.Current.CreateImageAsync(imageBytes))
            {
                var data = image.ToMonochrome().ToJpeg(50);
                cacheImg.Source = ImageSource.FromStream(() => new MemoryStream(data));
                CircleImg.Source = ImageSource.FromStream(() => new MemoryStream(data));
            }
        }

        private async void ResizePic_Clicked(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            byte[] imageBytes = webClient.DownloadData("https://tinyurl.com/qph49y4");

            using (var image = await Plugin.ImageEdit.CrossImageEdit.Current.CreateImageAsync(imageBytes))
            {
                var data = image.Crop(0, 1, 100, 100).ToPng();
                cacheImg.Source = ImageSource.FromStream(() => new MemoryStream(data));
                CircleImg.Source = ImageSource.FromStream(() => new MemoryStream(data));
            }
        }
    }
}
