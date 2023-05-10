using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the folder path containing the PDF files to merge
            string folderPath = @"C:\PDFFolder";

            // Get all the PDF files in the folder
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            // Sort the PDF files alphabetically
            Array.Sort(pdfFiles);

            // Create a new document to merge the PDFs
            Document mergedDocument = new Document();

            // Create a PdfCopy object to write the merged PDFs
            PdfCopy copy = new PdfCopy(mergedDocument, new FileStream(folderPath+"\\MergedPDF.pdf", FileMode.Create));

            // Open the merged document
            mergedDocument.Open();

            // Merge each PDF file
            foreach (string pdfFile in pdfFiles)
            {
                // Open the PDF file
                PdfReader reader = new PdfReader(pdfFile);

                // Merge the pages into the merged document
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage importedPage = copy.GetImportedPage(reader, i);
                    copy.AddPage(importedPage);
                }

                // Close the PDF reader
                reader.Close();
            }

            // Close the merged document
            mergedDocument.Close();

            Console.WriteLine("PDF files merged successfully!");
            Console.ReadLine();
        }
    }
}
