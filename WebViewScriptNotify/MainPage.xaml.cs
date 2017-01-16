using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebViewScriptNotify
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void WebView_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            await WebView.InvokeScriptAsync("eval", new[]
            {
                "var Anchors = document.getElementsByTagName(\"a\");" +
                "for (var i = 0; i < Anchors.length ; i++) {" +
                "    Anchors[i].addEventListener(\"contextmenu\", " +
                "        function (event) {" +
                "            window.external.notify(this.href);" +
                "        }," +
                "        false);" +
                "}"
            });
        }

        private void WebView_OnScriptNotify(object sender, NotifyEventArgs e)
        {
            var pointer = Window.Current.CoreWindow.PointerPosition;
            var url = e.Value;
            TextBlockLink.Text = $"Pointer X({pointer.X}) | Y({pointer.Y}) ->  url: {url}";

            TextBlockLink2.Text = url;

            Ppup.HorizontalOffset = pointer.X - 300;
            Ppup.VerticalOffset = pointer.Y - 60;

            Ppup.IsOpen = true;
        }

    }
}
