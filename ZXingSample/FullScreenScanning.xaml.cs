using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace ZXingSample
{
	public partial class FullScreenScanning : ZXingScannerPage
	{
		public FullScreenScanning()
		{
			InitializeComponent();
		}

		public void Handle_OnScanResult(Result result)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
                IsScanning = false;
                var client = new HttpClient
                {
                    BaseAddress = new Uri("http://192.168.0.15:32547/api/disposelist/item/add/")
                };

                var response = await client.PutAsync($"{result.Text}/nandre04", null);

                await DisplayAlert(response.StatusCode.ToString(), result.Text, "OK");

                await Task.Delay(3000);
                IsScanning = true;
            });
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			IsScanning = true;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			IsScanning = false;
		}
	}
}
