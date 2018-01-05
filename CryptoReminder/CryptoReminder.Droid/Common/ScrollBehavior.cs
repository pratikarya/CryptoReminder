using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.Animations;

namespace CryptoReminder.Droid.Common
{
    [Register("CryptoReminder.Droid.Common.ScrollBehavior")]
    public class ScrollBehavior : CoordinatorLayout.Behavior
    {
        public ScrollBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int nestedScrollAxes)
        {
            return nestedScrollAxes == ViewCompat.ScrollAxisVertical ||
                     base.OnStartNestedScroll(coordinatorLayout, child, directTargetChild, target, nestedScrollAxes);
        }

        public override void OnNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed)
        {
            base.OnNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed);

            var floatingActionButtonChild = child.JavaCast<FloatingActionButton>();

            if (dyConsumed < 0 && floatingActionButtonChild.Visibility != ViewStates.Visible)
            {
                floatingActionButtonChild.Visibility = ViewStates.Visible;
                floatingActionButtonChild.Animate().TranslationY(0).SetInterpolator(new DecelerateInterpolator(2)).Start();
            }
            else if (dyConsumed > 0 && floatingActionButtonChild.Visibility == ViewStates.Visible)
            {
                floatingActionButtonChild.Visibility = ViewStates.Invisible;
                floatingActionButtonChild.Animate().TranslationY(floatingActionButtonChild.Height + 16).SetInterpolator(new AccelerateInterpolator(2)).Start();
            }
        }

    }
}