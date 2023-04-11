using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    public static class WordsUtils
    {
        public static DocumentBuilder ReplaceMergeField(this Document document, string mergeField)
        {
            var builder = new DocumentBuilder(document);
            builder.MoveToMergeField(mergeField);
            return builder;
        }

        public static void InsertDocument(this Node insertAfterNode, Document srcDoc)
        {
            // Make sure that the node is either a paragraph or table.
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
              (!insertAfterNode.NodeType.Equals(NodeType.Table)))
                throw new ArgumentException("The destination node should be either a paragraph or table.");

            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = insertAfterNode.ParentNode;

            // This object will be translating styles and lists during the import.
            NodeImporter importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

            // Loop through all sections in the source document.
            foreach (Section srcSection in srcDoc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node srcNode in srcSection.Body)
                {
                    // Let's skip the node if it is a last empty paragraph in a section.
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        Paragraph para = (Paragraph)srcNode;
                        if (para.IsEndOfSection && !para.HasChildNodes)
                            continue;
                    }

                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Node newNode = importer.ImportNode(srcNode, true);

                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }
        }
    }
}
