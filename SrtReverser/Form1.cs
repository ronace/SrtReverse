using System.Windows.Forms;

namespace SrtReverser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            switch (SrtReverse.SrtReverser.Reverse(@"d:\The.Duchess.1080p.BluRay.x264-1920.srt", @"d:\The.Duchess.1080p.BluRay.x264-1920.Reversed.srt"))
            {
                case SrtReverse.SrtReverser.Results.Ok:
                    break;

                case SrtReverse.SrtReverser.Results.UnsupportedFileExtention:
                    break;
            }
        }
    }
}
