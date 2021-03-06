﻿namespace Gu.Wpf.ValidationScope.Ui.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using TestStack.White.UIItems;
    using TestStack.White.UIItems.ListBoxItems;

    public class ParentChildScopesWindowTests : WindowTests
    {
        protected override string WindowName { get; } = "ParentChildScopesWindow";

        private TextBox TextBoxScopeTextBox1 => this.Window.Get<TextBox>("TextBoxScopeTextBox1");

        private TextBox TextBoxScopeTextBox2 => this.Window.Get<TextBox>("TextBoxScopeTextBox2");

        private ComboBox TextBoxScopeComboBox1 => this.Window.Get<ComboBox>("TextBoxScopeComboBox1");

        private TextBox ComboBoxScopeTextBox1 => this.Window.Get<TextBox>("ComboBoxScopeTextBox1");

        private TextBox ComboBoxScopeTextBox2 => this.Window.Get<TextBox>("ComboBoxScopeTextBox2");

        private ComboBox ComboBoxScopeComboBox2 => this.Window.Get<ComboBox>("ComboBoxScopeComboBox2");

        private TextBox NoScopeTextBox1 => this.Window.Get<TextBox>("NoScopeTextBox1");

        private TextBox NoScopeTextBox2 => this.Window.Get<TextBox>("NoScopeTextBox2");

        private ComboBox NoScopeComboBox1 => this.Window.Get<ComboBox>("NoScopeComboBox1");

        private GroupBox Scope => this.Window.GetByText<GroupBox>("Scope");

        private IReadOnlyList<string> ScopeErrors => this.Scope.GetErrors();

        private string ScopeHasError => this.Scope.Get<Label>("HasErrorTextBlock").Text;

        private GroupBox Node => this.Window.GetByText<GroupBox>("Node");

        private string ChildCount => this.Node.Get<Label>("ChildCountTextBlock").Text;

        private IReadOnlyList<string> NodeErrors => this.Node.GetErrors();

        private IReadOnlyList<string> NodeChildren => this.Node.GetChildren();

        private string NodeHasError => this.Node.Get<Label>("HasErrorTextBlock").Text;

        private string NodeType => this.Node.Get<Label>("NodeTypeTextBlock").Text;

        [SetUp]
        public void SetUp()
        {
            this.TextBoxScopeTextBox1.Enter("0");
            this.TextBoxScopeTextBox2.Enter("0");
            this.PressTab();
        }

        [Test]
        public void CheckNodeType()
        {
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
            this.TextBoxScopeTextBox1.Enter('a');
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);
            this.TextBoxScopeTextBox1.Enter('1');
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void AddThenRemoveErrorTextBox()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.TextBoxScopeTextBox1.Enter('a');
            var expectedErrors = new[] { "Value 'a' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.TextBoxScopeTextBox1.Enter('1');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void AddThenRemoveErrorTwiceTextBox()
        {
            this.AddThenRemoveErrorTextBox();
            this.AddThenRemoveErrorTextBox();
        }

        [Test]
        public void AddThenRemoveErrorComboBox()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.ComboBoxScopeComboBox2.Enter('a');
            var expectedErrors = new[] { "Value 'a' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.ComboBoxScopeComboBox2.Enter('1');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void AddThenRemoveErrorTwiceComboBox()
        {
            this.AddThenRemoveErrorComboBox();
            this.AddThenRemoveErrorComboBox();
        }

        [Test]
        public void NoErrorWhenNotScopedTextBox()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.NoScopeTextBox1.Enter('a');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void NoErrorWhenNotScopedComboBox()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.NoScopeComboBox1.Enter('a');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void AddTwoErrorsThenRemoveThemOneByOne()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.TextBoxScopeTextBox1.Enter('a');
            var expectedErrors = new[] { "Value 'a' could not be converted." };

            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.ComboBoxScopeComboBox2.Enter('b');
            expectedErrors = new[] { "Value 'a' could not be converted.", "Value 'b' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.ComboBoxScopeTextBox2.Enter('1');
            expectedErrors = new[] { "Value 'a' could not be converted." };

            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.ComboBoxScopeTextBox1.Enter('2');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }

        [Test]
        public void AddTwoErrorsThenThenRemoveBothAtOnce()
        {
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);

            this.TextBoxScopeTextBox2.Enter('a');
            var expectedErrors = new[] { "Value 'a' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.ComboBoxScopeComboBox2.Enter('b');
            expectedErrors = new[] { "Value 'a' could not be converted.", "Value 'b' could not be converted." };
            Assert.AreEqual("HasError: True", this.ScopeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.ScopeErrors);

            Assert.AreEqual("Children: 1", this.ChildCount);
            Assert.AreEqual("HasError: True", this.NodeHasError);
            CollectionAssert.AreEqual(expectedErrors, this.NodeErrors);
            CollectionAssert.AreEqual(new[] { "System.Windows.Controls.StackPanel" }, this.NodeChildren);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ScopeNode", this.NodeType);

            this.NoScopeTextBox2.Enter('1');
            Assert.AreEqual("HasError: False", this.ScopeHasError);
            CollectionAssert.IsEmpty(this.ScopeErrors);

            Assert.AreEqual("Children: 0", this.ChildCount);
            Assert.AreEqual("HasError: False", this.NodeHasError);
            CollectionAssert.IsEmpty(this.NodeErrors);
            Assert.AreEqual("Gu.Wpf.ValidationScope.ValidNode", this.NodeType);
        }
    }
}
