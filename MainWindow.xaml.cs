using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Documents;
using System.Windows.Media;

namespace SharpEditor
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class MainWindow
    {
        private readonly Brush REFRENCE_TYPES_BRUSH = Brushes.DodgerBlue;
        private readonly Brush VALUE_TYPES_BRUSH = Brushes.DodgerBlue;

        public MainWindow()
        {
            InitializeComponent();
            SyntaxTimer();
        }

        private void SyntaxTimer()
        {
            var syntaxTime = new Timer();
            syntaxTime.Elapsed += SyntaxHighlightText;
            syntaxTime.Interval = 1000;
            syntaxTime.Enabled = true;
        }

        private void SyntaxHighlightText(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var wordRanges = GetAllWordRanges(HostTextBox.Document);
                foreach (var wordRange in wordRanges)
                {
                    //ref: Syntax.txt
                    switch (wordRange.Text.ToLower())
                    {
                        //---VALUE_TYPES---
                        case "bool":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "byte":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "char":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "decimal":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "double":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "enum":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "float":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "int":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "long":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "sbyte":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "short":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "struct":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "uint":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "ulong":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "ushort":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        //---REFRENCE_TYPES---
                        case "class":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "interface":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "delegate":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "dynamic":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "object":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        case "string":
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, VALUE_TYPES_BRUSH);
                            break;
                        default:
                            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                            break;
                    }
                }
            });
        }

        private static IEnumerable<TextRange> GetAllWordRanges(FlowDocument doc)
        {
            const string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
            var pointer = doc.ContentStart;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    var textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    var matches = Regex.Matches(textRun, pattern);
                    foreach (Match match in matches)
                    {
                        var startIndex = match.Index;
                        var length = match.Length;
                        var start = pointer.GetPositionAtOffset(startIndex);
                        if (start == null) continue;
                        var end = start.GetPositionAtOffset(length);
                        yield return new TextRange(start, end);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }
    }
}