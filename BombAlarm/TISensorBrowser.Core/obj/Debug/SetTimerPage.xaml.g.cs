// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace TISensorBrowser {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class SetTimerPage : ContentPage {
        
        private TimePicker timePicker;
        
        private Switch t_switch;
        
        private Entry entry;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(SetTimerPage));
            timePicker = this.FindByName <TimePicker>("timePicker");
            t_switch = this.FindByName <Switch>("t_switch");
            entry = this.FindByName <Entry>("entry");
        }
    }
}
