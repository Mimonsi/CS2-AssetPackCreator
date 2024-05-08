using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetPackCreator
{
    public partial class MarkdownPreview : Form
    {
        public MarkdownPreview()
        {
            InitializeComponent();
            Task t = webView.EnsureCoreWebView2Async();
            while (!t.IsCompletedSuccessfully)
            {
                Application.DoEvents();
            }
            markdownTimer.Start();
        }

        private void MarkdownPreview_Load(object sender, EventArgs e)
        {
            markdownTimer_Tick(this, new EventArgs());
        }

        private void markdownTimer_Tick(object sender, EventArgs e)
        {
            webView.CoreWebView2.NavigateToString(Main.markdownPreviewHtml);
        }
    }
}
