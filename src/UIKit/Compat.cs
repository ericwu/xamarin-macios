//
// Compat.cs: Stuff we won't provide in Xamarin.iOS.dll or newer XAMCORE_* profiles
//
// Authors:
//   Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2013, 2015 Xamarin, Inc.


using System;

namespace XamCore.UIKit {

#if !XAMCORE_2_0
	public partial class UIPrintFormatter {

		[Obsolete ("This cannot be directly created. Use UISimpleTextPrintFormatter instead.")]
		public UIPrintFormatter (string text)
		{
		}

		[Obsolete ("This cannot be directly created. Use UISimpleTextPrintFormatter instead.")]
		public UIPrintFormatter (MonoTouch.Foundation.NSAttributedString attributedText)
		{
		}

		[Obsolete ("This cannot be used directly. Use UISimpleTextPrintFormatter instead.")]
		public virtual MonoTouch.Foundation.NSAttributedString AttributedText { get; set; }
	}
#endif

#if !XAMCORE_3_0
	public partial class UIAdaptivePresentationControllerDelegate {

		[Obsolete ("Incorrect signature. Use the overload with a UITraitCollection parameter.")]
		public virtual UIViewController GetAdaptivePresentationStyle (UIPresentationController controller, UIModalPresentationStyle style)
		{
			return null;
		}
	}

#if !XAMCORE_2_0
	public partial class  UIPopoverPresentationControllerDelegate {

		[Obsolete ("Incorrect signature. Use the overload with a UITraitCollection parameter.")]
		public override UIViewController GetAdaptivePresentationStyle (UIPresentationController controller, UIModalPresentationStyle style)
		{
			return null;
		}
	}
#endif

	public partial class UIAdaptivePresentationControllerDelegate_Extensions {

		[Obsolete ("Incorrect signature. Use the overload with a UITraitCollection parameter.")]
		public static UIViewController GetAdaptivePresentationStyle (IUIAdaptivePresentationControllerDelegate This, UIPresentationController controller, UIModalPresentationStyle style)
		{
			return null;
		}
	}

	public static partial class NSIdentifier {

		[Obsolete ("Use GetIdentifier method")]
		public static string Identifier (this NSLayoutConstraint This)
		{
			return This.GetIdentifier ();
		}
	}
#endif
}
