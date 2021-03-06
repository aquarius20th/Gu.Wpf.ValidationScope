namespace Gu.Wpf.ValidationScope.Ui.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using TestStack.White.UIItems;
    using TestStack.White.WindowsAPI;

    public class DataGridWindowTests : WindowTests
    {
        protected override string WindowName { get; } = "DataGridWindow";

        private ListView DataGrid => this.Window.Get<ListView>("DataGrid");

        private GroupBox Scope => this.Window.GetByText<GroupBox>("Scope");

        private IReadOnlyList<string> ScopeErrors => this.Scope.GetErrors();

        private string ScopeHasError => this.Scope.Get<Label>("HasErrorTextBlock").Text;

        private GroupBox Node => this.Window.GetByText<GroupBox>("Node");

        private string ChildCount => this.Node.Get<Label>("ChildCountTextBlock").Text;

        private IReadOnlyList<string> NodeErrors => this.Node.GetErrors();

        private string NodeHasError => this.Node.Get<Label>("HasErrorTextBlock").Text;

        private IReadOnlyList<string> NodeChildren => this.Node.GetChildren();

        private string NodeType => this.Node.Get<Label>("NodeTypeTextBlock").Text;

        [Test]
        public void AddThenRemoveError()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            var cell = this.DataGrid.Rows[0].Cells[0];
            cell.Click();
            cell.Enter("a");
            this.Window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
            var expectedErrors = new[] { "Value 'a' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.DataGrid Items.Count:3" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.Window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.TAB);
            this.Window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.BACKSPACE);
            cell.Enter("2");
            this.Window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);

            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }
    }
}