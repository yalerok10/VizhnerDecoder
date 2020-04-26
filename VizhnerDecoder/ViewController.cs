using CoreGraphics;
using Foundation;
using MobileCoreServices;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.IO;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace VizhnerDecoder
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            Styling();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void Styling()
        {
            NavigationController.NavigationBar.SetBackgroundImage(
                UIColor.FromRGBA(0, 0, 0, 0.3f).ToImage(), UIBarMetrics.Default
            );
            UISegmentedControl.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.White
            }, UIControlState.Normal);
            UISegmentedControl.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.SystemPinkColor
            }, UIControlState.Selected);
            SourceTextView.Layer.BorderWidth = 1;
            SourceTextView.Layer.BorderColor = UIColor.DarkGray.CGColor;
            SourceTextView.Layer.CornerRadius = 8.0f;
            ResultTextView.Layer.BorderWidth = 1;
            ResultTextView.Layer.BorderColor = UIColor.DarkGray.CGColor;
            ResultTextView.Layer.CornerRadius = 8.0f;
        }

        public bool isEncodeAction = true;

        async partial void UploadButton_TouchUpInside(UIButton sender)
        {
            var fileData = await CrossFilePicker.Current.PickFile(new string[] {
                    UTType.Text,
                    "org.openxmlformats.wordprocessingml.document"
                });
            if (fileData != null)
            {
                if (fileData.FileName.EndsWith("txt"))
                {
                    SourceTextView.Text = File.ReadAllText(fileData.FilePath, Encoding.GetEncoding(1251));
                }
                else
                {
                    using (var doc = new WordDocument(fileData.GetStream(), FormatType.Docx))
                    {
                        SourceTextView.Text = doc.GetText().Remove(0, 61);
                    }

                }
            }
        }

        partial void CodeButton_TouchUpInside(UIButton sender)
        {
            string key = KeyField.Text;
            string sourceText = SourceTextView.Text;
            if (key != "" && sourceText != "")
            {
                if (!isEncodeAction)
                {
                    string encodedText = Decoder.DecodeString(SourceTextView.Text, KeyField.Text);
                    ResultTextView.Text = encodedText;
                }
                else
                {
                    string encodedText = Decoder.EncodeString(SourceTextView.Text, KeyField.Text);
                    ResultTextView.Text = encodedText;
                }
            }
            SourceTextView.ResignFirstResponder();
            KeyField.ResignFirstResponder();
        }

        partial void ChangedCodeType(UISegmentedControl sender)
        {
            if (sender.SelectedSegment == 0)
            {
                isEncodeAction = true;
                CodeButton.SetTitle("Зашифровать", UIControlState.Normal);
            } else
            {
                isEncodeAction = false;
                CodeButton.SetTitle("Расшифровать", UIControlState.Normal);
            }
        }

        partial void SaveTxtButton_TouchUpInside(UIButton sender)
        {
            var textAlert = UIAlertController.Create("Сохранение", "Введите имя файла", UIAlertControllerStyle.Alert);
            textAlert.AddTextField(textField => { });

            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
            var okayAction = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, alertAction =>
                {
                    SaveToTxt(textAlert.TextFields[0].Text, ResultTextView.Text);
                }
            );

            textAlert.AddAction(cancelAction);
            textAlert.AddAction(okayAction);
            PresentViewController(textAlert, true, null);
            
        }

        private void SaveToTxt(string fileName, string content) {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                fileName + ".txt");
            File.WriteAllText(filePath, content);
        }

        private void SaveToDocx(string fileName, string content)
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                fileName + ".docx");
            WordDocument document = new WordDocument();
            document.EnsureMinimal();
            document.LastParagraph.AppendText(content);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            document.Save(fs, FormatType.Docx);
            fs.Close();
            document.Close();
        }

        partial void SaveDocxButton_TouchUpInside(UIButton sender)
        {
            var textAlert = UIAlertController.Create("Сохранение", "Введите имя файла", UIAlertControllerStyle.Alert);
            textAlert.AddTextField(textField => { });

            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
            var okayAction = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, alertAction =>
                {
                    SaveToDocx(textAlert.TextFields[0].Text, ResultTextView.Text);
                }
            );

            textAlert.AddAction(cancelAction);
            textAlert.AddAction(okayAction);
            PresentViewController(textAlert, true, null);
        }
    }
}