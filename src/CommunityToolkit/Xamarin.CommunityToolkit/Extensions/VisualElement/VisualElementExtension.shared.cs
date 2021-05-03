﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="VisualElement"/>.
	/// </summary>
	public static partial class VisualElementExtension
	{
		public static Task<bool> ColorTo(this VisualElement element, Color color, uint length = 250u, Easing? easing = null)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));

			var animationCompletionSource = new TaskCompletionSource<bool>();

			new Animation
			{
				{ 0, 1, new Animation(v => element.BackgroundColor = new Color(v, element.BackgroundColor.G, element.BackgroundColor.B, element.BackgroundColor.A), element.BackgroundColor.R, color.R) },
				{ 0, 1, new Animation(v => element.BackgroundColor = new Color(element.BackgroundColor.R, v, element.BackgroundColor.B, element.BackgroundColor.A), element.BackgroundColor.G, color.G) },
				{ 0, 1, new Animation(v => element.BackgroundColor = new Color(element.BackgroundColor.R, element.BackgroundColor.G, v, element.BackgroundColor.A), element.BackgroundColor.B, color.B) },
				{ 0, 1, new Animation(v => element.BackgroundColor = new Color(element.BackgroundColor.R, element.BackgroundColor.G, element.BackgroundColor.B, v), element.BackgroundColor.A, color.A) },
			}.Commit(element, nameof(ColorTo), 16, length, easing, (d, b) => animationCompletionSource.SetResult(true));

			return animationCompletionSource.Task;
		}

		public static void AbortAnimations(this VisualElement element, params string[] otherAnimationNames)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));

			ViewExtensions.CancelAnimations(element);
			element.AbortAnimation(nameof(ColorTo));

			if (otherAnimationNames == null)
				return;

			foreach (var name in otherAnimationNames)
				element.AbortAnimation(name);
		}

		internal static bool TryFindParentElementWithParentOfType<T>(this VisualElement? element, out VisualElement? result, out T? parent) where T : VisualElement
		{
			result = null;
			parent = null;

			while (element?.Parent != null)
			{
				if (element.Parent is not T parentElement)
				{
					element = element.Parent as VisualElement;
					continue;
				}

				result = element;
				parent = parentElement;

				return true;
			}

			return false;
		}

		internal static bool TryFindParentOfType<T>(this VisualElement? element, out T? parent) where T : VisualElement
			=> TryFindParentElementWithParentOfType(element, out _, out parent);
	}
}