// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace VizhnerDecoder
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CodeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField KeyField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView ResultTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveDocxButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView SourceTextView { get; set; }

        [Action ("ChangedCodeType:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ChangedCodeType (UIKit.UISegmentedControl sender);

        [Action ("CodeButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CodeButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SaveDocxButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SaveDocxButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SaveTxtButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SaveTxtButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("UploadButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UploadButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CodeButton != null) {
                CodeButton.Dispose ();
                CodeButton = null;
            }

            if (KeyField != null) {
                KeyField.Dispose ();
                KeyField = null;
            }

            if (ResultTextView != null) {
                ResultTextView.Dispose ();
                ResultTextView = null;
            }

            if (SaveDocxButton != null) {
                SaveDocxButton.Dispose ();
                SaveDocxButton = null;
            }

            if (SourceTextView != null) {
                SourceTextView.Dispose ();
                SourceTextView = null;
            }
        }
    }
}