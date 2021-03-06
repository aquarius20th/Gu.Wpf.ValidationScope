namespace Gu.Wpf.ValidationScope.Ui.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using TestStack.White.InputDevices;
    using TestStack.White.UIItems;
    using TestStack.White.UIItems.Finders;
    using TestStack.White.UIItems.ListBoxItems;
    using TestStack.White.WindowsAPI;

    public static class UIItemExt
    {
        public static string ItemStatus(this IUIItem item)
        {
            return (string)item.AutomationElement.GetCurrentPropertyValue(AutomationElementIdentifiers.ItemStatusProperty);
        }

        public static IReadOnlyList<T> GetMultiple<T>(this UIItemContainer container, string automationId)
            where T : IUIItem
        {
            return container.GetMultiple(SearchCriteria.ByAutomationId(automationId))
                           .OfType<T>()
                           .ToList();
        }

        public static T GetByText<T>(this UIItemContainer container, string text)
            where T : UIItem
        {
            return container.Get<T>(SearchCriteria.ByText(text));
        }

        public static T GetByIndex<T>(this UIItemContainer container, int index)
            where T : UIItem
        {
            return container.Get<T>(SearchCriteria.Indexed(index));
        }

        public static IReadOnlyList<string> GetErrors(this UIItemContainer container)
        {
            return container.GetMultiple<Label>("ErrorTextBlock")
                            .Select(x => x.Text)
                            .ToList();
        }

        public static IReadOnlyList<string> GetChildren(this UIItemContainer container)
        {
            return container.GetMultiple<Label>("ChildTextBlock")
                            .Select(x => x.Text)
                            .ToList();
        }

        public static void Enter(this TextBox textBox, char @char)
        {
            textBox.SelectAllText();
            Keyboard.Instance.Send(new string(@char, 1), textBox.ActionListener);
        }

        public static void SelectAllText(this TextBox textBox)
        {
            textBox.Focus();
            Keyboard.Instance.HoldKey(KeyboardInput.SpecialKeys.CONTROL);
            Keyboard.Instance.Send("a", textBox.ActionListener);
            Keyboard.Instance.LeaveKey(KeyboardInput.SpecialKeys.CONTROL);
        }

        public static void Enter(this ComboBox comboBox, char @char)
        {
            comboBox.SelectAllText();
            Keyboard.Instance.Send(new string(@char, 1), comboBox.ActionListener);
        }

        public static void SelectAllText(this ComboBox comboBox)
        {
            comboBox.Focus();
            Keyboard.Instance.HoldKey(KeyboardInput.SpecialKeys.CONTROL);
            Keyboard.Instance.Send("a", comboBox.ActionListener);
            Keyboard.Instance.LeaveKey(KeyboardInput.SpecialKeys.CONTROL);
        }

        public static IEnumerable<IUIItem> Ancestors(this IUIItem item)
        {
            var parent = item.GetParent<IUIItemContainer>();
            while (parent != null)
            {
                yield return parent;
                parent = parent.GetParent<IUIItemContainer>();
            }
        }
    }
}