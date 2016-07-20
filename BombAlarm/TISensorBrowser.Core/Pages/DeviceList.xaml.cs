﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TISensorBrowser
{	
	public partial class DeviceList : ContentPage
	{	
		IAdapter adapter;
		ObservableCollection<IDevice> devices;

		public DeviceList (IAdapter adapter)
		{
			InitializeComponent ();

			this.adapter = adapter;
			this.devices = new ObservableCollection<IDevice> ();
			listView.ItemsSource = devices;

			adapter.DeviceDiscovered += (object sender, DeviceDiscoveredEventArgs e) => {
				Device.BeginInvokeOnMainThread(() => {
					devices.Add (e.Device);

				});
				if (e.Device.Name == "Bean"){
					toService(sender, e.Device);
				}
			};

			adapter.ScanTimeoutElapsed += (sender, e) => {
				adapter.StopScanningForDevices(); // not sure why it doesn't stop already, if the timeout elapses... or is this a fake timeout we made?
				Device.BeginInvokeOnMainThread ( () => {
					IsBusy = false;
					DisplayAlert("Timeout", "Bluetooth scan timeout elapsed", "OK");
				});
			};

			ScanAllButton.Activated += (sender, e) => {
				InfoFrame.IsVisible = false;
				StartScanning();
			};
			InfoFrame.IsVisible = false;
			StartScanning();
			var time_page = new SetTimerPage ();


			// load services on the next page
			Navigation.PushAsync(time_page);
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			if (((ListView)sender).SelectedItem == null) {
				return;
			}
			Debug.WriteLine (" xxxxxxxxxxxx  OnItemSelected " + e.SelectedItem.ToString ());
			var device = e.SelectedItem as IDevice;
			toService (sender, device);

		}
		public void toService(object sender, IDevice device){
			IsBusy = false;
			StopScanning ();
			var servicePage = new ServiceList(adapter, device);


			// load services on the next page
			Navigation.PushAsync(servicePage);

	
		}

		void StartScanning () {
			IsBusy = true;
			StartScanning (Guid.Empty);
		}
		void StartScanning (Guid forService) {

			if (adapter.IsScanning) {
				IsBusy = false;
				adapter.StopScanningForDevices();
				Debug.WriteLine ("StartScanning > adapter.StopScanningForDevices()");
			} else {
				devices.Clear();
				IsBusy = true;
				adapter.StartScanningForDevices(forService);
				Debug.WriteLine ("adapter.StartScanningForDevices("+forService+")");
			}
		}

		void StopScanning () {
			// stop scanning
			new Task( () => {
				if(adapter.IsScanning) {
					IsBusy = false;
					Debug.WriteLine ("Still scanning, stopping the scan");
					adapter.StopScanningForDevices();
				}
			}).Start();
		}
	}
}
