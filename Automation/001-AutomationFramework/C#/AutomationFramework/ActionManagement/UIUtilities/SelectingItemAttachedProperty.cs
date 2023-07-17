using ActionManagement.CoreActions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ActionManagement.UIUtilities
{
    public class SelectingItemAttachedProperty
    {
        public static readonly DependencyProperty SelectingItemProperty = DependencyProperty.RegisterAttached(
                "SelectingItem",
                typeof(SettingStatus),
                typeof(SelectingItemAttachedProperty),
                new PropertyMetadata(default(SettingStatus), OnSelectingItemChanged));

        public static SettingStatus GetSelectingItem(DependencyObject target)
        {
            return (SettingStatus)target.GetValue(SelectingItemProperty);
        }

        public static void SetSelectingItem(DependencyObject target, SettingStatus value)
        {
            target.SetValue(SelectingItemProperty, value);
        }

        public static void OnSelectingItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid == null || grid.SelectedItem == null)
            {
                return;
            }

            // Works with .Net 4.5
            grid.Dispatcher.InvokeAsync(() =>
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem, null);
            });

            // Works with .Net 4.0
            grid.Dispatcher.BeginInvoke((Action)(() =>
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem, null);
            }));
        }
    }
}
