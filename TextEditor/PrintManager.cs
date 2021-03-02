﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextEditor
{
    class PrintManager
    {
        public static readonly int DPI = 96;
        private readonly RichTextBox _textBox;

        public PrintManager(RichTextBox textBox)
        {
            _textBox = textBox;
        }

        public bool Print()
        {
            PrintDialog dlg = new PrintDialog();

            if(dlg.ShowDialog() == true)
            {
                PrintQueue printqueue = dlg.PrintQueue;

                DocumentPaginator paginator = GetPaginator(
                    printqueue.UserPrintTicket.PageMediaSize.Width.Value,
                    printqueue.UserPrintTicket.PageMediaSize.Height.Value
                    );
                dlg.PrintDocument(paginator, "TextEditor Printing");

                return true;
            }
            return false;
        }

        public DocumentPaginator GetPaginator ( double pageWidth , double pageHeight)
        {
            TextRange originalRange = new TextRange(_textBox.Document.ContentStart, _textBox.Document.ContentEnd);

            MemoryStream memoryStream = new MemoryStream();

            originalRange.Save(memoryStream, DataFormats.Xaml);

            FlowDocument copy = new FlowDocument();

            TextRange copyRange = new TextRange(
                copy.ContentStart, copy.ContentEnd);

            copyRange.Load(memoryStream, DataFormats.Xaml);

            DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

            return new PrintingPaginator(paginator, new Size(pageWidth, pageHeight), new Size(DPI, DPI));
        }

        public void PrintPreview()
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog(this);
            dlg.ShowDialog();
        }
    }
}
