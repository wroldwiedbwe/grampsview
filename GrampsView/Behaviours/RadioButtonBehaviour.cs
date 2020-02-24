namespace GrampsView.Behaviours
{
    using GrampsView.UserControls;

    using System;

    using Xamarin.Forms;

    public class RadioButtonBehaviour : Behavior<CheckBoxExtended>
    {
        protected override void OnAttachedTo(CheckBoxExtended bindable)
        {
            if (bindable is null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnAttachedTo(bindable);
            bindable.CheckedChanged += Bindable_CheckedChanged;
        }

        protected override void OnDetachingFrom(CheckBoxExtended bindable)
        {
            if (bindable is null)
            {
                throw new ArgumentNullException(nameof(bindable));
            }

            base.OnDetachingFrom(bindable);
            bindable.CheckedChanged -= Bindable_CheckedChanged;
        }

        private void Bindable_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}