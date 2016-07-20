﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;
using System.Linq;

namespace TISensorBrowser
{	
	public class CharacteristicDetails 
	{	

		public ICharacteristic Characteristic;


		public CharacteristicDetails (IAdapter adapter, IDevice device, IService service, ICharacteristic Characteristic)
		{
			this.Characteristic = Characteristic;

//			if (Characteristic.CanUpdate) {
//				Characteristic.ValueUpdated += (s, e) => {
//					Debug.WriteLine("Characteristic.ValueUpdated");
//					Device.BeginInvokeOnMainThread( () => {
//						UpdateValue(Characteristic);
//					});
//				};
//				Characteristic.StartUpdates();
//			}
		}

		public async void OnAppearing ()
		{
			if (Characteristic.CanRead) {
				var c = await Characteristic.ReadAsync();
				UpdateValue(c);
			}
		}

		public void OnDisappearing() 
		{
			
			if (Characteristic.CanUpdate) {
				Characteristic.StopUpdates();
			}
		}
		public void UpdateValue (ICharacteristic c) {
//			Name.Text = c.Name;
//			//ID.Text = c.ID.ToString(); OTHER INFO YOU CAN GET FROM BEAN
//			ID.Text = c.ID.PartialFromUuid ();
			string hex = BitConverter.ToString(c.Value);
			//var s = (from i in c.Value where i == 0 select );
			//select i.ToString ()); //i.ToString ("X"));
			List<string> numbers = hex.Split('-').ToList<string>();
			string resultstring = "";
			int counter = 0;

			foreach (string number  in numbers) {
				counter++;
				int temp = int.Parse(number, System.Globalization.NumberStyles.HexNumber);
				if (counter == 1) { //only grab first value, can be changed to get more values
					resultstring = temp.ToString ();
				}
				if (temp == 31) {
					Debug.WriteLine ("ITS 31");	
				}
			}
			Debug.WriteLine (resultstring);
		}
	}
}



