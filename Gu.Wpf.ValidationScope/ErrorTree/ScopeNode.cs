namespace Gu.Wpf.ValidationScope
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    [DebuggerDisplay("ScopeNode Errors: {errors?.Count ?? 0}, Children: {Children.Count}, Source: {Source}")]
    internal sealed class ScopeNode : Node
    {
        private readonly WeakReference<DependencyObject> sourceReference;

        public ScopeNode(DependencyObject source, IErrorNode child)
            : base(child)
        {
            this.sourceReference = new WeakReference<DependencyObject>(source);
        }

        public override DependencyObject Source
        {
            get
            {
                DependencyObject source;
                this.sourceReference.TryGetTarget(out source);
                return source;
            }
        }

        protected override IReadOnlyList<ValidationError> GetAllErrors()
        {
            if (this.Source == null)
            {
                // not sure we need to protect against null here but doing it to be safe in case GC collects the binding.
                return EmptyValidationErrors;
            }

            if (this.AllChildren.Any())
            {
                var allErrors = this.AllChildren.OfType<ErrorNode>()
                                             .SelectMany(x => x.GetValidationErrors())
                                             .ToList();
                return allErrors;
            }
            else
            {
                return EmptyValidationErrors;
            }
        }
    }
}