using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace StructuredLogging.Desktop.Utilities.Behaviours
{
    public sealed class AllowableCharactersTextBoxBehavior
        : Behavior<TextBox>
    {
        public static readonly DependencyProperty AllowWhitespaceProperty = DependencyProperty.Register("AllowWhitespace", typeof(bool), typeof(AllowableCharactersTextBoxBehavior), new FrameworkPropertyMetadata(default(bool)));
        public static readonly DependencyProperty RegularExpressionProperty = DependencyProperty.Register("RegularExpression", typeof(string), typeof(AllowableCharactersTextBoxBehavior), new FrameworkPropertyMetadata(".*"));
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(AllowableCharactersTextBoxBehavior), new FrameworkPropertyMetadata(int.MinValue));

        public bool AllowWhitespace { get { return (bool)GetValue(AllowWhitespaceProperty); } set { SetValue(AllowWhitespaceProperty, value); } }
        public string RegularExpression { get { return (string)GetValue(RegularExpressionProperty); } set { SetValue(RegularExpressionProperty, value); } }
        public int MaxLength { get { return (int) GetValue(MaxLengthProperty); } set { SetValue(MaxLengthProperty, value); } }
        
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs args)
        {
            if (args.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(args.DataObject.GetData(DataFormats.Text));

                if (!IsValid(text, true))
                {
                    args.CancelCommand();
                }
            }
            else
            {
                args.CancelCommand();
            }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Space && !AllowWhitespace)
                args.Handled = true;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs args)
        {
            args.Handled = !IsValid(args.Text, false);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private bool IsValid(string newText, bool isPasted)
        {
            return !ExceedsMaxLength(newText, isPasted) && Regex.IsMatch(newText, RegularExpression);
        }

        private bool ExceedsMaxLength(string newText, bool isPasted)
        {
            if (MaxLength == 0)
            { 
                return false;
            }

            return LengthOfModifiedText(newText, isPasted) > MaxLength;
        }

        private int LengthOfModifiedText(string newText, bool isPasted)
        {
            int countOfSelectedChars = AssociatedObject.SelectedText.Length;
            int caretIndex = AssociatedObject.CaretIndex;
            string text = AssociatedObject.Text;

            if (countOfSelectedChars > 0 || isPasted)
            {
                text = text.Remove(caretIndex, countOfSelectedChars);
                return text.Length + newText.Length;
            }

            bool insert = Keyboard.IsKeyToggled(Key.Insert);

            return insert && caretIndex < text.Length ? text.Length : text.Length + newText.Length;
        }
    }
}