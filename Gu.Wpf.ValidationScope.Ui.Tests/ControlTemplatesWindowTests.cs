namespace Gu.Wpf.ValidationScope.Ui.Tests
{
    using Gu.Wpf.ValidationScope.Demo;
    using NUnit.Framework;

    using TestStack.White.UIItems;

    public class ControlTemplatesWindowTests : WindowTests
    {
        protected override string WindowName { get; } = "ControlTemplatesWindow";

        [Test]
        public void Updates()
        {
            CollectionAssert.IsEmpty(this.Window.GetErrors());
            var textBox1 = this.Window.Get<TextBox>(AutomationIDs.TextBox1);
            textBox1.Enter('a');
            CollectionAssert.AreEqual(new[] { "Value 'a' could not be converted." }, this.Window.GetErrors());

            var textBox2 = this.Window.Get<TextBox>(AutomationIDs.TextBox2);
            textBox2.Enter('b');
            var expectedErrors = new[]
            {
                    "Value 'a' could not be converted.",
                    "Value 'b' could not be converted."
                };
            CollectionAssert.AreEqual(expectedErrors, this.Window.GetErrors());
            textBox1.Enter('1');
            CollectionAssert.IsEmpty(this.Window.GetErrors());
        }
    }
}